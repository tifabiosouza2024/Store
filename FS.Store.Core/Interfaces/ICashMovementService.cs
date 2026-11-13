using FS.Store.Model.Entity;
using FS.Store.Model.Enums;

namespace FS.Store.Core.Interfaces
{
    public interface ICashMovementService : IBaseService<CashMovement, long>
    {
        Task<IEnumerable<CashMovement>?> GetAllByCashSessionIdAsync(long cashSessionId
            , CashMovementTypeEnum? type = null);
    }
}
