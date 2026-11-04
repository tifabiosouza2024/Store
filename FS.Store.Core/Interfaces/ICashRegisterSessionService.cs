using FS.Store.Model.Entity;
using Microsoft.EntityFrameworkCore.Query;

namespace FS.Store.Core.Interfaces
{
    public interface ICashRegisterSessionService : IBaseService<CashRegisterSession, long>
    {
        Task<CashRegisterSession?> GetByIdAsync(long id
            , Func<IQueryable<CashRegisterSession>, IIncludableQueryable<CashRegisterSession, object?>>? includeProperties = null);
        Task<bool> AlreadyOpened();
        Task<IEnumerable<CashRegisterSession>> GetCashRegisterSessionOpenedAsync();
        Task<CashRegisterSession?> GetToClose(long? id
            , Func<IQueryable<CashRegisterSession>, IIncludableQueryable<CashRegisterSession, object?>>? includeProperties = null);
    }
}
