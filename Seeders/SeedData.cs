using E_Platform.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Platform.Seeders
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //roles
            await CreateRoleIfNotExists(roleManager, "Administrador");
            await CreateRoleIfNotExists(roleManager, "Alumno");
            await CreateRoleIfNotExists(roleManager, "Instructor");

            //administrador
            var adminUser = new ApplicationUser { Name = "Admin", UserName = "admin", Email = "admin@admin.com", EmailConfirmed = true };
            var adminPassword = "admin";

            var user = await userManager.FindByNameAsync(adminUser.UserName);
            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrador");
                }
            }
        }

        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
