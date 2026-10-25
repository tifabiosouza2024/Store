using System.Security.Claims;

namespace FS.Store.WebAPI.Extensions
{
    public static class ClaimTypesExtensions
    {
        public const string Operation = "Operation";

        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            if (Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id))
                return id;

            return default;
        }

        public static Guid GetOperationId(this ClaimsPrincipal user)
        {
            if (Guid.TryParse(user.FindFirst(Operation)?.Value, out var id))
                return id;

            return default;
        }
    }
}
