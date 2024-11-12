using Microsoft.AspNetCore.Identity;

namespace E_Platform.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
