using System.Threading.Tasks;
using Laborlance_API.Dtos;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Laborlance_API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(DataContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;

        }
        public async Task<Worker> UpdateWorker(WorkerForUpdateDto workerForUpdateDto)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == workerForUpdateDto.Id);
            worker.UserName = workerForUpdateDto.UserName;
            worker.Email = workerForUpdateDto.Email;
            worker.HourlyWage = workerForUpdateDto.HourlyWage;
            worker.Rating = workerForUpdateDto.Rating;
            await _context.SaveChangesAsync();
            return worker;
        }
    }
}