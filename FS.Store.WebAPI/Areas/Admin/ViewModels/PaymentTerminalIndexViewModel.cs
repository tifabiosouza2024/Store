using FS.Store.Model.Entity;

namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class PaymentTerminalIndexViewModel
    {
        public IEnumerable<PaymentTerminal>? PaymentTerminals { get; set; }
    }
}
