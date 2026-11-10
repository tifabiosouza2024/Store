using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FS.Store.Model.Enums
{
    /// <summary>
    /// Status de caixa, sendo:<br />
    /// 1. aberto<br />
    /// 2. fechado<br />
    /// 3. cancelado<br />
    /// </summary>
    public enum CashRegisterStatusEnum
    {
        [Comment("Aberto")]
        Open = 1,
        [Comment("Fechado")]
        Closed = 2,
        [Comment("Cancelado")]
        Canceled = 3
    }

    public static class CashRegisterStatusEnumExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItemsInEditView(this CashRegisterStatusEnum currentStatus)
        {
            var options = new List<SelectListItem>();

            switch ((int)currentStatus)
            {
                case 1: // Aberto
                    options.Add(new SelectListItem("Aberto", "1"));
                    options.Add(new SelectListItem("Cancelado", "3"));
                    break;

                case 2: // Fechado
                    options.Add(new SelectListItem("Fechado", "2"));
                    options.Add(new SelectListItem("Aberto", "1"));
                    break;

                case 3: // Cancelado
                    options.Add(new SelectListItem("Cancelado", "3"));
                    break;
            }

            return options;
        }
    }
}
