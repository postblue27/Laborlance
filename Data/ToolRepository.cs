using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Laborlance_API.Data
{
    public class ToolRepository : IToolRepository
    {
        private readonly DataContext _context;
        public ToolRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<List<Tool>> GetRenterTools(int renterId)
        {
            var renterTools = await _context.Tools.Where(t => t.RenterId == renterId).ToListAsync();
            return renterTools;
        }
    }
}