using Common.Constants;
using DataLayer.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrackYourLife.API.ViewModels;

namespace TrackYourLife.API.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        //private readonly ITokensService _tokensService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public TokenController(
            SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] GetTokenViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Username);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                          new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(ClaimTypes.Name, user.Email)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LONG_LONG_HARD_KEY_1234567890"));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                              issuer: JwtConfigConstants.Issuer,
                              claims: claims,
                              expires: model.RememberMe ? DateTime.Now.AddMonths(1) : DateTime.Now.AddDays(1),
                              signingCredentials: creds);

                        return Ok(new {
                            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                            expiresIn = 30 * 60,
                            tokenType = "Bearer"
                        });
                    }
                }

                return Unauthorized();
            }

            return BadRequest("Could not create token");
        }
    }
}
