using FS.Store.Model.Elements;
using FS.Store.Model.Entity;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashSessionIndexViewModel
    {
        public Pagination<CashRegisterSession>? CashRegisterSessionsOpened { get; set; }
    }
}
