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
            //admin seeding
            if (await userManager.FindByNameAsync(adminUsername) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminUsername };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            //worker seeding
            string workerUsername = "newWorker";
            string wokerEmail = "newWorker@gmail.com";
            string workerPassword = "password";
            if (await userManager.FindByNameAsync(workerUsername) == null)
            {
                Worker worker = new Worker { Email = wokerEmail, UserName = workerUsername};
                IdentityResult result = await userManager.CreateAsync(worker, workerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(worker, "worker");
                }
            }
            //renter seeding
            string renterUsername = "newRenter";
            string renterEmail = "newRenter@gmail.com";
            string renterPassword = "password";
            if (await userManager.FindByNameAsync(renterUsername) == null)
            {
                Renter renter = new Renter { Email = renterEmail, UserName = renterUsername};
                IdentityResult result = await userManager.CreateAsync(renter, renterPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(renter, "renter");
                }
            }
            //customer seeding
            string cusomerUsername = "newCustomer";
            string cusomerEmail = "newCustomer@gmail.com";
            string cusomerPassword = "password";
            if (await userManager.FindByNameAsync(cusomerUsername) == null)
            {
                Customer customer = new Customer { Email = cusomerEmail, UserName = cusomerUsername};
                IdentityResult result = await userManager.CreateAsync(customer, cusomerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customer, "customer");
                }
            }
        }
    }
}