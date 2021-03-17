using System.Collections.Generic;

namespace Laborlance_API.Models
{
    public class Customer : User
    {
        public ICollection<Order> Orders { get; set; }
        public ICollection<Operation> OrderedOperations { get; set; }
        public ICollection<Review> GivenReviews { get; set; }
    }
}