using Microsoft.AspNetCore.Mvc.Rendering;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class HomeViewModel
    {
        public Guid? OperationId { get; set; }
        public string? OperationName { get; set; }
        public IEnumerable<SelectListItem> Operations { get; set; } = [];
    }
}
