using Microsoft.AspNetCore.Identity;

namespace E_Platform.Models
{
    public class RolePermission
    {
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }

        public int PermissionId { get; set; }
        public AppPermission Permission { get; set; }
    }
}
