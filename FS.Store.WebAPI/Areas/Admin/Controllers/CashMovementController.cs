using FS.Store.Core.Exceptions;
using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.Model.Dtos.CashMovement;
using FS.Store.Model.Enums;
using FS.Store.Model.Extensions;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CashMovementController(CashSessionHelper cashSessionHelper
        , ICashRegisterSessionService cashRegisterSessionService) : Controller
    {
        public async Task<IActionResult> Index(long? cashSessionId)
        {
            var session = await cashRegisterSessionService.GetToClose(cashSessionId);
            if (session == null)
                return this.RedirectAdminToBadRequest("Sessão de caixa não localizada para registro de entrada de dinheiro");

            CashMovementListViewModel vm = new();

            var result = session.CashMovements.Where(c => !c.Deleted).ToList();
            if (result == null)
                return this.RedirectAdminToBadRequest("Sessão de caixa não localizada para registro de entrada de dinheiro");

            if (result.Count != 0)
                vm.Items = result.ToDto<List<CashMovementItemViewModel>>();

            vm.CashSessionId = cashSessionId.GetValueOrDefault();
            vm.OpeningAmount = session.OpeningAmount;
            vm.TotalSupply = vm.Items.Where(i => i.CashMovementType == CashMovementTypeEnum.CashInflows).Sum(i => i.Amount);
            vm.TotalWithdrawal = vm.Items.Where(i => i.CashMovementType == CashMovementTypeEnum.Withdrawal).Sum(i => i.Amount);
            vm.TotalExpense = vm.Items.Where(i => i.CashMovementType == CashMovementTypeEnum.Expense).Sum(i => i.Amount);
            vm.TotalRecords = vm.Items.Count;
            return View(vm);
        }

        // Sangria
        [HttpGet]
        public async Task<IActionResult> Withdrawal(long cashSessionId)
        {
            var session = await cashRegisterSessionService.GetToClose(cashSessionId);
            if (session == null)
                return this.RedirectAdminToBadRequest("Sessão de caixa não localizada para registro de entrada de dinheiro");

            var model = new CashMovementViewModel
            {
                CashSessionId = cashSessionId,
                CashSessionNumber = session.CashSessionNumber,
                AvailableBalance = session.OpeningAmount
                    + session.CashMovements.Where(m => m.CashMovementType == CashMovementTypeEnum.CashInflows).Sum(m => m.Amount)
                    - session.CashMovements.Where(m => m.CashMovementType != CashMovementTypeEnum.CashInflows).Sum(m => m.Amount)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdrawal(long cashSessionId, CashMovementViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.CashSessionId != cashSessionId)
                return this.RedirectAdminToBadRequest("Código de sessão inválida");

            try
            {
                await cashSessionHelper.RegisterCashInflows(cashSessionId, model.ToDto<CashMovementDto>(), CashMovementTypeEnum.Withdrawal);
            }
            catch (ServiceException e)
            {
                return this.RedirectAdminToBadRequest(e.Message);
            }

            return RedirectToAction(nameof(Index), new { cashSessionId });
        }

        public async Task<IActionResult> Supply(long cashSessionId)
        {
            var session = await cashRegisterSessionService.GetToClose(cashSessionId);
            if (session == null)
                return this.RedirectAdminToBadRequest("Sessão de caixa não localizada para registro de entrada de dinheiro");

            var model = new CashMovementViewModel
            {
                CashSessionId = cashSessionId,
                CashSessionNumber = session.CashSessionNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Supply(long cashSessionId, CashMovementViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.CashSessionId != cashSessionId)
                return this.RedirectAdminToBadRequest("Código de sessão inválida");

            try
            {
                await cashSessionHelper.RegisterCashInflows(cashSessionId, model.ToDto<CashMovementDto>(), CashMovementTypeEnum.CashInflows);
            }
            catch (ServiceException e)
            {
                return this.RedirectAdminToBadRequest(e.Message);
            }

            return RedirectToAction(nameof(Index), new { cashSessionId });
        }

        public async Task<IActionResult> Expense(long cashSessionId)
        {
            var session = await cashRegisterSessionService.GetToClose(cashSessionId);
            if (session == null)
                return this.RedirectAdminToBadRequest("Sessão de caixa não localizada para registro de entrada de dinheiro");

            var model = new CashMovementViewModel
            {
                CashSessionId = cashSessionId,
                CashSessionNumber = session.CashSessionNumber,
                AvailableBalance = session.OpeningAmount
                    + session.CashMovements.Where(m => m.CashMovementType == CashMovementTypeEnum.CashInflows).Sum(m => m.Amount)
                    - session.CashMovements.Where(m => m.CashMovementType != CashMovementTypeEnum.CashInflows).Sum(m => m.Amount)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Expense(long cashSessionId, CashMovementViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.CashSessionId != cashSessionId)
                return this.RedirectAdminToBadRequest("Código de sessão inválida");

            try
            {
                await cashSessionHelper.RegisterCashInflows(cashSessionId, model.ToDto<CashMovementDto>(), CashMovementTypeEnum.Expense);
            }
            catch (ServiceException e)
            {
                return this.RedirectAdminToBadRequest(e.Message);
            }

            return RedirectToAction(nameof(Index), new { cashSessionId });
        }
    }
}
