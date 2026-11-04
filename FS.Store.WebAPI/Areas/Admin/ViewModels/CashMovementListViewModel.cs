namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashMovementListViewModel
    {
        public long CashSessionId { get; set; }

        public decimal OpeningAmount { get; set; }

        public decimal TotalSupply { get; set; }

        public decimal TotalWithdrawal { get; set; }
        public decimal TotalExpense { get; set; }

        public decimal Balance => OpeningAmount + TotalSupply - TotalWithdrawal - TotalExpense;

        public int TotalRecords { get; set; }

        public List<CashMovementItemViewModel> Items { get; set; } = [];
    }
}
