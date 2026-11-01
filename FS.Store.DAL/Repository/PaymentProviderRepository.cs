using FS.Store.DAL.Context;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.DAL.Repository
{
    public class PaymentProviderRepository(AppDbContext dbcontext) : BaseRepository<PaymentProvider, int>(dbcontext), IPaymentProviderRepository
    {
    }
}
