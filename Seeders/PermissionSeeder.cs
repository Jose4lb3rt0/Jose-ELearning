using E_Platform.Data;
using E_Platform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Platform.Seeders
{
    public static class PermissionSeeder
    {
        public static async Task SeedPermissions(RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            var permissions = new List<string> { "CrearCurso", "EditarCurso", "EliminarCurso", "VerCurso", "VerCursos" };

            var existingPermissions = await context.AppPermissions.AsNoTracking().ToListAsync();

            foreach (var permissionName in permissions)
            {
                if (!existingPermissions.Any(p => p.Name == permissionName))
                {
                    context.AppPermissions.Add(new AppPermission { Name = permissionName });
                    Console.WriteLine($"Permiso creado: {permissionName}");
                }
            }

            await context.SaveChangesAsync();

            var allPermissions = await context.AppPermissions.AsNoTracking().ToListAsync();

            var adminRole = await roleManager.FindByNameAsync("Administrador");
            var alumnoRole = await roleManager.FindByNameAsync("Alumno");

            if (adminRole != null)
            {
                var existingRolePermissions = await context.RolePermissions
                    .Where(rp => rp.RoleId == adminRole.Id)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (var permission in allPermissions)
                {
                    if (!existingRolePermissions.Any(rp => rp.PermissionId == permission.Id))
                    {
                        context.RolePermissions.Add(new RolePermission
                        {
                            RoleId = adminRole.Id,
                            PermissionId = permission.Id
                        });
                        Console.WriteLine($"Permiso '{permission.Name}' asignado a rol 'Administrador'");
                    }
                }

                await context.SaveChangesAsync();
            }

            if (alumnoRole != null)
            {
                var existingRolePermissions = await context.RolePermissions
                    .Where(rp => rp.RoleId == alumnoRole.Id)
                    .AsNoTracking()
                    .ToListAsync();

                var studentPermissions = allPermissions
                    .Where(p => p.Name == "VerCurso" || p.Name == "VerCursos")
                    .ToList();

                foreach (var permission in studentPermissions)
                {
                    if (!existingRolePermissions.Any(rp => rp.PermissionId == permission.Id))
                    {
                        context.RolePermissions.Add(new RolePermission
                        {
                            RoleId = alumnoRole.Id,
                            PermissionId = permission.Id
                        });
                        Console.WriteLine($"Permiso '{permission.Name}' asignado a rol 'Alumno'");
                    }
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
