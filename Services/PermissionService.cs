using E_Platform.Data;
using E_Platform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Platform.Services
{
    public class PermissionService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public PermissionService(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> HasPermissionAsync(ApplicationUser user, string permissionName)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var rolePermissions = await _context.RolePermissions
                .Include(rp => rp.Permission)
                .Include(rp => rp.Role)
                .Where(rp => userRoles.Contains(rp.Role.Name) && rp.Permission.Name == permissionName)
                .ToListAsync();

            return rolePermissions.Any();
        }

        public async Task<List<string>> GetPermissionAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var permissions = await _context.RolePermissions
                .Where(rp => roles.Contains(rp.Role.Name))
                .Select(rp => rp.Permission.Name)
                .Distinct()
                .ToListAsync();

            return permissions;
        }
    }

}
