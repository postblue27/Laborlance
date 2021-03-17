using System.Collections.Generic;
using System.Threading.Tasks;
using Laborlance_API.Models;
using Microsoft.AspNetCore.Identity;

namespace Laborlance_API.Helpers
{
    public class RolesInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            string adminUsername = "admin";
            string adminEmail = "admin@gmail.com";
            string password = "password";
            var roles = new List<Role>
            {
                new Role{Name = "Admin"},
                new Role{Name = "Worker"},
                new Role{Name = "Customer"},
                new Role{Name = "Renter"},
            };

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role.Name) == null)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (await userManager.FindByNameAsync(adminUsername) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminUsername };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            string inhUsername = "newInhUser";
            string inhUserEmail = "newinhuser@gmail.com";
            string inhUserPassword = "password";
            string inhUserName = "ownInhUserName";
            if (await userManager.FindByNameAsync(inhUsername) == null)
            {
                InhUser inhUser = new InhUser { Email = inhUserEmail, UserName = inhUsername, InhUserName = inhUserName };
                IdentityResult result = await userManager.CreateAsync(inhUser, inhUserPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(inhUser, "renter");
                }
            }
        }
    }
}