using FS.Store.Core.Extension;
using FS.Store.Model.Entity;
using Microsoft.AspNetCore.Identity;

namespace FS.Store.Core.Helper
{
    public class PasswordHelper(IPasswordHasher<User> _passwordHasher)
    {
        public string HashPassword(User user, string password)
            => _passwordHasher.HashPassword(user, password);

        public bool IsCorrectPassword(User? user, string? password)
        {
            if (user == null || password.IsNullOrEmpty())
                return false;

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                password!);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
