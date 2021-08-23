using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Laborlance_API.Dtos;
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
        private readonly IMapper _mapper;

        public AdminRepository(DataContext context, UserManager<User> userManager, IMapper mapper)
        {
            _mapper = mapper;
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
        public async Task<List<UserForAndroid>> GetUsersByRoleForAndroid(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            var userListToReturn = new List<UserForAndroid>();
            foreach (User u in users)
            {
                var userForAndroid = _mapper.Map<UserForAndroid>(u);
                userListToReturn.Add(userForAndroid);
            }
            return userListToReturn;
        }
    }
}