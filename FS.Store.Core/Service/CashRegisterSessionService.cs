using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;
using FS.Store.Model.Enums;
using Microsoft.EntityFrameworkCore.Query;

namespace FS.Store.Core.Service
{
    public class CashRegisterSessionService(ICashRegisterSessionRepository repository
        , CurrentUserHelper currentUserHelper) : BaseService<CashRegisterSession, long>(repository), ICashRegisterSessionService
    {
        public async Task<IEnumerable<CashRegisterSession>> GetCashRegisterSessionOpenedAsync()
        {
            var result = await GetAllAsync(c
                => c.OperationId == currentUserHelper.CurrentOperationId
                && c.CashRegisterStatus == CashRegisterStatusEnum.Open);

            return result!;
        }

        public async Task<bool> AlreadyOpened()
            => await ExistsAsync(e
                => e.OperationId == currentUserHelper.CurrentOperationId
                && e.CashRegisterStatus == CashRegisterStatusEnum.Open);

        public async Task<CashRegisterSession?> GetToClose(long? id
            , Func<IQueryable<CashRegisterSession>, IIncludableQueryable<CashRegisterSession, object?>>? includeProperties = null)
            => await FirstOrDefaultAsync(c
                => c.Id == id
                && c.OperationId == currentUserHelper.CurrentOperationId
                && c.CashRegisterStatus == CashRegisterStatusEnum.Open
            , includeProperties: includeProperties);

        public async Task<CashRegisterSession?> GetByIdAsync(long id
            , Func<IQueryable<CashRegisterSession>, IIncludableQueryable<CashRegisterSession, object?>>? includeProperties = null)
             => await FirstOrDefaultAsync(c
                => c.Id == id
                && c.OperationId == currentUserHelper.CurrentOperationId
                && c.CashRegisterStatus == CashRegisterStatusEnum.Open
            , includeProperties: includeProperties);
    }
}
