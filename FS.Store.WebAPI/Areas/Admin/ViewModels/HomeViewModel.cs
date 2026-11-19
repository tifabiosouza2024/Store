using FS.Store.Model.Entity;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class HomeViewModel
    {
        public Guid? OperationId { get; set; }
        public string? OperationName { get; set; }
        public IEnumerable<OperationUserRole>? OperationRoles { get; set; }
        public IEnumerable<GlobalUserRole>? GlobalRoles { get; set; }
        public long? CashSessionId { get; set; }

        public int QttInvite { get; set; }
    }
}
