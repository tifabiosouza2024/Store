using FS.Store.Model.Entity;

namespace FS.Store.Core.Interfaces
{
    public interface ICashMovementService : IBaseService<CashMovement, long>
    {
        //Task<IEnumerable<CashMovement>?> GetAllAsync(long cashSessionId
        //    , CashMovementTypeEnum? type = null
        //    , PeriodDto? period = null);
    }
}
