using FS.Store.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{
    /// <summary>
    /// Relacionamento entre Operação x Usuário x Papeis de acesso ao sistema
    /// <para>
    /// TypeId: long
    /// </para>
    /// </summary>
    [Table("global_user_roles")]
    [Comment("Relacionamento entre usuário do sistema e suas funções")]
    public class GlobalUserRole : BaseEntity<long>
    {
        [Column("is_active")]
        [DefaultValue(true)]
        [Comment("Determina se o usuário estará ativo para operação do sistema.")]
        public bool IsActive { get; set; }

        [Column("status")]
        [Comment("Status da função do usuário no sistema")]
        public RoleStatusEnum Status { get; set; } = RoleStatusEnum.Pending;

        [Column("invited_at", TypeName = "datetime(0)")]
        [Comment("Data do convite")]
        public DateTime InvitedAt { get; set; }

        [Column("accepted_at", TypeName = "datetime(0)")]
        [Comment("Data de aceitação do convite")]
        public DateTime? AcceptedAt { get; set; }

        [ForeignKey("user_id")]
        [Column("user_id")]
        [Comment("Id do Usuário")]
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey("role_id")]
        [Column("role_id")]
        [Comment("Id do papel do Usuário na Operação")]
        public int RoleId { get; set; }
        public virtual GlobalRole? Role { get; set; }

        [Column("invited_by_user_id")]
        [ForeignKey("invited_by_user_id")]
        [Comment("Usuário responsável pelo convite")]
        public Guid? InvitedByUserId { get; set; }
        public virtual User? InvitedByUser { get; set; }
    }
}
