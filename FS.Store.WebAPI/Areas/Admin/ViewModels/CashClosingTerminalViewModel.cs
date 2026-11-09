namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashClosingTerminalViewModel
    {
        public Guid PaymentTerminalId { get; set; }

        public string? TerminalName { get; set; }

        public string? ProviderName { get; set; }
        public string? ProviderLogo { get; set; }

        public decimal CreditAmount { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal PixAmount { get; set; }
    }
}
