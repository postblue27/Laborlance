using System.Collections.Generic;

namespace Laborlance_API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Proposal> Proposals { get; set; }
    }
}