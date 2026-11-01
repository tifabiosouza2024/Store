using FS.Store.Model.Dtos.CashClosing;

namespace FS.Store.Model.Dtos
{
    public class CashClosingDto
    {
        public string? OperationName { get; set; }

        public decimal OpeningAmount { get; set; }

        public long CashRegisterSessionId { get; set; }

        public DateTime OpenedAt { get; set; }

        public decimal CashAmount { get; set; }

        public string? Notes { get; set; }

        public List<CashClosingTerminalDto> Terminals { get; set; } = [];
    }
}
