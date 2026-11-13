using FS.Store.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{
    /// <summary>
    /// Representa o controle de abertura e fechamento de caixa
    /// <para>
    /// TypeId: long
    /// </para>
    /// </summary>
    [Table("cash_register_session")]
    [Comment("controle de abertura e fechamento de caixa")]
    public class CashRegisterSession : BaseEntity<long>
    {
        public CashRegisterSession()
        {
            CashMovements = [];
        }
        [Column("opened_at", TypeName = "datetime(0)")]
        [Comment("Data/Hora de abertura")]
        [DataType(DataType.DateTime)]
        public DateTime OpenedAt { get; set; }

        [Column("closed_at", TypeName = "datetime(0)")]
        [Comment("Data/Hora de fechamento")]
        [DataType(DataType.DateTime)]
        public DateTime? ClosedAt { get; set; }

        [Column("opening_amount", TypeName = "decimal(10,2)")]
        [Comment("Valor em espécie do total da abertura de caixa")]
        public decimal OpeningAmount { get; set; }

        [Column("total_sales", TypeName = "decimal(10,2)")]
        [Comment("Valor total vendido")]
        public decimal TotalSales { get; set; }

        [Column("cash_register_status")]
        [Comment("Status da abertura de caixa (1 - aberto, 2 - fechado, 3 - cancelado)")]
        public CashRegisterStatusEnum CashRegisterStatus { get; set; }

        [Column("cash_session_number")]
        public int CashSessionNumber { get; set; }

        [Column("operation_id")]
        [ForeignKey("operation_id")]
        [Comment("ID da Operação")]
        public Guid OperationId { get; set; }
        public virtual Operation? Operation { get; set; }

        [Column("opened_by_user_id")]
        [ForeignKey("opened_by_user_id")]
        [Comment("ID do Usuário que abriu o caixa")]
        public Guid OpenedByUserId { get; set; }
        public virtual User? OpenedByUser { get; set; }

        [Column("closed_by_user_id")]
        [ForeignKey("closed_by_user_id")]
        [Comment("ID do Usuário que fechou o caixa")]
        public Guid? ClosedByUserId { get; set; }
        public virtual User? ClosedByUser { get; set; }

        [Column("changed_by_user_id")]
        [ForeignKey("changed_by_user_id")]
        [Comment("ID do Usuário que alterou o caixa")]
        public Guid? ChangedByUserId { get; set; }
        public virtual User? ChangedByUser { get; set; }

        public virtual ICollection<CashMovement> CashMovements { get; set; }

        public virtual CashClosing? CashClosing { get; set; }

        /// <summary>
        /// retorna o balanço, obtendo os valores do fechamento subtraindo o valor de entrada, resultando no total vendido.
        /// </summary>
        public decimal ClosingBalance
            => CashClosing?.ClosingTotalSales ?? 0 - OpeningAmount;
    }
}
