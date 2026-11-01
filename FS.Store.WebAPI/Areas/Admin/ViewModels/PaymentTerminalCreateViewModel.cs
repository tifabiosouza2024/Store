using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class PaymentTerminalCreateViewModel
    {
        public string? OperationName { get; set; }

        [Required(ErrorMessage = "Informe o nome do terminal.")]
        [StringLength(50)]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Selecione o fornecedor.")]
        public int PaymentProviderId { get; set; }

        public IEnumerable<SelectListItem>? PaymentProviders { get; set; }
    }
}
