using DataLayer.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TrackYourLife.API.Extensions
{
    public static class UserExtensions
    {
        public static ClaimsIdentity GetIdentity(this User user)
        {
            if (user == null) return null;

            string rolesString = string.Join(", ", user.UserRoles.Select(x => x.Role.Name));
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, rolesString)
                };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
