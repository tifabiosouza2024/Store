using Microsoft.AspNetCore.Mvc;

namespace FS.Store.WebAPI.Extensions
{
    public static class ControllerExtension
    {
        private const string OrderBy = "OrderBy";
        public static IActionResult RedirectAdminToBadRequest(this Controller controller, string? message = null)
        {
            return controller.RedirectToAction("BadRequest", "Public", new { Area = "Admin", Message = message });
        }

        public static Dictionary<string, string> GetSorting(this Controller controller)
        {
            return controller.HttpContext.Request.Query.Where(q => q.Key.StartsWith(OrderBy, StringComparison.OrdinalIgnoreCase))
                .ToDictionary(q => q.Key.Replace(OrderBy, "", StringComparison.InvariantCultureIgnoreCase), q => q.Value.ToString());
        }

        public static Dictionary<string, string> GetSortingRedirect(this Controller controller)
        {
            return controller.HttpContext.Request.Query.Where(q => q.Key.StartsWith(OrderBy, StringComparison.OrdinalIgnoreCase))
                .ToDictionary(k => k.Key, v => v.Value.ToString());
        }
    }
}
