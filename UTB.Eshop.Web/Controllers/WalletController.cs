// Add a new controller, for example, WalletController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using cazino3.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace cazino3.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        private readonly UserManager<cazinoUser> _userManager;

        public WalletController(UserManager<cazinoUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> BuyTokens(int tokenValue)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                // Update the user's balance
                user.WalletBalance += tokenValue;

                // Save the changes to the database
                await _userManager.UpdateAsync(user);

                // You can return a success message or other relevant data
                return Json(new { success = true, message = "Tokens purchased successfully!" });
            }

            // Return an error message if the user is not found
            return Json(new { success = false, message = "User not found." });
        }
        public async Task<IActionResult> CashIn(int cashInAmount)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                // Update the user's balance
                if (cashInAmount <= user.WalletBalance && cashInAmount >= 0 && decimal.TryParse(cashInAmount.ToString(), out _))//konroluje jesti to je decimální nenegativní číslo menší než kolik má user na účtě
                {
                    user.WalletBalance -= cashInAmount;
                    await _userManager.UpdateAsync(user);
                    return Json(new { success = true, message = "Tokens Cashed In!" });
                }
                else 
                {
                    return Json(new { success = true, message = "Invalid value" });
                }

            }

            // Return an error message if the user is not found
            return Json(new { success = false, message = "User not found." });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBalance(int newBalance)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                // Update the user's balance
                user.WalletBalance = newBalance;

                // Save the changes to the database
                await _userManager.UpdateAsync(user);

                // You can return a success message or other relevant data
                return Json(new { success = true, message = "Balance updated successfully!" });
            }

            // Return an error message if the user is not found
            return Json(new { success = false, message = "User not found." });
        }

        
    }
}
