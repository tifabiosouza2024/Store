using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Store.Model.Entity
{
    [Table("roles")]
    [Comment("Papeis de usuarios do sistema")]
    public class Role : BaseEntity<int>
    {

        [Column("name")]
        [MaxLength(255)]
        [Required]
        [Comment("Nome do papel")]
        public string Name { get; set; } = default!;

        [Column("normalized_name")]
        [Required]
        [MaxLength(255)]
        [Comment("Nome do papel normalizado (em uppercase)")]
        public string NormalizedName
        {
            get => _normalizedName;
            set => _normalizedName = value?.ToUpperInvariant() ?? default!;
        }
        private string _normalizedName = default!;
    }
}
