// Add a new controller, for example, WalletController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using cazino3.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace cazino3.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        private readonly UserManager<cazinoUser> _userManager;
        private readonly DBcazinoContext _dbCazinoContext;

        public WalletController(UserManager<cazinoUser> userManager, DBcazinoContext dbCazinoContext)
        {
            _userManager = userManager;
            _dbCazinoContext = dbCazinoContext;
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
                if (cashInAmount <= user.WalletBalance && cashInAmount > 0 && decimal.TryParse(cashInAmount.ToString(), out _))//konroluje jesti to je decimální nenegativní číslo menší než kolik má user na účtě
                {
                    user.WalletBalance -= cashInAmount;
                    await _userManager.UpdateAsync(user);
                    //-------
                    var cashin = new cashIn()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserEmail = user.Email,
                        Amount = cashInAmount,
                        Date = DateTime.UtcNow

                    };
                    _dbCazinoContext.CashIn.Add(cashin);
                    await _dbCazinoContext.SaveChangesAsync();

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
        public IActionResult GetCashIns()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            if (user != null)
            {
                var cashIns = _dbCazinoContext.CashIn
                    .Where(c => c.UserEmail == user.Email)
                    .ToList();

                return Json(cashIns);
            }

            return Json(new List<cashIn>());
        }

    }

}
