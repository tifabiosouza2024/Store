using FS.Store.Core.Extension;
using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.Model.Dtos;
using FS.Store.Model.Entity;
using FS.Store.Model.Extensions;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CashSessionController(ICashRegisterSessionService cashRegisterSessionService
        , IOperationService operationService
        , ICashClosingService cashClosingService
        , CashSessionHelper cashSessionHelper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            IEnumerable<CashRegisterSession> cashSessions = await cashRegisterSessionService.GetCashRegisterSessionOpenedAsync();
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
            CashSessionOpenViewModel session = new()
            {
                OperationName = operation?.Name,
                CashSessionAlreadyExists = await cashRegisterSessionService.AlreadyOpened()
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

            var registerSession = new CashRegisterSession()
            {
                CashRegisterStatus = Model.Enums.CashRegisterStatusEnum.Open,
                OpenedAt = DateTime.Now,
                OpenedByUserId = User.GetUserId(),
                OpeningAmount = session.Amount,
                OperationId = User.GetOperationId()
            };

            await cashRegisterSessionService.AddOrUpdateAsync(registerSession);

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

        private async Task GetCashCloseSessionViewModel(CashClosingViewModel sessionVM, CashRegisterSession session)
        {
            var operation = await operationService.FindByIdAsync(User.GetOperationId());

            sessionVM.OperationName = operation?.Name;
            sessionVM.OpeningAmount = session.OpeningAmount;
            sessionVM.CashRegisterSessionId = session.Id;
            sessionVM.OpenedAt = session.OpenedAt;
            if (operation?.PaymentTerminals != null)
            {
                foreach (var terminal in operation.PaymentTerminals)
                {
                    sessionVM.Terminals.Add(new CashClosingTerminalViewModel()
                    {
                        PaymentTerminalId = terminal.Id,
                        TerminalName = terminal.Name,
                        ProviderName = terminal.PaymentProvider?.Name,
                    });
                }
            }
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

            return RedirectToAction("Index");
        }

        public IActionResult History()
        {
            return View();
        }

    }
}
