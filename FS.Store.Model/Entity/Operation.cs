using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FS.Store.Model.Entity
{
    [Table("operations")]
    [Comment("Empresa/Loja Operante")]
    public class Operation : BaseEntity<Guid>
    {
        [Column("document")]
        [Comment("CNPJ da operacao")]
        [StringLength(18)]
        public string Document { get; set; } = default!;

        [StringLength(255)]
        [Comment("Nome da operacao")]
        [Column("name")]
        public string Name { get; set; } = default!;

        [StringLength(255)]
        [Comment("E-mail da operacao")]
        [Column("email")]
        public string Email { get; set; } = default!;

        [Column("is_active")]
        [DefaultValue(true)]
        [NotNull]
        [Comment("Operacao ativa")]
        public bool? IsActive { get; set; }

        public virtual ICollection<OperationUserRole>? UsersRoles { get; set; }
    }
}
