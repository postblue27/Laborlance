using System.Collections.Generic;

namespace Laborlance_API.Models
{
    public class Worker : User
    {
        public double HourlyWage { get; set; }
        public double Rating { get; set; }
        public ICollection<Proposal> Proposals { get; set; }
        public ICollection<Operation> Operations { get; set; }
        public ICollection<Review> ReceivedReviews { get; set; }
    }
}