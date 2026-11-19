using FS.Store.Core.Extension;
using FS.Store.Core.Interfaces;
using FS.Store.Model.Dtos;
using FS.Store.Model.Extensions;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SUPER,ADMIN,MANAGER")]
    public class UserController(IUserService userService
        , IRoleService roleService) : Controller
    {
        public async Task<IActionResult> Index(int? page, int? qttPerPage)
        {
            var sorting = this.GetSorting();
            var users = await userService.GetAllPagedAsync(currentPage: page, qttPerPage: qttPerPage, q => q.OrderBy(o => o.Name), dynamicSorts: sorting);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await roleService.GetAssignableRolesAsync();
            var vm = new CreateUserViewModel
            {
                Roles = roles,
                OperationName = User.GetOperationName()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            var operationId = User.GetOperationId();

            if (operationId == Guid.Empty)
                return Forbid();

            var roles = await roleService.GetAssignableRolesAsync();

            if (!roles.Any(r => r.Id == model.RoleId))
                ModelState.AddModelError(nameof(model.RoleId), "Você não possui permissão para atribuir essa função.");

            if (!ModelState.IsValid)
            {
                model.Roles = roles;
                model.OperationName = User.GetOperationName();
                return View(model);
            }
            var dto = model.ToDto<AssignUserToOperationDto>();
            dto.InvitedByUserId = User.GetUserId();
            dto.OperationId = User.GetOperationId();
            await userService.CreateNewUserOperationAsync(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}
