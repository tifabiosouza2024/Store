using FS.Store.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{
    /// <summary>
    /// Registra as movimentações financeiras de acordo com o tipo: Sangria, Suprimento e Despesas
    /// <see cref="CashMovementTypeEnum"/> para maiores informações dos tipos.
    /// <para>
    /// TypeId: long</br>
    /// </para>
    /// </summary>
    [Table("cash_movement")]
    [Comment("Registra as movimentações financeiras")]
    public class CashMovement : BaseEntity<long>
    {
        [Column("cash_movement_type")]
        [StringLength(20)]
        [Comment("Tipo de movimentação (suprimento, sangria, despesa)")]
        public CashMovementTypeEnum CashMovementType { get; set; }

        [Column("amount", TypeName = "decimal(10,2)")]
        [Comment("valor da movimentação")]
        public decimal Amount { get; set; }

        [Column("description")]
        [Comment("Descrição")]
        [StringLength(255)]
        public string Description { get; set; } = default!;

        [Column("occurred_at", TypeName = "datetime(0)")]
        [Comment("Data/Hora em que ocorreu a movimentação")]
        [DataType(DataType.DateTime)]
        public DateTime OccurredAt { get; set; }

        [Column("canceled_at", TypeName = "datetime(0)")]
        [Comment("Data/Hora em que houve o cancelamento da movimentação")]
        [DataType(DataType.DateTime)]
        public DateTime CanceledAt { get; set; }

        [Column("cancellation_reason")]
        [Comment("Motivo do cancelamento")]
        [StringLength(255)]
        public string? CancellationReason { get; set; }

        [Column("created_by_user_id")]
        [ForeignKey("created_by_user_id")]
        [Comment("Usuário que criou a movimentação")]
        public Guid CreatedByUserId { get; set; }
        public virtual User? CreatedByUser { get; set; }

        [Column("canceled_by_user_id")]
        [ForeignKey("canceled_by_user_id")]
        [Comment("Usuário que realizou o cancelamento da movimentação")]
        public Guid? CanceledByUserId { get; set; }
        public virtual User? CanceledByUser { get; set; }

        [Column("cash_register_session_id")]
        [ForeignKey("cash_register_session_id")]
        [Comment("Sessão de Caixa")]
        public long CashRegisterSessionId { get; set; }
        public virtual CashRegisterSession? CashRegisterSession { get; set; }
    }
}
