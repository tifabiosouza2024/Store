using FS.Store.Model.Entity;
using Microsoft.AspNetCore.Identity;

namespace FS.Store.DAL.Helper
{
    public class PasswordHelper(IPasswordHasher<User> _passwordHasher)
    {
        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(
            User user,
            string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                password);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
