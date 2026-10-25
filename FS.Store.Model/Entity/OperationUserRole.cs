using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{
    [Table("operation_user_role")]
    [Comment("Relacionamento entre operacao, usuario e seus papeis")]
    public class OperationUserRole : BaseEntity<long>
    {
        [ForeignKey("operation_id")]
        [Column("operation_id")]
        [Comment("Id da operacao")]
        public Guid OperationId { get; set; }
        public virtual Operation? Operation { get; set; }

        [ForeignKey("user_id")]
        [Column("user_id")]
        [Comment("Id do usuario")]
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey("role_id")]
        [Column("role_id")]
        [Comment("Id da funcao de acesso")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
