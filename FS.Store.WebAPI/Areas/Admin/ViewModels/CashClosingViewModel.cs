using FS.Store.Model.Entity;
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
        public List<CashMovement> CashMovements { get; set; } = [];

        public decimal Withdrawals => CashMovements.Where(c => c.CashMovementType == Model.Enums.CashMovementTypeEnum.Withdrawal).Sum(c => c.Amount);
        public decimal Expenses => CashMovements.Where(c => c.CashMovementType == Model.Enums.CashMovementTypeEnum.Expense).Sum(c => c.Amount);
        public decimal Supplys => CashMovements.Where(c => c.CashMovementType == Model.Enums.CashMovementTypeEnum.CashInflows).Sum(c => c.Amount);
    }
}
