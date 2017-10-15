using BusinessLayer.Models;
using BusinessLayer.Services.Abstractions;
using DataLayer.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrackYourLife.API.Extensions;

namespace TrackYourLife.API.Controllers
{
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokensService _tokensService;
        private readonly IUsersService _usersService;

        public TokenController(
            ITokensService tokensService,
            IUsersService usersService)
        {
            _tokensService = tokensService;
            _usersService = usersService;
        }


        [HttpPost]
        public async Task Token()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            User person = await _usersService.GetUserByCredentialsAsync(username, password);
            if (person == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            };

            ClaimsIdentity identity = person.GetIdentity();
            var accessToken = _tokensService.GenerateTokenForIdentity(identity, TimeSpan.FromMinutes(AuthOptions.LIFETIME));

            Response.ContentType = "application/json";
            string serializedResponse = JsonConvert.SerializeObject(accessToken, new JsonSerializerSettings { Formatting = Formatting.Indented });
            await Response.WriteAsync(serializedResponse);
        }
    }
}
