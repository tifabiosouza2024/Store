using System.ComponentModel.DataAnnotations;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashClosingViewModel
    {
        public string? OperationName { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "é necessario um valor positivo para abertura de caixa.")]
        public decimal OpeningAmount { get; set; }


        public long CashRegisterSessionId { get; set; }

        public DateTime OpenedAt { get; set; }

        public decimal CashAmount { get; set; }

        public string? Notes { get; set; }

        public List<CashClosingTerminalViewModel> Terminals { get; set; } = [];
    }
}
