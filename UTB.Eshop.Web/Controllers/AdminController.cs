// Add a new controller, for example, WalletController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using cazino3.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace cazino3.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<cazinoUser> _userManager;

        public AdminController(UserManager<cazinoUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> BanUser(string email, string banReason)
        {
            // Trim input parameters
            email = email?.Trim();
            banReason = banReason?.Trim();

            // Find the user by nickname in the database

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                user.IsBanned = true;
                user.BanReason = banReason;

                // Save the changes to the database
                await _userManager.UpdateAsync(user);

                // You can return a success message or other relevant data
                return Json(new { success = true, message = "User was banned." });
            }

            // Return an error message if the user is not found
            return Json(new { success = false, message = "User not found." });
        }
        public async Task<IActionResult> SetAdmin(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                user.IsAdmin = true;

                // Save the changes to the database
                await _userManager.UpdateAsync(user);

                // You can return a success message or other relevant data
                return Json(new { success = true, message = "User was added admin privilegia." });

            }

            // Return an error message if the user is not found
            return Json(new { success = false, message = "User not found." });
        }
    }
}
