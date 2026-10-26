using FS.Store.Core.Interfaces;
using FS.Store.Model.Entity;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Extensions;
using FS.Store.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class HomeController(AuthHelper authHelper
        , IOperationService operationService
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

            var operationsRoles = await operationUserRoleService.FindAllByUserIdAsync(User.GetUserId());
            vm.Operations = new SelectList(operationsRoles.Select(o => o?.Operation), "Id", "Name", vm.OperationId);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOperation(Guid operationId)
        {
            var user = await userService.FindByIdAsync(User.GetUserId());
            if (user == null)
                return this.RedirectAdminToBadRequest();

            await authHelper.AssignPermissionsAsync(user, operationId);
            user.LastOperationId = operationId;
            await userService.AddOrUpdateAsync(user);
            return RedirectToAction("Index");
        }
    }
}
