using System.Collections.Generic;

namespace Laborlance_API.Models
{
    public class Operation
    {
        public int OperationId { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ToolInRent> ToolsInRent { get; set; }
        public bool IsFinished { get; set; }
        public double FinalPrice { get; set; }
    }
}