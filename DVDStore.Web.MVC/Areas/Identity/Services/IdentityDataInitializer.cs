using DVDStore.Web.MVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DVDStore.Web.MVC.Areas.Identity.Services
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // List of roles to seed
            string[] roleNames = { "Administrator", "User", "Manager" }; // Add or remove roles as needed

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }
        }

        public static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // List of users to seed
            ApplicationUser[] usersToSeed = new ApplicationUser[]
            {
            new ApplicationUser {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                FirstName = "Administrator",
                LastName = "User",
                EmailConfirmed = true // Since you're faking email confirmation
            },
            new ApplicationUser
            {
                UserName = "manager@example.com",
                Email = "manager@example.com",
                FirstName = "Manager",
                LastName = "User",
                EmailConfirmed = true // Since you're faking email confirmation
            },
            new ApplicationUser
            {
                UserName = "user@example.com",
                Email = "user@example.com",
                FirstName = "Regular",
                LastName = "User",
                EmailConfirmed = true // Since you're faking email confirmation
            }
            // Add more users as needed
            };

            foreach (var user in usersToSeed)
            {
                // Check if the user already exists
                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    // Create the user
                    IdentityResult result = await userManager.CreateAsync(user, "Password123!"); // Use a secure way to store the password or change it accordingly
                    // See: https://www.jondjones.com/programming/aspnet-core/how-to/secret-management-in-net-8-explained/
                    // See: https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows
                    // assign roles to the seeded users when they are created
                    if (result.Succeeded && user.FirstName == "Administrator")
                    {
                        await userManager.AddToRoleAsync(user, "Administrator"); // Assign roles as needed
                    }
                    else if (result.Succeeded && user.FirstName == "Manager")
                    {
                        await userManager.AddToRoleAsync(user, "Manager"); // Assign roles as needed
                    }
                    else if (result.Succeeded && user.FirstName == "Regular")
                    {
                        await userManager.AddToRoleAsync(user, "User"); // Assign roles as needed
                    }
                }
            }
        }
    }
}