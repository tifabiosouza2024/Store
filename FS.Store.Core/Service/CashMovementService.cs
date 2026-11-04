using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.Core.Service
{
    public class CashMovementService(ICashMovementRepository repository
        , CurrentUserHelper currentUserHelper) : BaseService<CashMovement, long>(repository), ICashMovementService
    {
        //public async Task<IEnumerable<CashMovement>?> GetAllAsync(long cashSessionId, CashMovementTypeEnum? type = null, PeriodDto? period = null)
        //{
        //    var operationId = currentUserHelper.CurrentOperationId;
        //    DateTime? startDate = period?.StartDate;
        //    DateTime? endDate = period?.EndDate;

        //    var cashMovements = await base.GetAllAsync(cm => cm.CashRegisterSessionId == cashSessionId
        //        && operationId == cm.CashRegisterSession!.OperationId
        //        && (type == null || cm.CashMovementType == type)
        //        && (startDate == null || cm.OccurredAt >= startDate)
        //        && (endDate == null || cm.OccurredAt <= endDate)
        //        , includeProperties: q => q.Include(cm => cm.CreatedByUser));

        //    return cashMovements;
        //}
    }
}
