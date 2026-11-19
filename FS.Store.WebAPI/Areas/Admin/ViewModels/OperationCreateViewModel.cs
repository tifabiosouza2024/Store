using System.ComponentModel.DataAnnotations;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{

    public class OperationCreateViewModel
    {
        [Required]
        [StringLength(18)]
        public string Document { get; set; } = default!;

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = default!;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = default!;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string ManagerEmail { get; set; } = default!;

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = default!;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = default!;
    }

}