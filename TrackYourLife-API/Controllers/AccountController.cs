using DataLayer.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackYourLife.API.ViewModels;
using TrackYourLife.API.ViewModels.Common;

namespace TrackYourLife.API.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            string username = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            var roleNames = await _userManager.GetRolesAsync(user);
            var roleName = roleNames.FirstOrDefault();
            var role = await _roleManager.FindByNameAsync(roleName);

            var claims = await _roleManager.GetClaimsAsync(role);
            var claimValues = claims.Select(x => Convert.ToInt32(x.Value));

            var viewModel = new UserInfoViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                RoleName = roleName,
                Claims = claimValues.ToArray()
            };

            var response = new ResponseWrapper<UserInfoViewModel>
            {
                Content = viewModel
            };

            return Json(response);
        }
    }
}
