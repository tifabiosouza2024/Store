using FS.Store.Core.Extension;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FS.Store.Core.Helper
{
    public class CurrentUserHelper(IHttpContextAccessor httpContextAccessor)
    {
        public Guid? UserId
            => User?.GetUserId();

        public bool IsAuthenticated
            => User?.Identity?.IsAuthenticated ?? false;

        public Guid? CurrentOperationId
            => User?.GetOperationId();
        private ClaimsPrincipal? User
            => httpContextAccessor.HttpContext.User;
    }
}
