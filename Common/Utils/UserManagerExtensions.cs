using System;
using System.Collections.Generic;
using System.Text;
using Common.Constants;
using Common.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Common.Utils
{
    public static class UserManagerExtensions
    {
        public static bool IsUserInMedEmployeeRole(this UserManager<AppUser> userManager, string username)
        {
            var user = userManager.FindByNameAsync(username).Result;
            var isMedEmployee = userManager.IsInRoleAsync(user, RolesConstants.MedicalEmployee).Result;
            return isMedEmployee;
        }
    }
}
