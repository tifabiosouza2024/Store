using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{
    public abstract class BaseEntity<IdType>
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual IdType Id { get; set; } = default!;

        [Column("created_at", TypeName = "datetime(0)")]
        [Comment("Data de criação do registro")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at", TypeName = "datetime(0)")]
        [Comment("Data de atualização do registro")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        [Column("deleted")]
        [DefaultValue(false)]
        [Comment("registro deletado virtualmente")]
        public bool Deleted { get; set; }
    }
}
