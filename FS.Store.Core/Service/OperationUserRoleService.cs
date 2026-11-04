using FS.Store.Core.Exceptions;
using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;

namespace FS.Store.Core.Service
{
    public class OperationUserRoleService(IOperationUserRoleRepository repository
        , CurrentUserHelper currentUserHelper) : BaseService<OperationUserRole, long>(repository), IOperationUserRoleService
    {
        public async Task<IEnumerable<Operation>?> FindAllOperationsByUserIdAsync()
        {
            var our = await repository.GetAllAsync(o => o.UserId == currentUserHelper.UserId);
            ServiceException.ThrowIf(our is null || !our.Any(), "Nenhum registro encontrado para o usuário logado.");

            return [.. our?.Select(o => o.Operation)!];
        }
    }
}
