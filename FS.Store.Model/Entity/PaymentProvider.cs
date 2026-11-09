using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FS.Store.Model.Entity
{
    /// <summary>
    /// Armazena os fornecedores de terminais
    /// <para>
    /// TypeId: int
    /// </para>
    /// </summary>
    [Table("payment_provider")]
    [Comment("Armazena os fornecedores dos terminais")]
    public class PaymentProvider : BaseEntity<int>
    {
        [Column("name")]
        [NotNull]
        [StringLength(50)]
        [Comment("Empresa da maquininha de cartão")]
        public string Name { get; set; } = default!;

        [Column("logo_icon")]
        [StringLength(255)]
        [Comment("Ícone do logo do fornecedor")]
        public string? LogoIcon { get; set; }
    }
}
