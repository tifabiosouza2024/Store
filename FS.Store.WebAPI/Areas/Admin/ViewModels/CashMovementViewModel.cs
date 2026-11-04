namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashMovementViewModel
    {
        public decimal Amount { get; set; }

        public decimal AvailableBalance { get; set; }

        public string? Description { get; set; }

        public long CashSessionId { get; set; }

        public int CashSessionNumber { get; set; }
    }
}
