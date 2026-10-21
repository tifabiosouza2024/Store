//using FS.Store.Core.Extension;

//namespace FS.Store.Core.Utils
//{
//    public static class PasswordUtils
//    {
//        public static string? GenerateHashPassword(string? password)
//        {
//            if (string.IsNullOrEmpty(password))
//                return null;
//            int workfactor = 10; // 2 ^ (10) = 1024 iterations.

//            string salt = BCrypt.Net.BCrypt.GenerateSalt(workfactor);
//            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

//            return hash;
//        }

//        public static bool PasswordCompare(string? hash, string? password)
//        {
//            try
//            {
//                return BCrypt.Net.BCrypt.Verify(password, hash);
//            }
//            catch (ArgumentException)
//            {
//                throw new SaltParseException();
//            }

//        }

//        public static bool IsValidPassword(string newPassword)
//        {
//            return string.IsNotNullAndNotEmpty(newPassword) && newPassword.Length >= 6 && newPassword.Length <= 20;
//        }
//    }
//}
