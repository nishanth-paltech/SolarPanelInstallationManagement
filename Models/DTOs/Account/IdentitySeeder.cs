using Microsoft.AspNetCore.Identity;
using SolarPanelInstallationManagement.Models.Entities;

namespace SolarPanelInstallationManagement.Models.DTOs.Account
{
    public static class IdentitySeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();

            var roleManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            var config = scope.ServiceProvider
                .GetRequiredService<IConfiguration>();

            const string adminRole = "Admin";

            // 1️⃣ Ensure Admin role exists
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // 2️⃣ Read admin credentials
            var userName = config["AdminUser:UserName"];
            var password = config["AdminUser:Password"];
            var email = config["AdminUser:Email"];

            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("AdminUser configuration is missing.");
            }

            // 3️⃣ Ensure admin user exists
            var adminUser = await userManager.FindByNameAsync(userName);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, password);

                if (!result.Succeeded)
                {
                    throw new Exception(
                        "Failed to create admin user: " +
                        string.Join(", ", result.Errors.Select(e => e.Description))
                    );
                }
            }

            // 4️⃣ Ensure user is in Admin role
            if (!await userManager.IsInRoleAsync(adminUser, adminRole))
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }
    }
}
