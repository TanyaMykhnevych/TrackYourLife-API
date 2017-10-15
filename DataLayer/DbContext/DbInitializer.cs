using Common.Constants;
using Common.Utils;
using DataLayer.Entities;
using DataLayer.Entities.Identity;
using DataLayer.Entities.Organ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.DbContext
{
    public static class DbInitializer
    {
        public static void Seed()
        {
            AppDbContext dbContext = new AppDbContext();

            FillClinics(dbContext);
            FillRoles(dbContext);
            FillUsers(dbContext);
            FillOrganInfos(dbContext);
        }

        private static void FillClinics(AppDbContext dbContext)
        {
            if (!dbContext.Clinics.Any())
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
                dbContext.Clinics.AddRange(clinics);
                dbContext.SaveChanges();
            }
        }

        private static void FillRoles(AppDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                string createdBy = "Seed";
                DateTime created = DateTime.UtcNow;
                var roles = new List<Role>
                {
                    new Role
                    {
                        Name = RolesConstants.Administrator,
                        Created = created,
                        CreatedBy = createdBy
                    },
                    new Role
                    {
                        Name = RolesConstants.Donor,
                        Created = created,
                        CreatedBy = createdBy
                    },
                    new Role
                    {
                        Name = RolesConstants.Patient,
                        Created = created,
                        CreatedBy = createdBy
                    },
                    new Role
                    {
                        Name = RolesConstants.MedicalEmployee,
                        Created = created,
                        CreatedBy = createdBy
                    }
                };

                dbContext.Roles.AddRange(roles);
                dbContext.SaveChanges();
            }
        }

        private static void FillUsers(AppDbContext dbContext)
        {
            string createdBy = "Seed";
            DateTime created = DateTime.UtcNow;
            if (!dbContext.Users.Any())
            {
                Role adminRole = dbContext.Roles.SingleOrDefault(x => x.Name == RolesConstants.Administrator);
                Role patientRole = dbContext.Roles.SingleOrDefault(x => x.Name == RolesConstants.Patient);
                Role donorRole = dbContext.Roles.SingleOrDefault(x => x.Name == RolesConstants.Donor);
                Role medEmployeeRole = dbContext.Roles.SingleOrDefault(x => x.Name == RolesConstants.MedicalEmployee);

                string adminPassHash = PasswordHasher.GetPasswordHash("Test123!");
                string patientPassHash = PasswordHasher.GetPasswordHash("Test123!");
                string donorPassHash = PasswordHasher.GetPasswordHash("Test123!");
                string medEmployeePassHash = PasswordHasher.GetPasswordHash("Test123!");

                UserInfo adminUserInfo = new UserInfo
                {
                    Email = "admin1@test.com",
                    Created = created,
                    CreatedBy = createdBy
                };
                UserInfo patientUserInfo = new UserInfo
                {
                    Email = "patient1@test.com",
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
                UserInfo medEmployeeUserInfo = new UserInfo
                {
                    Email = "medEmployee1@test.com",
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

                var users = new List<User>
                {
                    new User
                    {
                        Username = "admin1@test.com",
                        UserRoles = new List<UserRole>
                        {
                            new UserRole {Created = created, CreatedBy = createdBy, RoleId = adminRole.Id}
                        },
                        PasswordHash = adminPassHash,
                        UserInfo = adminUserInfo,
                        Created = created,
                        CreatedBy = createdBy
                    },
                    new User
                    {
                        Username = "patient1@test.com",
                        UserRoles = new List<UserRole>
                        {
                            new UserRole {Created = created, CreatedBy = createdBy, RoleId = patientRole.Id}
                        },
                        PasswordHash = patientPassHash,
                        UserInfo = patientUserInfo,
                        Created = created,
                        CreatedBy = createdBy
                    },
                    new User
                    {
                        Username = "donor1@test.com",
                        UserRoles = new List<UserRole>
                        {
                            new UserRole {Created = created, CreatedBy = createdBy, RoleId = donorRole.Id}
                        },
                        PasswordHash = donorPassHash,
                        UserInfo = donorUserInfo,
                        Created = created,
                        CreatedBy = createdBy
                    },
                    new User
                    {
                        Username = "medEmployee1@test.com",
                        UserRoles = new List<UserRole>
                        {
                            new UserRole {Created = created, CreatedBy = createdBy, RoleId = medEmployeeRole.Id}
                        },
                        PasswordHash = medEmployeePassHash,
                        UserInfo = medEmployeeUserInfo,
                        Created = created,
                        CreatedBy = createdBy
                    }
                };

                dbContext.Users.AddRange(users);
                dbContext.SaveChanges();
            }
        }

        private static void FillOrganInfos(AppDbContext dbContext)
        {
            if (!dbContext.OrganInfos.Any())
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

                dbContext.OrganInfos.AddRange(organs);
                dbContext.SaveChanges();
            }
        }
    }
}
