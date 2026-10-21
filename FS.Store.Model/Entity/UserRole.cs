using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{
    [Table("user_roles")]
    public class UserRole : BaseEntity<long>
    {
        public virtual User User { get; set; }

        [ForeignKey("user_id")]
        [Column("user_id")]
        public Guid UserId { get; set; }

        public virtual Role Role { get; set; }
        [ForeignKey("role_id")]
        [Column("role_id")]
        public int RoleId { get; set; }
    }
}
