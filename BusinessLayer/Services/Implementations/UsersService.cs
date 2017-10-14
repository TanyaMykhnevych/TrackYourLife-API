using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Utils;
using DataLayer.Entities;
using DataLayer.Entities.Identity;
using DataLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRolesRepository _rolesRepository;

        public UsersService(
            IUsersRepository usersRepository,
            IRolesRepository rolesRepository)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
        }

        public Task<User> GetUserByCredentialsAsync(string username, string password)
        {
            string passwordHash = PasswordHasher.GetPasswordHash(password);
            return _usersRepository.GetUserAsync(username, passwordHash);
        }

        public User RegisterUserAsDonor(UserInfo userInfo)
        {
            Role patientRole = _rolesRepository.GetRoleByName(RolesConstants.Donor);
            //TODO: send email about registration
            return RegisterUser(userInfo, patientRole);
        }

        public User RegisterUserAsPatient(UserInfo userInfo)
        {
            Role patientRole = _rolesRepository.GetRoleByName(RolesConstants.Patient);
            //TODO: send email about registration
            return RegisterUser(userInfo, patientRole);
        }

        private User RegisterUser(UserInfo userInfo, Role role)
        {
            //TODO: need to determine user
            string createdBy = "Default";
            DateTime created = DateTime.UtcNow;

            userInfo.Created = created;
            userInfo.CreatedBy = createdBy;

            string password = PasswordHasher.GeneratePassword();
            string passwordHash = PasswordHasher.GetPasswordHash(password);
            var user = new User()
            {
                Created = created,
                CreatedBy = createdBy,
                UserInfo = userInfo,
                Username = userInfo.Email,
                PasswordHash = passwordHash,
                UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        RoleId = role.Id,
                        Created = created,
                        CreatedBy = createdBy
                    }
                }
            };

            try
            {
                user = _usersRepository.SaveUser(user);
            }
            catch (Exception ex)
            {
                //TODO: Log rexception
                throw;
            }

            //TODO: send email with credentials to userInfo.Email

            return user;
        }
    }
}
