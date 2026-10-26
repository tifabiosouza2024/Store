using Microsoft.EntityFrameworkCore;
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
    [Comment("Relacionamento entre Operação, Usuário e seus papeis")]
    public class OperationUserRole : BaseEntity<long>
    {
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
    }
}
