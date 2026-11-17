using FS.Store.Core.Extension;
using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountController(PasswordHelper passwordHelper
        , IUserService userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var user = await userService.FindByIdAsync(User.GetUserId());
            var vm = new AccountIndexViewmodel()
            {
                PersonalData = new PersonalDataViewModel()
                {
                    Name = user?.Name,
                    Email = user?.Email
                }
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePersonalData(PersonalDataViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await userService.FindByIdAsync(User.GetUserId());

            bool existsUser = false;
            if (!vm.Email.Trim().Equals(user?.Email))
                existsUser = await userService.ExistsAsync(u => u.Email.Equals(vm.Email));

            if (existsUser)
                ModelState.AddModelError(nameof(PersonalDataViewModel.Email), "Já existe um usuário cadastrado com este e-mail");

            if (user != null && !existsUser && (!user.Name.Equals(vm.Name) || !user.Email.Equals(vm.Email)))
            {
                user.Name = vm.Name;
                user.Email = vm.Email;

                await userService.SaveChangesAsync();

                return RedirectToAction(nameof(Index), "Account", new { Area = "Admin" }, fragment: "personal-data");
            }

            var accountVM = new AccountIndexViewmodel()
            {
                PersonalData = vm
            };

            return View("Index", accountVM);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            var user = await userService.FindByIdAsync(User.GetUserId());
            if (user == null)
                return RedirectToAction("Login", "Public", new { Area = "Admin" });

            if (ModelState.IsValid)
            {
                if (passwordHelper.IsCorrectPassword(user, vm.CurrentPassword))
                {
                    user.PasswordHash = passwordHelper.HashPassword(user, vm.NewPassword);
                    user.MustChangePassword = false;
                    await userService.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), "Account", new { Area = "Admin" }, fragment: "personal-data");
                }

            }
            ModelState.AddModelError(nameof(ChangePasswordViewModel.CurrentPassword), "A senha atual informada não está correta.");

            return View(vm);
        }
    }
}
