using System.Threading.Tasks;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Laborlance_API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string username, string userType)
        {
            if(await _context.Users.AnyAsync(u => u.UserName == username))
                return true;
            return false;
        }

        public async Task<User> GetUser(string username, string userType)
        {
            var user = new User();

            user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
                     
            if(user == null)
                return null;

            return user;
        }
    }
}