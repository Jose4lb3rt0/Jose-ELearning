using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Platform.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public decimal EstudiaCoins { get; set; } = 100;
    }
}
