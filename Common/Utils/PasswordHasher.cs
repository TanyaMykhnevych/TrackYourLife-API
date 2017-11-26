using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utils
{
    public static class PasswordHasher
    {
        public static string GetPasswordHash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            byte[] bytes = Encoding.Unicode.GetBytes(password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString.ToUpper();
        }

        public static bool IsPasswordEqualToHash(string pass, string hash)
        {
            return GetPasswordHash(pass).Equals(hash);
        }

        public static string GeneratePassword()
        {
            var x = Guid.NewGuid().ToString();
            x = x.Replace("-", "");
            return string.Join("", x.Take(8));
        }

        public static string GetStaticPassword()
        {
            return "Test123!";
        }
    }
}
