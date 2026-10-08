using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{
    [Table("roles")]
    public class Role : BaseEntity<int>
    {

        [Column("name")]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; } = default!;

        [Column("normalized_name")]
        [Required]
        [MaxLength(255)]
        public string NormalizedName { get; set; } = default!;

        public virtual ICollection<User> Users { get; set; } = default!;
    }
}
