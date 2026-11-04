using FS.Store.Model.Attributes;
using FS.Store.Model.Enums;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashMovementItemViewModel
    {
        public int Id { get; set; }

        public DateTime OccurredAt { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        [MapFrom("CreatedByUser.Name")]
        public string? UserName { get; set; }

        public CashMovementTypeEnum CashMovementType { get; set; }
    }
}
