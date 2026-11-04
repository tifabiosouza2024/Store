using FS.Store.Model.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashRegisterSessionEditViewModel
    {
        public long Id { get; set; }

        [Required]
        public Guid OperationId { get; set; }

        public IEnumerable<SelectListItem> Operations { get; set; } = [];

        [Required]
        public int CashSessionNumber { get; set; }

        [Required]
        public decimal OpeningAmount { get; set; }

        [Required]
        public DateTime OpenedAt { get; set; }

        public DateTime? ClosedAt { get; set; }

        [Required]
        public CashRegisterStatusEnum CashRegisterStatus { get; set; }

        public IEnumerable<SelectListItem> StatusOptions { get; set; } = [];

        // Indica se a sessão já estava fechada antes da edição.
        public bool WasClosed { get; set; }

        // Dados informativos
        public string? OpenedByUserName { get; set; }

        public string? ClosedByUserName { get; set; }

        public string? ChangedByUserName { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Exclusão virtual
        public bool Deleted { get; set; }
    }
}
