using FS.Store.Model.Entity;

namespace FS.Store.Core.Interfaces
{
    public interface ICashRegisterSessionService : IBaseService<CashRegisterSession, long>
    {
        Task<bool> AlreadyOpened();
        Task<IEnumerable<CashRegisterSession>> GetCashRegisterSessionOpenedAsync();
        Task<CashRegisterSession?> GetToClose(long? id);
    }
}
