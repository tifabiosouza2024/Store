using FS.Store.Model.Entity;
using FS.Store.Model.Enums;

namespace FS.Store.Core.Interfaces
{
    public interface IOperationUserRoleService : IBaseService<OperationUserRole, long>
    {
        Task<IEnumerable<OperationUserRole>?> FindAllOperationsByUserIdAsync(OperationUserRoleStatusEnum? status = null);
    }
}
