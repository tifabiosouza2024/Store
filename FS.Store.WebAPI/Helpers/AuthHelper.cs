namespace FS.Store.WebAPI.Helpers
{
    using FS.Store.Core.Helper;
    using FS.Store.Model.Entity;
    using FS.Store.WebAPI.Areas.Admin.ViewModels;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using System.Security.Claims;

    public class AuthHelper(PasswordHelper passwordHelper
        , IHttpContextAccessor httpContextAccessor
        , ILogger<AuthHelper> logger)
    {
        public async Task<bool> LoginAsync(User? user, LoginViewModel model)
        {
            if (user == null || !user.IsActive || !passwordHelper.IsCorrectPassword(user, model.Password))
                return false;

            List<Claim> claims = [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email)
            ];

            if (user.Roles is not null)
            {
                foreach (var role in user.Roles)
                {
                    if (role.Role is not null)
                        claims.Add(new Claim(ClaimTypes.Role, role.Role.NormalizedName));
                }
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                AllowRefresh = true
            };

            var httpContext = httpContextAccessor.HttpContext ?? throw new InvalidOperationException("Não existe um contexto HTTP disponível.");

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Login realizado para o usuário {UserId}.", user.Id);

            return true;
        }
    }
}
