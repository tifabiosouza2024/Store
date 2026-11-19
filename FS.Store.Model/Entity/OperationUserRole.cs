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
    [Table("operation_user_role")]
    [Comment("Relacionamento entre Operação, Usuário e seus papeis na operação")]
    public class OperationUserRole : BaseEntity<long>
    {
        [Column("is_active")]
        [DefaultValue(true)]
        [Comment("Determina se o usuário estará ativo para aquela operação.")]
        public bool IsActive { get; set; }

        [Column("status")]
        [Comment("Status do vínculo do usuário com a operação")]
        public RoleStatusEnum Status { get; set; } = RoleStatusEnum.Pending;

        [Column("invited_at", TypeName = "datetime(0)")]
        [Comment("Data do convite")]
        public DateTime InvitedAt { get; set; }

        [Column("accepted_at", TypeName = "datetime(0)")]
        [Comment("Data de aceitação do convite")]
        public DateTime? AcceptedAt { get; set; }

        [ForeignKey("operation_id")]
        [Column("operation_id")]
        [Comment("Id da Operação")]
        public Guid OperationId { get; set; }
        public virtual Operation? Operation { get; set; }

        [ForeignKey("user_id")]
        [Column("user_id")]
        [Comment("Id do Usuário")]
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey("role_id")]
        [Column("role_id")]
        [Comment("Id do papel do Usuário na Operação")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }

        [Column("invited_by_user_id")]
        [ForeignKey("invited_by_user_id")]
        [Comment("Usuário responsável pelo convite")]
        public Guid? InvitedByUserId { get; set; }
        public virtual User? InvitedByUser { get; set; }
    }
}
