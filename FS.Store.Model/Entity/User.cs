using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FS.Store.Model.Entity
{
    /// <summary>
    /// Usuarios do sistema
    /// <para>
    /// TypeId: Guid
    /// </para>
    /// </summary>
    [Table("users")]
    [Comment("Usuários do sistema")]
    public class User : BaseEntity<Guid>
    {
        [Column("email")]
        [MaxLength(255)]
        [NotNull]
        [Comment("E-mail do usuário")]
        public string Email { get; set; } = default!;

        [Column("name")]
        [MaxLength(255)]
        [NotNull]
        [Comment("Nome do usuário")]
        public string Name { get; set; } = default!;

        [Column("password_hash")]
        [MaxLength(255)]
        [NotNull]
        [Comment("Hash de senha")]
        public string PasswordHash { get; set; } = default!;

        [Column("is_active")]
        [DefaultValue(true)]
        [Comment("Usuário ativo")]
        public bool IsActive { get; set; }

        [Column("is_beta")]
        [DefaultValue(false)]
        [Comment("Usuário testador de versões beta")]
        public bool IsBeta { get; set; }
        [Column("remember_me")]
        [Comment("Informa se o usuário permanece logado após fechar o navegador")]
        public bool RememberMe { get; set; }

        [Column("last_operation_id")]
        [Comment("Id da última operação logada")]
        public Guid? LastOperationId { get; set; }
        public virtual Operation? LastOperation { get; set; }

        public virtual ICollection<OperationUserRole>? Operations { get; set; }
    }
}
