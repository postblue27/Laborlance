namespace Laborlance_API.Models
{
    public class ToolInProposal
    {
        public int ToolInProposalId { get; set; }
        public int ToolId { get; set; }
        public Tool Tool { get; set; }
        public int ProposalId { get; set; }
        public Proposal Proposal { get; set; }
    }
}