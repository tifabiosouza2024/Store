using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FS.Store.Model.Entity
{
    [Table("users")]
    public class User : BaseEntity<Guid>
    {
        [Column("email")]
        [MaxLength(255)]
        [NotNull]
        public string Email { get; set; } = default!;

        [Column("password")]
        [MaxLength(255)]
        [NotNull]
        public string Password { get; set; } = default!;

        [Column("is_active")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [Column("is_beta")]
        [DefaultValue(false)]
        public bool IsBeta { get; set; }

        [Column("refresh_token")]
        [MaxLength(255)]
        public string? RefreshToken { get; set; }

        [Column("dt_expiration_refresh_token", TypeName = "datetime")]
        [DataType(DataType.DateTime)]
        public DateTime? DtExpirationRefreshToken { get; set; }

        public virtual ICollection<Role>? Roles { get; set; }
    }
}
