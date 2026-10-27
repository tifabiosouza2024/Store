using FS.Store.Core.Interfaces;
using FS.Store.Model.Entity;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SUPER")]
    public class CashSessionController(ICashRegisterSessionService cashRegisterSessionService
        , IOperationService operationService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            IEnumerable<CashRegisterSession> cashSessions = await cashRegisterSessionService.GetCashRegisterSessionOpenedAsync(User.GetOperationId());
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
                CashSessionAlreadyExists = await cashRegisterSessionService.AlreadyOpened(operation!.Id)
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

        public IActionResult Close()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

    }
}
