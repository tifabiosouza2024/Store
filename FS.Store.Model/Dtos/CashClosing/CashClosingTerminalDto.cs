namespace FS.Store.Model.Dtos.CashClosing
{
    public class CashClosingTerminalDto
    {
        public Guid PaymentTerminalId { get; set; }

        public string? TerminalName { get; set; }

        public string? ProviderName { get; set; }

        public decimal CreditAmount { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal PixAmount { get; set; }
    }
}
