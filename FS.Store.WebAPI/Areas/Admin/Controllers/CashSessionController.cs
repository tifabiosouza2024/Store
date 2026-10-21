using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CashSessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
