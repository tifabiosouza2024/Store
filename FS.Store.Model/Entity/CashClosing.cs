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
        [Column("closed_at", TypeName = "datetime(0)")]
        [Comment("Data/Hora de fechamento")]
        [DataType(DataType.DateTime)]
        public DateTime ClosedAt { get; set; }

        [Column("cash_amount")]
        [Comment("Valor total em dinheiro")]
        public decimal CashAmount { get; set; }

        [Column("total_credit")]
        [Comment("Valor total de crédito")]
        public decimal TotalCredit { get; set; }

        [Column("total_debit")]
        [Comment("Valor total de débito")]
        public decimal TotalDebit { get; set; }

        [Column("total_pix")]
        [Comment("Valor total de PIX")]
        public decimal TotalPix { get; set; }

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
    }
}
