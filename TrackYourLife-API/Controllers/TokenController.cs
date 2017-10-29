using BusinessLayer.Models;
using BusinessLayer.Services.Abstractions;
using DataLayer.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TrackYourLife.API.Extensions;

namespace TrackYourLife.API.Controllers
{
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokensService _tokensService;
        private readonly UserManager<AppUser> _userManager;

        public TokenController(
            ITokensService tokensService,
            UserManager<AppUser> userManager)
        {
            _tokensService = tokensService;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task Token()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];


            AppUser user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                Response.StatusCode = 401;
                await Response.WriteAsync("Invalid username.");
                return;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
            {
                Response.StatusCode = 401;
                await Response.WriteAsync("Invalid password.");
                return;
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);
            ClaimsIdentity identity = user.GetIdentity(string.Join(", ", roles));
            var accessToken = _tokensService.GenerateTokenForIdentity(identity, TimeSpan.FromMinutes(AuthOptions.LIFETIME));

            Response.ContentType = "application/json";
            string serializedResponse = JsonConvert.SerializeObject(accessToken, new JsonSerializerSettings { Formatting = Formatting.Indented });
            await Response.WriteAsync(serializedResponse);
        }
    }
}
