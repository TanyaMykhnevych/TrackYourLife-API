using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "TrackYourLife-TokenIssuer";

        /// <summary>
        /// Domain name which will use this auth service
        /// </summary>
        public const string AUDIENCE = "http://localhost:51884/"; // потребитель токена

        /// <summary>
        /// Encryption Key
        /// </summary>
        const string KEY = "2B5C9CB6-5787-4BE1-8550-965D06964445";

        /// <summary>
        /// Token Life-time - 1 minute
        /// </summary>
        public const int LIFETIME = 10;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
