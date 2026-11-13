using FS.Store.Core.Extension;
using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.Model.Dtos.CashClosing;
using FS.Store.Model.Elements;
using FS.Store.Model.Entity;
using FS.Store.Model.Enums;
using FS.Store.Model.Extensions;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Extensions;
using FS.Store.WebAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CashSessionController(ILogger<CashSessionController> logger
        , ICashRegisterSessionService cashRegisterSessionService
        , IOperationService operationService
        , IOperationUserRoleService operationUserRoleService
        , ICurrentCashSessionService currentCashSessionService
        , CashSessionHelper cashSessionHelper) : Controller
    {
        public async Task<IActionResult> Index(int page, int qttPerPage)
        {
            var sorting = this.GetSorting();
            Pagination<CashRegisterSession> cashSessions = await cashRegisterSessionService.GetCashRegisterSessionOpenedPagedAsync(page: page, qttPerPage: qttPerPage, sorting);
            CashSessionIndexViewModel vm = new()
            {
                CashRegisterSessionsOpened = cashSessions
            };
            return View(vm);
        }

        public async Task<IActionResult> Open()
        {
            var operation = await operationService.FindByIdAsync(User.GetOperationId());
            if (operation == null)
            {
                return this.RedirectAdminToBadRequest("Você não tem operação selecionada para continuar.");
            }
            var qtt = await cashRegisterSessionService.Count();
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("Quantidade de caixas abertos: {}", qtt);
            CashSessionOpenViewModel session = new()
            {
                OperationName = operation?.Name,
                CashSessionAlreadyExists = await cashRegisterSessionService.AlreadyOpened(),
                CashSessionNumber = qtt + 1,
                OpenedAt = DateTime.Now
            };
            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Open(CashSessionOpenViewModel session)
        {
            if (!ModelState.IsValid)
            {
                var operation = await operationService.FindByIdAsync(User.GetOperationId());
                session.OperationName = operation?.Name;
                return View(session);
            }

            // Realizar a conversão do model para entity
            var registerSession = new CashRegisterSession()
            {
                CashRegisterStatus = Model.Enums.CashRegisterStatusEnum.Open,
                OpenedAt = session.OpenedAt,
                OpenedByUserId = User.GetUserId(),
                OpeningAmount = session.Amount,
                CashSessionNumber = session.CashSessionNumber,
                OperationId = User.GetOperationId()
            };

            await cashRegisterSessionService.AddOrUpdateAsync(registerSession);

            currentCashSessionService.Set(registerSession.Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(long id)
        {
            var session = await cashRegisterSessionService.GetByIdAsync(id);
            if (session == null)
                return this.RedirectAdminToBadRequest("Sessão inexistente para esta operação");


            var vm = new CashRegisterSessionEditViewModel();

            var operations = await operationUserRoleService.FindAllOperationsByUserIdAsync();
            if (operations == null || !operations.Any())
                return this.RedirectAdminToBadRequest("Você não tem permissão para editar esta sessão de caixa.");

            vm.CashSessionNumber = session.CashSessionNumber;
            vm.CashRegisterStatus = session.CashRegisterStatus;
            vm.ChangedByUserName = session.ChangedByUser?.Name;
            vm.UpdatedAt = session.UpdatedAt;
            vm.ClosedAt = session.ClosedAt;
            vm.ClosedByUserName = session.ClosedByUser?.Name;
            vm.Deleted = session.Deleted;
            vm.Id = session.Id;
            vm.OpenedAt = session.OpenedAt;
            vm.OpenedByUserName = session.OpenedByUser?.Name;
            vm.OpeningAmount = session.OpeningAmount;
            vm.OperationId = session.OperationId;
            vm.StatusOptions = session.CashRegisterStatus.ToSelectListItemsInEditView();
            vm.WasClosed = session.CashRegisterStatus == CashRegisterStatusEnum.Closed;
            vm.Operations = [.. operations.Select(o => new SelectListItem { Text = o.Name, Value = o.Id.ToString(), Selected = o.Id == session.OperationId })];
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, CashRegisterSessionEditViewModel sessionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(sessionVM);
            }

            if (sessionVM.Id != id)
                return this.RedirectAdminToBadRequest("Sessão inexistente para esta operação");

            var session = await cashRegisterSessionService.GetByIdAsync(id);
            if (session == null)
                return this.RedirectAdminToBadRequest("Sessão inexistente para esta operação");

            session.OperationId = sessionVM.OperationId;
            session.CashSessionNumber = sessionVM.CashSessionNumber;
            session.OpeningAmount = sessionVM.OpeningAmount;
            session.OpenedAt = sessionVM.OpenedAt;
            session.ClosedAt = sessionVM.ClosedAt;
            session.CashRegisterStatus = sessionVM.CashRegisterStatus;
            session.ChangedByUserId = User.GetUserId();
            session.Deleted = sessionVM.Deleted;

            await cashRegisterSessionService.SaveChangesAsync();

            if (sessionVM.CashRegisterStatus == CashRegisterStatusEnum.Canceled || sessionVM.Deleted)
                currentCashSessionService.Clear();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Close(long? id)
        {
            var session = await cashRegisterSessionService.GetToClose(id);
            if (session == null)
                return this.RedirectAdminToBadRequest("Este caixa não está disponível para ser fechado.");
            CashClosingViewModel sessionVM = new();
            await GetCashCloseSessionViewModel(sessionVM, session);

            return View(sessionVM);
        }

        [HttpPost]
        public async Task<IActionResult> Close(CashClosingViewModel sessionVM)
        {
            if (!ModelState.IsValid)
            {
                var session = await cashRegisterSessionService.GetToClose(sessionVM.CashRegisterSessionId);
                if (session == null)
                    return this.RedirectAdminToBadRequest("Este caixa não está disponível para ser fechado.");

                await GetCashCloseSessionViewModel(sessionVM, session);

                return View(sessionVM);
            }

            await cashSessionHelper.CloseSessionAsync(sessionVM.ToDto<CashClosingDto>());

            currentCashSessionService.Clear();
            return RedirectToAction("Index");
        }

        private async Task GetCashCloseSessionViewModel(CashClosingViewModel sessionVM, CashRegisterSession session)
        {
            var operation = await operationService.FindByIdAsync(User.GetOperationId());

            sessionVM.OperationName = operation?.Name;
            sessionVM.OpeningAmount = session.OpeningAmount;
            sessionVM.CashRegisterSessionId = session.Id;
            sessionVM.OpenedAt = session.OpenedAt;
            if (operation?.PaymentTerminals != null)
            {
                foreach (var terminal in operation.PaymentTerminals.OrderBy(c => c.Name))
                {
                    sessionVM.Terminals.Add(new CashClosingTerminalViewModel()
                    {
                        PaymentTerminalId = terminal.Id,
                        TerminalName = terminal.Name,
                        ProviderName = terminal.PaymentProvider?.Name,
                        ProviderLogo = terminal.PaymentProvider?.LogoIcon
                    });
                }
            }
            sessionVM.CashMovements = [.. session.CashMovements];
        }

        public async Task<IActionResult> History(int page, int qttPerPage)
        {
            var sorting = this.GetSorting();
            Pagination<CashRegisterSession> result = await cashRegisterSessionService.GetAllPagedAsync(page, qttPerPage, sorting, q => q.OrderByDescending(o => o.Id));
            var vm = new CashSessionHistoryViewModel()
            {
                Pagination = result
            };
            return View(vm);
        }

    }
}
