using FS.Store.Core.Helper;
using FS.Store.Core.Utils;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FS.Store.WebAPI.Helpers
{
    public class AuthHelper(PasswordHelper passwordHelper
        , IUserRepository userRepository
        , IConfiguration _config
        , CookieHelper cookieHelper
        , ILogger<AuthHelper> logger)
    {
        public string CreateToken(User user, int? minutes = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email!)
                ];

            foreach (var role in user.Roles)
            {
                claims.Add(new(ClaimTypes.Role, role.Role.NormalizedName));
            }

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(minutes ?? 60),
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> LoginAsync(User? user, string? password)
        {
            if (user != null && passwordHelper.IsCorrectPassword(user, password))
            {
                string token = CreateToken(user);
                if (logger.IsEnabled(LogLevel.Debug))
                    logger.LogInformation("Token JWT criado para o usuario: [{T}]", token);

                if (user.DtExpirationRefreshToken == null || user.DtExpirationRefreshToken.Expired() || user.RefreshToken is null)
                {
                    user.RefreshToken = GenerateRefreshToken();
                }
                user.DtExpirationRefreshToken = DateTime.UtcNow.AddMinutes(70);
                await userRepository.AddOrUpdateAsync(user);
                cookieHelper.AppendAuthToken(token, user.RefreshToken);
                return true;
            }
            return false;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
