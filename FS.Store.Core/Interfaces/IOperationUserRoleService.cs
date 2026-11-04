using FS.Store.Model.Entity;

namespace FS.Store.Core.Interfaces
{
    public interface IOperationUserRoleService : IBaseService<OperationUserRole, long>
    {
        Task<IEnumerable<Operation>?> FindAllOperationsByUserIdAsync();
    }
}
