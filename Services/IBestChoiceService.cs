using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Models;

namespace Laborlance_API.Services
{
    public interface IBestChoiceService
    {
        Task<Dictionary<Proposal, double>> ProposalRating(int orderId);
    }
}