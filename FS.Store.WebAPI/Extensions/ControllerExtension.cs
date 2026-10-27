using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Extensions
{
    public static class ControllerExtension
    {
        public static IActionResult RedirectAdminToBadRequest(this Controller controller, string? message = null)
        {
            return controller.RedirectToAction("BadRequest", "Public", new { Area = "Admin", Message = message });
        }
    }
}
