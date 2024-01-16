using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using UTB.Eshop.Application.Abstractions;

namespace UTB.Eshop.Application.Implementation
{
    public class BalanceService : IBalanceService
    {
        private readonly UserManager<cazino3.Areas.Identity.Data.cazinoUser> _userManager;

        public BalanceService(UserManager<cazino3.Areas.Identity.Data.cazinoUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<int> GetBalanceAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return user.WalletBalance;
            }

            return 0;
        }
    }
}
