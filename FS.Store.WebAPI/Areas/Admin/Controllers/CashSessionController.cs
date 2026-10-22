using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SUPER")]
    public class CashSessionController : Controller
    {
        public IActionResult Open()
        {
            return View();
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
