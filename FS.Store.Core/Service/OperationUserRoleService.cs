using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.Core.Service
{
    public class OperationUserRoleService(IOperationUserRoleRepository repository
        , CurrentUserHelper currentUserHelper) : BaseService<OperationUserRole, long>(repository), IOperationUserRoleService
    {
        public async Task<IEnumerable<OperationUserRole>?> FindAllByUserIdAsync()
            => await repository.GetAllAsync(o => o.UserId == currentUserHelper.UserId);
    }
}
