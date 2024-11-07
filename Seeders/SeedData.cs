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

            if (!await roleManager.RoleExistsAsync("Administrador"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrador"));
            }

            if (!await roleManager.RoleExistsAsync("Alumno"))
            {
                await roleManager.CreateAsync(new IdentityRole("Alumno"));
            }

            if (!await roleManager.RoleExistsAsync("Instructor"))
            {
                await roleManager.CreateAsync(new IdentityRole("Instructor"));
            }

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
    }
}
