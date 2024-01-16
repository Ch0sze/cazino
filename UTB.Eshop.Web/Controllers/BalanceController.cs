using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UTB.Eshop.Application.Abstractions;

namespace UTB.Eshop.Web.Controllers
{
    [Route("api/balance")]
    public class BalanceController : Controller
    {
        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService ?? throw new ArgumentNullException(nameof(balanceService));
        }

        [HttpGet]
        public async Task<IActionResult> GetBalance()
        {
            try
            {
                var userName = User.Identity.Name;
                var balance = await _balanceService.GetBalanceAsync(userName);
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
