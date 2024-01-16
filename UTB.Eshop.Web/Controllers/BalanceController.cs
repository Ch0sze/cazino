using Microsoft.AspNetCore.Mvc;
using UTB.Eshop.Application.Abstractions;

namespace UTB.Eshop.Web.Controllers
{
    // YourProject.Presentation/Controllers/BalanceController.cs

    [Route("api/balance")]
    public class BalanceController : Controller
    {
        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBalance()
        {
            try
            {
                // Retrieve the authenticated user's name (assuming you have authentication set up)
                var userName = User.Identity.Name;

                // Use the service to get the balance
                var balance = await _balanceService.GetBalanceAsync(userName);

                // Return the balance as JSON
                return Json(new { balance });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while retrieving the balance.");
            }
        }
    }

}
