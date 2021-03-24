using System.Collections.Generic;
using Laborlance_API.Models;

namespace Laborlance_API.Dtos
{
    public class ProposalForCreateDto
    {
        public int OrderId { get; set; }
        public int WorkerId { get; set; }
        public ICollection<Tool> ToolsInProposal { get; set; }
    }
}