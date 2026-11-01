using FS.Store.Core.Exceptions;
using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.Core.Service
{
    public class PaymentTerminalService(IPaymentTerminalRepository repository
        , CurrentUserHelper currentUserHelper) : BaseService<PaymentTerminal, Guid>(repository), IPaymentTerminalService
    {
        public async Task<PaymentTerminal> AddOrUpdateAsync(PaymentTerminal terminal)
        {
            if (currentUserHelper.CurrentOperationId == Guid.Empty)
                throw new ServiceException("É necessário selecionar uma operação para continuar.");

            terminal.OperationId = currentUserHelper.CurrentOperationId.GetValueOrDefault();

            await base.AddOrUpdateAsync(terminal);

            return terminal;
        }

        public async Task<IEnumerable<PaymentTerminal>?> GetAllAsync()
            => await GetAllAsync(pt => pt.OperationId == currentUserHelper.CurrentOperationId);
    }
}
