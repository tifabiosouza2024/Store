namespace FS.Store.WebAPI.Helpers
{
    using FS.Store.Core.Helper;
    using FS.Store.Core.Interfaces;
    using FS.Store.Model.Entity;
    using FS.Store.WebAPI.Areas.Admin.ViewModels;
    using FS.Store.WebAPI.Extensions;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using System.Security.Claims;

    public class AuthHelper(PasswordHelper passwordHelper
        , IHttpContextAccessor httpContextAccessor
        , IUserService userService
        , ILogger<AuthHelper> logger)
    {
        public async Task<bool> LoginAsync(User? user, LoginViewModel model)
        {
            if (user == null || !user.IsActive || !passwordHelper.IsCorrectPassword(user, model.Password))
                return false;

            if (user.LastOperationId is null && 1 == user.Operations?.Count)
                user.LastOperationId = user.Operations?.First().OperationId;

            await AssignPermissionsAsync(user, user.LastOperationId);

            user.RememberMe = model.RememberMe;
            await userService.AddOrUpdateAsync(user);

            return true;
        }

        public async Task AssignPermissionsAsync(User user, Guid? operationId)
        {
            List<Claim> claims = [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email)
            ];

            if (operationId.GetValueOrDefault() != Guid.Empty)
                claims.Add(new(ClaimTypesExtensions.Operation, operationId.GetValueOrDefault().ToString()));

            var operationRoles = user.Operations?.Where(o => o.OperationId == operationId);
            if (operationRoles is not null)
            {
                foreach (var operationRole in operationRoles)
                {
                    if (operationRole.Role is not null)
                        claims.Add(new Claim(ClaimTypes.Role, operationRole.Role.NormalizedName));
                }
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = user.RememberMe,
                AllowRefresh = true
            };

            var httpContext = httpContextAccessor.HttpContext ?? throw new InvalidOperationException("Não existe um contexto HTTP disponível.");

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Login realizado para o usuário {UserId}.", user.Id);

        }
    }
}
