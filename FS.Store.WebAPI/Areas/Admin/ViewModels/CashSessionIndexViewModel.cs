using FS.Store.Model.Entity;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashSessionIndexViewModel
    {
        public IEnumerable<CashRegisterSession>? CashRegisterSessionsOpened { get; set; }
    }
}
