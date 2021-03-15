using System.Threading.Tasks;
using Laborlance_API.Models;

namespace Laborlance_API.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(string username, string email, string password);
        Task<bool> UserExists(string username, string userType);
        Task<User> Login(string username, string userType, string password);
        Task<User> GetUser(string username, string userType);
    }
}