using BusinessLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Services.Abstractions
{
    public interface ITokensService
    {
        GeneratedTokenDTO GenerateTokenForIdentity(ClaimsIdentity identity, TimeSpan expires);
    }
}
