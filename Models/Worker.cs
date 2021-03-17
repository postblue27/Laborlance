using System.Collections.Generic;

namespace Laborlance_API.Models
{
    public class Worker : User
    {
        public double HourlyWage { get; set; }
        public ICollection<Proposal> Proposals { get; set; }
        public ICollection<Operation> FulfilledOperations { get; set; }
    }
}