using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{
    /// <summary>
    /// Registra o fechamento de caixa
    /// <para>
    /// TypeId: long
    /// </para>
    /// </summary>
    [Table("cash_closing")]
    [Comment("Registra o fechamento do caixa")]
    public class CashClosing : BaseEntity<long>
    {
        public CashClosing()
        {
            this.Terminals = [];
        }
        [Column("closed_at", TypeName = "datetime(0)")]
        [Comment("Data/Hora de fechamento")]
        [DataType(DataType.DateTime)]
        public DateTime ClosedAt { get; set; }

        [Column("cash_closing_amount", TypeName = "decimal(10,2)")]
        [Comment("Valor total em dinheiro encontrado no fechamento do caixa")]
        public decimal CashAmount { get; set; }

        [Column("total_credit", TypeName = "decimal(10,2)")]
        [Comment("Valor total de crédito passado em todas as maquininhas")]
        public decimal TotalCredit { get; set; }

        [Column("total_debit", TypeName = "decimal(10,2)")]
        [Comment("Valor total de débito passado em todas as maquininhas")]
        public decimal TotalDebit { get; set; }

        [Column("total_pix", TypeName = "decimal(10,2)")]
        [Comment("Valor total de PIX recebido em todas as maquininhas")]
        public decimal TotalPix { get; set; }

        [Column("total_expenses", TypeName = "decimal(10,2)")]
        [Comment("Valor total de despesas pago em dinheiro")]
        public decimal TotalExpenses { get; set; }

        [Column("total_withdrawal", TypeName = "decimal(10,2)")]
        [Comment("Valor total de saques realizado em dinheiro")]
        public decimal TotalWithdrawal { get; set; }

        [Column("total_cash_inflows", TypeName = "decimal(10,2)")]
        [Comment("Valor total de aportes em dinheiro")]
        public decimal TotalCashInflows { get; set; }

        [Column("notes")]
        [Comment("Observações sobre o fechamento")]
        [StringLength(255)]
        public string? Notes { get; set; }

        [Column("cash_register_session_id")]
        [ForeignKey("cash_register_session_id")]
        [Comment("Id da sessão de Caixa")]
        public long CashRegisterSessionId { get; set; }
        public virtual CashRegisterSession? CashRegisterSession { get; set; }

        [Column("closed_by_user_id")]
        [ForeignKey("closed_by_user_id")]
        [Comment("Usuário que realizou o fechamento")]
        public Guid ClosedByUserId { get; set; }
        public virtual User? ClosedByUser { get; set; }

        public virtual ICollection<CashClosingTerminal>? Terminals { get; set; }

        /// <summary>
        /// Retorna a soma do valores. Dinheiro + Pix + Crédito + Débito + Despesas + Sangrias
        /// </summary>
        public decimal ClosingTotalSales
            => CashAmount + TotalPix + TotalCredit + TotalDebit + TotalExpenses + TotalWithdrawal;
    }
}
