using FS.Store.Model.Elements;
using FS.Store.Model.Entity;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashSessionHistoryViewModel
    {
        public Pagination<CashRegisterSession>? Pagination { get; set; }
    }
}
