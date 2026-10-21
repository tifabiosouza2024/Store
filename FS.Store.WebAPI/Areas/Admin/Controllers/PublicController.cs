using FS.Store.Core.Interfaces;
using FS.Store.WebAPI.Areas.Admin.ViewModels;
using FS.Store.WebAPI.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class PublicController(AuthHelper authHelper
        , IUserService userService) : Controller
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

            var user = await userService.FindByEmail(model.Email);

            if (await authHelper.LoginAsync(user, model))
            {
                return RedirectToAction("Index", "CashSession", new { Area = "Admin" });
            }

            ModelState.AddModelError(nameof(LoginViewModel.Password), "Senha Incorreta");

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Public", new { Area = "Admin" });
        }
    }
}
