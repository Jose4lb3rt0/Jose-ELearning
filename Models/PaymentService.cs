using Microsoft.AspNetCore.Identity;

namespace E_Platform.Models
{
    public class PaymentService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> PurchaseCourse(ApplicationUser user, Curso curso)
        {
            if (user.EstudiaCoins >= curso.Precio)
            {
                user.EstudiaCoins -= curso.Precio;
                await _userManager.UpdateAsync(user);
                return true;
            }
            return false;
        }

        public async Task AdjustBalance(ApplicationUser user, decimal amount)
        {
            user.EstudiaCoins += amount;
            await _userManager.UpdateAsync(user);
        }
    }

}
