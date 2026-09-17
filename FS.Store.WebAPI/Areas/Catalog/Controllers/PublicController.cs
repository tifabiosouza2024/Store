using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class PublicController(IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var presentationPath = Path.Combine(
            _environment.WebRootPath,
            "img",
            "presentation");

            var images = Directory
                .EnumerateFiles(presentationPath)
                .Select(Path.GetFileName)
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            return View(images);
        }
    }
}
