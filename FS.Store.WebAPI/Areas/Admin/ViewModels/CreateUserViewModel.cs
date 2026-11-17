using FS.Store.Model.Entity;
using System.ComponentModel.DataAnnotations;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CreateUserViewModel
    {
        public IEnumerable<Role>? Roles { get; set; }

        public string? OperationName { get; set; }

        [Required(ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [MinLength(6, ErrorMessage = "A senha deve possuir pelo menos 6 caracteres.")]
        public string? Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione uma função.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma função válida.")]
        public int RoleId { get; set; }
    }
}
