using FS.Store.DAL.Interfaces;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class PublicController
        (AuthHelper authHelper
        , IUserRepository userRepository)
        : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userRepository.FindByEmail(model.Email);

            if (await authHelper.LoginAsync(user, model.Password))
            {
                return RedirectToAction("Index", "CashSession", new { Area = "Admin" });
            }

            ModelState.AddModelError(nameof(LoginViewModel.Password), "Senha Incorreta");

            return View(model);
        }
    }
}
