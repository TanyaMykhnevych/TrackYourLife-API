using Common.Constants;
using Common.Utils;
using DataLayer.Entities;
using DataLayer.Entities.Identity;
using DataLayer.Entities.Organ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbContext
{
    public static class WebHostDbExtensions
    {
        public static IWebHost Seed(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                // alternatively resolve UserManager instead and pass that if only think you want to seed are the users
                using (var dbInit = scope.ServiceProvider.GetRequiredService<IDbInitializer>())
                {
                    dbInit.InitializeAsync().GetAwaiter().GetResult();
                }
            }
            return webhost;
        }
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            AppDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            await FillClinicsAsync();
            await FillRolesAsync();
            await FillUsersAsync();
            await FillOrganInfosAsync();
        }

        private async Task FillClinicsAsync()
        {
            if (!_dbContext.Clinics.Any())
            {
                var clinics = new List<Clinic>
                {
                    new Clinic()
                    {
                        Altitude = "10.20202020",
                        Longitude = "11.2328388",
                        Name = "Test Clinic 1",
                        AddressLine1 = "Some test address 1",
                        City = "Kharkov",
                        ContactEmail = "suppy2370@gmail.com",
                        ContactPhone = "+380993122354",
                        Country = "Ukraine"
                    },
                    new Clinic()
                    {
                        Altitude = "12.20202020",
                        Longitude = "13.2328388",
                        Name = "Test Clinic 2",
                        AddressLine1 = "Some test address 2",
                        City = "Kharkov",
                        ContactEmail = "suppy2370@gmail.com",
                        ContactPhone = "+380993122354",
                        Country = "Ukraine"
                    },
                    new Clinic()
                    {
                        Altitude = "14.20202020",
                        Longitude = "15.2328388",
                        Name = "Test Clinic 3",
                        AddressLine1 = "Some test address 3",
                        City = "Kharkov",
                        ContactEmail = "suppy2370@gmail.com",
                        ContactPhone = "+380993122354",
                        Country = "Ukraine"
                    },
                    new Clinic()
                    {
                        Altitude = "16.20202020",
                        Longitude = "17.2328388",
                        Name = "Test Clinic 4",
                        AddressLine1 = "Some test address 4",
                        City = "Kharkov",
                        ContactEmail = "suppy2370@gmail.com",
                        ContactPhone = "+380993122354",
                        Country = "Ukraine"
                    },
                };
                await _dbContext.Clinics.AddRangeAsync(clinics);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task FillRolesAsync()
        {
            if (!_dbContext.Roles.Any())
            {
                // TODO: add claims to roles
                await _roleManager.CreateAsync(new IdentityRole(RolesConstants.Administrator));
                await _roleManager.CreateAsync(new IdentityRole(RolesConstants.Donor));
                await _roleManager.CreateAsync(new IdentityRole(RolesConstants.Patient));
                await _roleManager.CreateAsync(new IdentityRole(RolesConstants.MedicalEmployee));
            }
        }

        private async Task FillUsersAsync()
        {
            if (_dbContext.Users.Any())
            {
                return;
            }

            //Create the default Admin account and apply the Administrator role
            string password = "Test123!";

            DateTime created = DateTime.UtcNow;
            string createdBy = "Seed";

            string adminEmail = "admin1@test.com";
            string patientEmail = "patient1@test.com";
            string donorEmail = "donor1@test.com";
            string medEmployeeEmail = "medEmployee1@test.com";

            UserInfo adminUserInfo = new UserInfo
            {
                Email = adminEmail,
                Created = created,
                CreatedBy = createdBy
            };
            var res = await _userManager.CreateAsync(new AppUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true, UserInfo = adminUserInfo }, password);
            res = await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(adminEmail), RolesConstants.Administrator);

            UserInfo patientUserInfo = new UserInfo
            {
                Email = patientEmail,
                ZipCode = "60000",
                Country = "Ukraine",
                City = "Kharkiv",
                FirstName = "First name 1",
                SecondName = "Second name 1",
                AddressLine1 = "Test address line 1 1",
                AddressLine2 = "Test address line 2 1",
                PhoneNumber = "+380991234567",
                Created = created,
                CreatedBy = createdBy
            };
            res = await _userManager.CreateAsync(new AppUser { UserName = patientEmail, Email = patientEmail, EmailConfirmed = true, UserInfo = patientUserInfo }, password);
            res = await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(patientEmail), RolesConstants.Patient);


            UserInfo donorUserInfo = new UserInfo
            {
                Email = "donor1@test.com",
                ZipCode = "60000",
                Country = "Ukraine",
                City = "Kharkiv",
                FirstName = "First name 1 2",
                SecondName = "Second name 1 2",
                AddressLine1 = "Test address line 1 2",
                AddressLine2 = "Test address line 2 2",
                PhoneNumber = "+380991234890",
                Created = created,
                CreatedBy = createdBy
            };
            res = await _userManager.CreateAsync(new AppUser { UserName = donorEmail, Email = donorEmail, EmailConfirmed = true, UserInfo = donorUserInfo }, password);
            res = await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(donorEmail), RolesConstants.Donor);

            UserInfo medEmployeeUserInfo = new UserInfo
            {
                Email = medEmployeeEmail,
                ZipCode = "60000",
                Country = "Ukraine",
                City = "Kharkiv",
                FirstName = "First name 1 3",
                SecondName = "Second name 1 3",
                AddressLine1 = "Test address line 1 3",
                AddressLine2 = "Test address line 2 3",
                PhoneNumber = "+380991245628",
                Created = created,
                CreatedBy = createdBy
            };
            res = await _userManager.CreateAsync(new AppUser { UserName = medEmployeeEmail, Email = medEmployeeEmail, EmailConfirmed = true, UserInfo = medEmployeeUserInfo }, password);
            res = await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(medEmployeeEmail), RolesConstants.MedicalEmployee);
        }

        private async Task FillOrganInfosAsync()
        {
            if (!_dbContext.OrganInfos.Any())
            {
                var organs = new List<OrganInfo>
                {
                     new OrganInfo
                     {
                         Name = "Heart",
                         Description = "The most important organ in your body",
                         OutsideHumanPossibleTime = TimeSpan.FromHours(2)
                     },
                     new OrganInfo
                     {
                         Name = "Liver",
                         Description = "Description 1",
                         OutsideHumanPossibleTime = TimeSpan.FromHours(12)
                     }
                };

                await _dbContext.OrganInfos.AddRangeAsync(organs);
                await _dbContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            this._dbContext?.Dispose();
            this._userManager?.Dispose();
            this._roleManager?.Dispose();
        }
    }
}
