using FS.Store.Model.Entity;

namespace FS.Store.Core.Interfaces
{
    public interface IPaymentTerminalService : IBaseService<PaymentTerminal, Guid>
    {
        Task<IEnumerable<PaymentTerminal>?> GetAllAsync();
        Task<PaymentTerminal> AddOrUpdateAsync(PaymentTerminal terminal);
    }
}
