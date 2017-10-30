using BusinessLayer.Models;
using BusinessLayer.Models.DTOs;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            var jwt = new JwtSecurityToken(new JwtHeader(new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)), new JwtPayload(identity.Claims));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new GeneratedTokenDTO
            {
                AccessToken = encodedJwt,
                TokenType = JwtBearerDefaults.AuthenticationScheme,
                ExpiresIn = (int)expires.TotalSeconds
            };
        }

    }
}
