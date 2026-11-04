using System.Security.Claims;

namespace FS.Store.Core.Extension
{
    public static class ClaimTypesExtensions
    {
        public const string OperationId = "OperationId";
        public const string OperationName = "OperationName";
        public const string RoleSuper = "SUPER";
        public const string RoleAdmin = "ADMIN";
        public const string RoleManager = "MANAGER";
        public const string RoleSeller = "SELLER";

        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            if (Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id))
                return id;

            return default;
        }

        public static Guid GetOperationId(this ClaimsPrincipal user)
        {
            if (Guid.TryParse(user.FindFirst(OperationId)?.Value, out var id))
                return id;

            return default;
        }

        public static string GetOperationName(this ClaimsPrincipal user)
        {
            return user.FindFirst(OperationName)?.Value ?? string.Empty;
        }

        public static List<string> GetRoles(this ClaimsPrincipal user)
            => [.. user.FindAll(ClaimTypes.Role).Select(c => c.Value)];
        public static bool IsSuper(this ClaimsPrincipal user)
            => user.GetRoles().Contains(RoleSuper);
        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.GetRoles().Contains(RoleAdmin);
        public static bool IsManager(this ClaimsPrincipal user)
            => user.GetRoles().Contains(RoleManager);
        public static bool IsSeller(this ClaimsPrincipal user)
            => user.GetRoles().Contains(RoleSeller);

        public static bool IsAdminOrGreater(this ClaimsPrincipal user)
            => user.IsSuper() || user.IsAdmin();
        public static bool IsManagerOrGreater(this ClaimsPrincipal user)
            => user.IsAdminOrGreater() || user.IsManager();
        public static bool IsSellerOrGreater(this ClaimsPrincipal user)
            => user.IsManagerOrGreater() || user.IsSeller();
    }
}
