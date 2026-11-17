using System.ComponentModel.DataAnnotations;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Insira a senha atual para validação")]
        public string CurrentPassword { get; set; } = null!;

        [Required(ErrorMessage = "Insira a nova senha")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 100 caracteres")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Confirme a nova senha")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 100 caracteres")]
        [Compare(nameof(NewPassword), ErrorMessage = "Ase senhas não coincidem")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
