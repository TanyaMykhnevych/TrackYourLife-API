using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities.Identity;
using TrackYourLife.API.ViewModels;

namespace TrackYourLife.API.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserInfo()
        {
            var response = await ContentExecuteAsync<UserInfoViewModel>(async () =>
            {
                string username = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);

                var roleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                var role = await _roleManager.FindByNameAsync(roleName);

                var claims = await _roleManager.GetClaimsAsync(role);
                var claimValues = claims.Select(x => Convert.ToInt32(x.Value));

                return new UserInfoViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    RoleName = roleName,
                    Claims = claimValues.ToArray()
                };
            });
            return Json(response);
        }
    }
}
