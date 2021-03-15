using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Laborlance_API.Models
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}