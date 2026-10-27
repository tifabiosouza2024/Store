using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.Core.Service
{
    public class CashRegisterSessionService(ICashRegisterSessionRepository repository) : BaseService<CashRegisterSession, long>(repository), ICashRegisterSessionService
    {
        public async Task<IEnumerable<CashRegisterSession>> GetCashRegisterSessionOpenedAsync(Guid operationId)
        {
            var result = await GetAllAsync(c
                => c.OperationId == operationId
                && c.CashRegisterStatus == Model.Enums.CashRegisterStatusEnum.Open);

            return result!;
        }

        public async Task<bool> AlreadyOpened(Guid operationId)
            => await ExistsAsync(e
                => e.OperationId == operationId
                && e.CashRegisterStatus == Model.Enums.CashRegisterStatusEnum.Open);
    }
}
