/**********************************************************************************
**
**  DVDStore Application v1.0
**
**  Copyright 2024
**  Developed by:
**     Ronald Garlit.
**
**  This software was created for educational purposes.
**
**  Use is subject to license terms.
***********************************************************************************
**
**  FileName: IdentityDataInitializer.cs (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**
**  Description:
**  This class contains static methods for seeding initial data into the ASP.NET Core
**  Identity system. It is responsible for creating predefined roles and users for
**  the DVDStore application. The SeedData method is the entry point, which calls
**  SeedRoles to create roles and SeedUsers to create users and assign them to roles.
**
**  The SeedRoles method creates roles specified in the roleNames array if they do not
**  already exist in the database. It ensures that roles like "Administrator", "Manager",
**  and "User" are available for user assignment.
**
**  The SeedUsers method creates user accounts if they do not already exist in the database.
**  It uses the UserManager service to create users with predefined usernames, emails,
**  first names, last names, and confirms their email addresses. Each user is assigned a
**  predefined password. After creating the users, it assigns them to the appropriate roles
**  based on their first name.
**
**  Note:
**  - Ensure to use a secure way to store and manage passwords.
**  - Modify the roles and users to match the specific requirements of the application.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-05-31      RGARLIT      STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace DVDStore.Web.MVC.Areas.Identity.Services
{
    /// <summary>
    /// Identity Data Initializer class to seed initial data into the ASP.NET Core Identity system.
    /// </summary>
    public static class IdentityDataInitializer
    {
        #region Public Methods

        /// <summary>
        /// Seed initial data into the ASP.NET Core Identity system.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        public static async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        /// <summary>
        /// Seed roles into the ASP.NET Core Identity system.
        /// </summary>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // List of roles to seed
            string[] roleNames = ["Administrator", "Manager", "User"]; // Add or remove roles as needed

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }
        }

        /// <summary>
        /// Seed users into the ASP.NET Core Identity system.
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // List of users to seed
            ApplicationUser[] usersToSeed =
            [
            new() {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                FirstName = "Administrator",
                LastName = "User",
                EmailConfirmed = true // Since you're faking email confirmation
            },
            new() {
                UserName = "manager@example.com",
                Email = "manager@example.com",
                FirstName = "Manager",
                LastName = "User",
                EmailConfirmed = true // Since you're faking email confirmation
            },
            new() {
                UserName = "user@example.com",
                Email = "user@example.com",
                FirstName = "Regular",
                LastName = "User",
                EmailConfirmed = true // Since you're faking email confirmation
            }
            // Add more users as needed
            ];

            foreach (var user in usersToSeed)
            {
                // Check if the user already exists
                if (await userManager.FindByEmailAsync(user.Email!) == null)
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

        #endregion Public Methods
    }
}

/*
          .---.     .---.
         ( -o- )---( -o- )
         ;-...-`   `-...-;
        /                 \
       /                   \
      | /_               _\ |
      \`'.`'"--.....--"'`.'`/
       \  '.   `._.`   .'  /
    _.-''.  `-.,___,.-`  .''-._
   `--._  `'-._______.-'`  _.--`
   jgs  /                 \
       /.-'`\   .'.   /`'-.\
      `      '.'   '.'

*/