using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FS.Store.Model.Entity
{
    [Table("users")]
    [Comment("Usuarios do sistema")]
    public class User : BaseEntity<Guid>
    {
        [Column("email")]
        [MaxLength(255)]
        [NotNull]
        [Comment("E-mail do usuario")]
        public string Email { get; set; } = default!;

        [Column("name")]
        [MaxLength(255)]
        [NotNull]
        [Comment("Nome do usuario")]
        public string Name { get; set; } = default!;

        [Column("password_hash")]
        [MaxLength(255)]
        [NotNull]
        [Comment("Hash de senha do usuario")]
        public string PasswordHash { get; set; } = default!;

        [Column("is_active")]
        [DefaultValue(true)]
        [Comment("Usuario ativo")]
        public bool IsActive { get; set; }

        [Column("is_beta")]
        [DefaultValue(false)]
        [Comment("Usuario testador de versoes beta")]
        public bool IsBeta { get; set; }
        [Column("remember_me")]
        [Comment("Informa se o usuario permanece logado apos fechar o navegador")]
        public bool RememberMe { get; set; }

        [Column("last_operation_id")]
        [Comment("Id da ultima operacao logada")]
        public Guid? LastOperationId { get; set; }
        public virtual Operation? LastOperation { get; set; }

        public virtual ICollection<OperationUserRole>? Operations { get; set; }
    }
}
