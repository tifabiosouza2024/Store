using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class HomeController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
