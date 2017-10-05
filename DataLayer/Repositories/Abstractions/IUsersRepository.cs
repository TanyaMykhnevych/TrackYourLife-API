using DataLayer.Entities.Identity;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Abstractions
{
    public interface IUsersRepository
    {
        Task<User> GetUserAsync(string username, string passwordHash);
    }
}
