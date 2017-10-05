using DataLayer.DbContext;
using DataLayer.Entities.Identity;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _dbContext;

        public UsersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<User> GetUserAsync(string username, string passwordHash)
        {
            return _dbContext.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(u => u.Role)
                .SingleOrDefaultAsync(u => u.PasswordHash.Equals(passwordHash));
        }
    }
}
