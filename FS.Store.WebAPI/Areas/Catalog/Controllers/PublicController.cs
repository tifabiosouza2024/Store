using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class PublicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
