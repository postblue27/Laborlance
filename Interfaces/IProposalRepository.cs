using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Models;

namespace Laborlance_API.Interfaces
{
    public interface IProposalRepository
    {
        Task<List<ToolInProposal>> GetProposedTools(int proposalId);
        Task<Proposal> GetProposal(int proposalId);
        Task<List<Proposal>> GetOrderProposals(int orderId);
    }
}