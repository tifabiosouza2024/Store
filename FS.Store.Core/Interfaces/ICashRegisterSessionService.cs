using FS.Store.Model.Entity;

namespace FS.Store.Core.Interfaces
{
    public interface ICashRegisterSessionService : IBaseService<CashRegisterSession, long>
    {
        Task<bool> AlreadyOpened(Guid operationId);
        Task<IEnumerable<CashRegisterSession>> GetCashRegisterSessionOpenedAsync(Guid operationId);
    }
}
