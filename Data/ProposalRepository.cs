using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laborlance_API.Interfaces;
using Laborlance_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Laborlance_API.Data
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly DataContext _context;
        public ProposalRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<List<Proposal>> GetOrderProposals(int orderId)
        {
            var proposals = await _context.Proposals.Where(p => p.OrderId == orderId)
                .Include(p => p.ProposedTools).ToListAsync();
            return proposals;
        }
    }
}