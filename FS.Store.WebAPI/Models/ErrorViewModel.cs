using FS.Store.Model.Extensions;

namespace FS.Store.WebAPI.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => RequestId.IsNotNullOrEmpty();
    }
}
