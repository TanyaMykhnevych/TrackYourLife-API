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
            //TODO: need to determine user
            string createdBy = "Default";
            DateTime created = DateTime.UtcNow;

            userInfo.Created = created;
            userInfo.CreatedBy = createdBy;

            Role donorRole = _rolesRepository.GetRoleByName(RolesConstants.Donor);

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
                        RoleId = donorRole.Id,
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

            return user;
        }
    }
}
