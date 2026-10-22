using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SUPER,SELLER")]
    public class CashMovement : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // Sangria
        public IActionResult Withdrawal()
        {
            return View();
        }

        public IActionResult Supply()
        {
            return View();
        }
    }
}
