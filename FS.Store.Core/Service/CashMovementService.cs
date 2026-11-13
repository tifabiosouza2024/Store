using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;
using FS.Store.Model.Enums;
using Microsoft.EntityFrameworkCore;

namespace FS.Store.Core.Service
{
    public class CashMovementService(ICashMovementRepository repository
        , CurrentUserHelper currentUserHelper) : BaseService<CashMovement, long>(repository), ICashMovementService
    {
        public async Task<IEnumerable<CashMovement>?> GetAllByCashSessionIdAsync(long cashSessionId, CashMovementTypeEnum? type = null)
        {
            var operationId = currentUserHelper.CurrentOperationId;

            var cashMovements = await base.GetAllAsync(cm => cm.CashRegisterSessionId == cashSessionId
                && operationId == cm.CashRegisterSession!.OperationId
                && (type == null || cm.CashMovementType == type)
                , includeProperties: q => q.Include(cm => cm.CreatedByUser));

            return cashMovements;
        }
    }
}
