using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.Eshop.Application.Abstractions;

namespace UTB.Eshop.Application.Implementation
{
    public class BalanceService : IBalanceService
    {
        private readonly UserManager<cazino3.Areas.Identity.Data.cazinoUser> _userManager; // Assuming you are using ASP.NET Identity

        public BalanceService(UserManager<cazino3.Areas.Identity.Data.cazinoUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int> GetBalanceAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return user.WalletBalance;
            }

            // Handle the case where the user is not found
            return 0;
        }
    }

}
