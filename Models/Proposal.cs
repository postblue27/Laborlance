using System.Collections.Generic;

namespace Laborlance_API.Models
{
    public class Proposal
    {
        public int ProposalId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public ICollection<ToolInProposal> ProposedTools { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}