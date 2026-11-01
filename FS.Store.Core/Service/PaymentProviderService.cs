using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.Core.Service
{
    public class PaymentProviderService(IPaymentProviderRepository repository) : BaseService<PaymentProvider, int>(repository), IPaymentProviderService
    {
    }
}
