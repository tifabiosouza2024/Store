using FS.Store.Core.Interfaces;
using FS.Store.Model.Dtos.Operations;
using FS.Store.Model.Entity;
using FS.Store.Model.Extensions;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SUPER")]
    public class OperationController(IOperationService operationService
        , IRoleService roleService
        , IUserService userService) : Controller
    {
        public async Task<IActionResult> Index(int? page, int? qttPerPage, string? search = null)
        {
            var sorting = this.GetSorting();

            Expression<Func<Operation, bool>>? filter = null;

            if (search.IsNotNullOrEmpty())
                filter = q => q.Name.Contains(search) || q.Document.Contains(search) || q.Email.Contains(search);

            var operations = await operationService.GetAllPagedAsync(page, qttPerPage, filter: filter, orderBy: q => q.OrderBy(o => o.Name), dynamicSorts: sorting);
            return View(operations);
        }

        public IActionResult Create()
        {
            return View(new OperationCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(OperationCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);

            }
            bool hasError = false;
            if (await operationService.ExistsAsync(o => o.Document.Equals(vm.Document)))
            {
                ModelState.AddModelError(nameof(vm.Document), "Já existe uma operação cadastrada com este CNPJ");
                hasError = true;
            }

            if (await operationService.ExistsAsync(o => o.Email.Equals(vm.Email.Trim())))
            {
                ModelState.AddModelError(nameof(vm.Document), "Já existe uma operação cadastrada com este E-mail");
                hasError = true;
            }
            if (hasError)
                return View(vm);

            Operation operation = await operationService.CreateOperation(vm.ToDto<CreateOperationDto>());

            var role = await roleService.FirstOrDefaultAsync(o => o.NormalizedName.Equals("ADMIN"));
            await userService.CreateNewUserOperationAsync(new()
            {
                Email = vm.ManagerEmail,
                Password = vm.Password,
                RoleId = role.Id,
                OperationId = operation.Id
            });

            return RedirectToAction(nameof(Index));
        }
    }
}
