namespace FS.Store.WebAPI.Helpers
{
    using FS.Store.Core.Extension;
    using FS.Store.Core.Helper;
    using FS.Store.Core.Interfaces;
    using FS.Store.Model.Entity;
    using FS.Store.Model.Enums;
    using FS.Store.WebAPI.Areas.Admin.ViewModels;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using System.Security.Claims;

    public class AuthHelper(PasswordHelper passwordHelper
        , CurrentUserHelper currentUserHelper
        , IHttpContextAccessor httpContextAccessor
        , IUserService userService
        , IOperationService operationService
        , ILogger<AuthHelper> logger)
    {
        public async Task<bool> LoginAsync(User? user, LoginViewModel model)
        {
            if (user == null || !user.IsActive || !passwordHelper.IsCorrectPassword(user, model.Password))
                return false;

            user!.RememberMe = model.RememberMe;

            if (user.GlobalRoles != null && user.GlobalRoles.Any(o => o.IsActive && o.Status == RoleStatusEnum.Active))
            {
                await AssignPermissionsAsync(user, null);
            }
            else
            {
                if (user.Operations?.Count(o => o.IsActive && o.Status == RoleStatusEnum.Active) > 1)
                {
                    user.LastOperationId = null;
                }
                else
                {
                    user.LastOperationId = user.Operations?.FirstOrDefault(o => o.IsActive && o.Status == RoleStatusEnum.Active)?.OperationId;
                }

                await AssignPermissionsAsync(user, user?.LastOperationId);
            }

            return true;
        }

        public async Task AssignPermissionsAsync(User? user, Guid? operationId)
        {
            if (user == null)
                return;

            List<Claim> claims = [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name ?? "Usuário"),
                new(ClaimTypes.Email, user.Email)
            ];

            var operation = await operationService.FindByIdAsync(operationId.GetValueOrDefault());
            if (operation != null)
            {
                claims.Add(new(ClaimTypesExtensions.OperationId, operation.Id.ToString()));
                claims.Add(new(ClaimTypesExtensions.OperationName, operation.Name.ToString()));
            }

            var globalRoles = user.GlobalRoles?.Where(o => o.IsActive && o.Status == RoleStatusEnum.Active).ToList() ?? [];
            if (operationId != null && operationId != Guid.Empty)
            {
                var operationRoles = user.Operations?.Where(o => o.OperationId == operationId && o.IsActive && o.Status == RoleStatusEnum.Active) ?? [];
                foreach (var operationRole in operationRoles)
                    if (operationRole.Role is not null)
                        claims.Add(new Claim(ClaimTypes.Role, operationRole.Role.NormalizedName));
            }
            else if (globalRoles.Any())
            {
                foreach (var globalRole in globalRoles)
                    if (globalRole.Role is not null)
                        claims.Add(new Claim(ClaimTypes.Role, globalRole.Role.NormalizedName));
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

            await userService.SaveChangesAsync();

        }
    }
}
