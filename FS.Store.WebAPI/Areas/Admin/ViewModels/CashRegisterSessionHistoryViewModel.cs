using FS.Store.Model.Elements;
using FS.Store.Model.Entity;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class CashRegisterSessionHistoryViewModel
    {
        public Pagination<CashRegisterSession> Pagination { get; set; } = null!;
    }
}
