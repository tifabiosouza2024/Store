using System.ComponentModel.DataAnnotations;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashMovementViewModel
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser informado e precisa ser maior que zero.")]
        public decimal Amount { get; set; }

        public decimal AvailableBalance { get; set; }

        public string? Description { get; set; }

        public long CashSessionId { get; set; }

        public int CashSessionNumber { get; set; }
    }
}
