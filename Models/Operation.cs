using System;
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
        public ICollection<Tool> ToolsInRent { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double CurrentPrice { get; set; }
    }
}