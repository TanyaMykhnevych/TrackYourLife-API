using DataLayer.DbContext;
using DataLayer.Entities.Identity;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
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
                    .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase)
                                        && u.PasswordHash.Equals(passwordHash));
        }

        public User SaveUser(User user)
        {
            user.Created = DateTime.UtcNow;
            user.CreatedBy = "Default";

            user = _dbContext.Users.Update(user).Entity;
            _dbContext.SaveChanges();

            return user;
        }
    }
}
