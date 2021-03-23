using System.Collections.Generic;

namespace Laborlance_API.Models
{
    public class Renter : User
    {
        public ICollection<Tool> Tools { get; set; }
    }
}