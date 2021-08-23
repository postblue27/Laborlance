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
            var renterTools = await _context.Tools.Where(t => t.RenterId == renterId).Include(t => t.Operation).
                Include(t => t.ToolImages).ToListAsync();
            return renterTools;
        }
        public async Task<Tool> GetToolById(int toolId)
        {
            var tool = await _context.Tools.Include(t => t.Operation).Include(t => t.ToolImages)
                .FirstOrDefaultAsync(o => o.ToolId == toolId);
            return tool;
        }
        public async Task<List<Tool>> GetAllTools()
        {
            var tools = await _context.Tools.ToListAsync();
            return tools;
        }

        public async Task<ToolImage> GetToolImageByPublicIdAsync(string publicId)
        {
            var image = await _context.ToolImages.FirstOrDefaultAsync(ti => ti.PublicId == publicId);
            return image;
        }
    }
}