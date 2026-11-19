using FS.Store.Core.Extension;
using FS.Store.Core.Interfaces;
using FS.Store.Model.Entity;
using FS.Store.Model.Enums;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Extensions;
using FS.Store.WebAPI.Helpers;
using FS.Store.WebAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController(AuthHelper authHelper
        , IOperationService operationService
        , ICurrentCashSessionService currentCashSessionService
        , IOperationUserRoleService operationUserRoleService
        , IUserService userService) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeViewModel vm = new();
            Operation? operation = null;
            if (User.GetOperationId() != Guid.Empty)
            {
                operation = await operationService.FindByIdAsync(User.GetOperationId());
                vm.OperationName = operation?.Name;
                vm.OperationId = operation?.Id;
            }

            var user = await userService.FindByIdAsync(User.GetUserId(), includeProperties: q => q.Include(u => u.Operations).Include(u => u.GlobalRoles));
            if (user == null)
                return this.RedirectAdminToBadRequest("Você não tem permissão para acessar esta página");

            if (user.Operations != null && user.Operations.Count != 0)
            {
                vm.OperationRoles = user.Operations.Where(o => o.Status == RoleStatusEnum.Active);
                vm.QttInvite = user.Operations.Count(o => o.Status == RoleStatusEnum.Pending);
            }

            if (user.GlobalRoles != null && user.GlobalRoles.Count != 0)
            {
                vm.GlobalRoles = user.GlobalRoles;
            }

            vm.CashSessionId = await currentCashSessionService.GetOrResolveAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOperation(Guid? operationId)
        {
            var user = await userService.FindByIdAsync(User.GetUserId()
                , includeProperties: q => q.Include(o => o.Operations));
            if (user == null)
                return this.RedirectAdminToBadRequest("Você não está autenticado para realizar esta operação.");

            await authHelper.AssignPermissionsAsync(user, operationId);
            user.LastOperationId = operationId;
            await userService.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewInvite()
        {
            var pendingInvites = await operationUserRoleService.FindAllOperationsByUserIdAsync(RoleStatusEnum.Pending);
            return View(pendingInvites);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptInvite(long id)
        {
            var pendingInvites = await operationUserRoleService.FindAllOperationsByUserIdAsync(RoleStatusEnum.Pending);

            var pending = pendingInvites?.FirstOrDefault(i => i.Id == id);
            if (pending == null)
                return this.RedirectAdminToBadRequest("Você não tem permissão para realizar esta ação!");

            pending.AcceptedAt = DateTime.Now;
            pending.IsActive = true;
            pending.Status = RoleStatusEnum.Active;
            await operationUserRoleService.SaveChangesAsync();

            var user = await userService.FindByIdAsync(User.GetUserId());
            user?.LastOperationId = null;
            await userService.SaveChangesAsync();

            await authHelper.AssignPermissionsAsync(user, null);


            if (pendingInvites?.Count() > 1)
                return RedirectToAction(nameof(ViewInvite));


            return RedirectToAction("Index");
        }
    }
}
