using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{
    /// <summary>
    /// Armazena os terminais (maquininhas) de pagamentos das empresas
    /// <para>
    /// TypeId: Guid
    /// </para>
    /// </summary>
    [Table("payment_terminal")]
    [Comment("Armazena os terminais (maquininhas) de pagamento")]
    public class PaymentTerminal : BaseEntity<Guid>
    {
        [Column("name")]
        [StringLength(50)]
        [Comment("Nome do terminal")]
        public string Name { get; set; } = default!;

        [ForeignKey("payment_provider_id")]
        [Column("payment_provider_id")]
        [Comment("Id do fornecedor do terminal")]
        public int PaymentProviderId { get; set; }
        public virtual PaymentProvider? PaymentProvider { get; set; }

        [ForeignKey("operation_id")]
        [Column("operation_id")]
        [Comment("Id da operação")]
        public Guid OperationId { get; set; }
        public virtual Operation? Operation { get; set; }

    }
}
