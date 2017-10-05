using BusinessLayer.Helpers;
using BusinessLayer.Services.Abstractions;
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

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<User> GetUserByCredentialsAsync(string username, string password)
        {
            string passwordHash = PasswordHasher.GetPasswordHash(password);
            return _usersRepository.GetUserAsync(username, passwordHash);
        }
    }
}
