using FS.Store.Core.Extension;
using FS.Store.Core.Helper;
using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;
using System.Security.Claims;

namespace FS.Store.Core.Service
{
    public class RoleService(IRoleRepository repository
        , CurrentUserHelper currentUserHelper) : BaseService<Role, int>(repository), IRoleService
    {

        public async Task<IEnumerable<Role>?> GetAssignableRolesAsync()
        {
            var userLevel = GetUserLevel(currentUserHelper.User!);

            var roles = await GetAllAsync();

            var result = roles?.Where(role =>
                RoleHierarchy.TryGetValue(role.NormalizedName, out var roleLevel) &&
                roleLevel <= userLevel);

            return result;
        }

        private static readonly Dictionary<string, int> RoleHierarchy = new()
        {
            [ClaimTypesExtensions.RoleSuper] = 4,
            [ClaimTypesExtensions.RoleAdmin] = 3,
            [ClaimTypesExtensions.RoleManager] = 2,
            [ClaimTypesExtensions.RoleSeller] = 1
        };

        private static int GetUserLevel(ClaimsPrincipal user)
        {
            var result = user.GetRoles()
                .Where(RoleHierarchy.ContainsKey)
                .Select(role => RoleHierarchy[role])
                .DefaultIfEmpty(0)
                .Max();

            return result;
        }
    }
}
