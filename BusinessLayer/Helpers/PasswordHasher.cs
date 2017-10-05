using System;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Helpers
{
    public static class PasswordHasher
    {
        public static string GetPasswordHash(string password)
        {
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
    }
}
