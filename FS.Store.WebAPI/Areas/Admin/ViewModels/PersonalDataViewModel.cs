using System.ComponentModel.DataAnnotations;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class PersonalDataViewModel
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter no máximo {1} caracteres.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo E-mail deve ser um endereço de e-mail válido.")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O campo E-mail deve ter no máximo {1} caracteres.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Insira a senha atual para validação dos dados.")]
        public string? Password { get; set; }
    }
}
