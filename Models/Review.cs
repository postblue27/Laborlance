using System.ComponentModel.DataAnnotations;

namespace Laborlance_API.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        [Range(1,5)]
        public int Grade { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}