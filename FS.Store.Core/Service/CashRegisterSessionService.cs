using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Elements;
using FS.Store.Model.Entity;
using FS.Store.Model.Enums;
using Microsoft.EntityFrameworkCore.Query;

namespace FS.Store.Core.Service
{
    public class CashRegisterSessionService(ICashRegisterSessionRepository repository
        , CurrentUserHelper currentUserHelper) : BaseService<CashRegisterSession, long>(repository), ICashRegisterSessionService
    {
        public async Task<IEnumerable<CashRegisterSession>> GetCashRegisterSessionOpenedAsync(Dictionary<string, string>? dynamicSorts = null)
        {
            var result = await base.GetAllAsync(filter: c
                => c.OperationId == currentUserHelper.CurrentOperationId
                && c.CashRegisterStatus == CashRegisterStatusEnum.Open
                , dynamicSorts: dynamicSorts);

            return result!;
        }
        public async Task<Pagination<CashRegisterSession>> GetCashRegisterSessionOpenedPagedAsync(int page, int qttPerPage, Dictionary<string, string>? dynamicSorts = null)
        {
            var result = await base.GetAllPagedAsync(currentPage: page, qttPerPage: qttPerPage, filter: c
                => c.OperationId == currentUserHelper.CurrentOperationId
                && c.CashRegisterStatus == CashRegisterStatusEnum.Open
                , dynamicSorts: dynamicSorts);

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

        public async Task<Pagination<CashRegisterSession>> GetAllPagedAsync(int page, int qttPerPage, Dictionary<string, string>? dynamicSorts = null
            , Func<IQueryable<CashRegisterSession>, IQueryable<CashRegisterSession>>? orderBy = null)
            => await base.GetAllPagedAsync(currentPage: page, qttPerPage: qttPerPage, filter: c
                => c.OperationId == currentUserHelper.CurrentOperationId,
                dynamicSorts: dynamicSorts, orderBy: orderBy);

        public async Task<int> Count()
            => await base.CountAsync(c => c.OperationId == currentUserHelper.CurrentOperationId
            && c.CashRegisterStatus == CashRegisterStatusEnum.Open);
    }
}
