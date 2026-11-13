using FS.Store.Model.Elements;
using FS.Store.Model.Entity;
using Microsoft.EntityFrameworkCore.Query;

namespace FS.Store.Core.Interfaces
{
    public interface ICashRegisterSessionService : IBaseService<CashRegisterSession, long>
    {
        Task<Pagination<CashRegisterSession>> GetAllPagedAsync(int page, int qttPerPage, Dictionary<string, string>? dynamicSorts = null
            , Func<IQueryable<CashRegisterSession>, IQueryable<CashRegisterSession>>? orderBy = null);

        Task<CashRegisterSession?> GetByIdAsync(long id
            , Func<IQueryable<CashRegisterSession>, IIncludableQueryable<CashRegisterSession, object?>>? includeProperties = null);
        Task<bool> AlreadyOpened();
        Task<IEnumerable<CashRegisterSession>> GetCashRegisterSessionOpenedAsync(Dictionary<string, string>? dynamicSorts = null);
        Task<Pagination<CashRegisterSession>> GetCashRegisterSessionOpenedPagedAsync(int page, int qttPerPage, Dictionary<string, string>? dynamicSorts = null);
        Task<CashRegisterSession?> GetToClose(long? id
            , Func<IQueryable<CashRegisterSession>, IIncludableQueryable<CashRegisterSession, object?>>? includeProperties = null);

        Task<int> Count();
    }
}
