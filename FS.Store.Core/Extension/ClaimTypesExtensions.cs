using System.Security.Claims;

namespace FS.Store.Core.Extension
{
    public static class ClaimTypesExtensions
    {
        public const string OperationId = "OperationId";
        public const string OperationName = "OperationName";

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
    }
}
