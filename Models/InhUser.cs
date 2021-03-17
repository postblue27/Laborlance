using System.Collections.Generic;

namespace Laborlance_API.Models
{
    public class InhUser : User
    {
        public string InhUserName { get; set; }
        public ICollection<Tool> Tools { get; set; }
    }
}