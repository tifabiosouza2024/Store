using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FS.Store.Model.Entity
{
    /// <summary>
    /// Armazena os fornecedores de terminais
    /// <para>
    /// TypeId: long
    /// </para>
    /// </summary>
    [Table("provider")]
    [Comment("Armazena os fornecedores dos terminais")]
    public class PaymentProvider : BaseEntity<int>
    {
        [Column("name")]
        [NotNull]
        [StringLength(50)]
        [Comment("Empresa da maquininha de cartão")]
        public string Name { get; set; } = default!;

    }
}
