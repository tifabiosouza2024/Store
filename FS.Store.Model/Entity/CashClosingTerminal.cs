using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{

    /// <summary>
    /// Armazena o valor de cada terminal (maquininha) de cartão
    /// <para>
    /// TypeId: long
    /// </para>
    /// </summary>
    [Table("cash_closing_terminal")]
    [Comment("Armazena o valor de cada terminal (maquininha)")]
    public class CashClosingTerminal : BaseEntity<long>
    {
        [Column("credit_amount", TypeName = "decimal(10,2)")]
        [Comment("Valor em crédito do terminal")]
        public decimal CreditAmount { get; set; }

        [Comment("Valor em débito do terminal")]
        [Column("debit_amount", TypeName = "decimal(10,2)")]
        public decimal DebitAmount { get; set; }

        [Column("pix_amount", TypeName = "decimal(10,2)")]
        [Comment("Valor em PIX do terminal")]
        public decimal PixAmount { get; set; }

        [Column("payment_terminal_id")]
        [ForeignKey("payment_terminal_id")]
        [Comment("Terminal de pagamento (maquininha)")]
        public Guid PaymentTerminalId { get; set; }
        public virtual PaymentTerminal? PaymentTerminal { get; set; }

        [Column("cash_register_session_id")]
        [ForeignKey("cash_register_session_id")]
        public long CashRegisterSessionId { get; set; }
        public virtual CashRegisterSession? CashRegisterSession { get; set; }

        [Column("created_by_user_id")]
        [ForeignKey("created_by_user_id")]
        [Comment("Usuário que criou a movimentação")]
        public Guid CreatedByUserId { get; set; }
        public virtual User? CreatedByUser { get; set; }
    }
}
