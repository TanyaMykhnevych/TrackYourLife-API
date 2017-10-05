using DataLayer.Entities.Identity;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IUsersService
    {
        Task<User> GetUserByCredentialsAsync(string username, string password);
    }
}
