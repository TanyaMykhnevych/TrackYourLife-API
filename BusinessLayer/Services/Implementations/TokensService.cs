using BusinessLayer.Models;
using BusinessLayer.Models.DTOs;
using BusinessLayer.Services.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BusinessLayer.Services.Implementations
{
    public class TokensService : ITokensService
    {
        public GeneratedTokenDTO GenerateTokenForIdentity(ClaimsIdentity identity, TimeSpan expires)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(expires),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new GeneratedTokenDTO
            {
                AccessToken = encodedJwt,
                Username = identity.Name
            };
        }

    }
}
