using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Laborlance_API.Data
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public AdminRepository(DataContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;

        }
        public async Task<List<User>> GetUsers()
        {
            return await _userManager.Users.Include(u => u.UserRoles).ToListAsync();
        }

        public async Task<List<User>> GetUsersByRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return (List<User>)users;
        }
    }
}