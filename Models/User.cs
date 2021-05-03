using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Laborlance_API.Models
{
    public class User : IdentityUser<int>
    {
        public string PhotoUrl { get; set; }
        public string PublicId { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}