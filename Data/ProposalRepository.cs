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
        public async Task<Proposal> GetProposal(int proposalId)
        {
            var proposal = await _context.Proposals.Include(p => p.ProposedTools).Include(p => p.Order).
                FirstOrDefaultAsync(p => p.ProposalId == proposalId);
            return proposal;
        }
        public async Task<List<ToolInProposal>> GetProposedTools(int proposalId)
        {
            var tools = await _context.ToolsInProposals.Include(tip => tip.Tool).Where(tip => tip.ProposalId == proposalId).ToListAsync();
                
            return tools;
        }
        public async Task<List<Proposal>> GetOrderProposals(int orderId)
        {
            var proposals = await _context.Proposals.Where(p => p.OrderId == orderId)
                .Include(p => p.ProposedTools).ToListAsync();
            return proposals;
        }
    }
}