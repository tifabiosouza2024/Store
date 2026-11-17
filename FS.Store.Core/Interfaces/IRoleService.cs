using FS.Store.Model.Entity;

namespace FS.Store.Core.Interfaces
{
    public interface IRoleService : IBaseService<Role, int>
    {
        Task<IEnumerable<Role>?> GetAssignableRolesAsync();
    }
}
