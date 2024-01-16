using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.Eshop.Application.Abstractions;

namespace UTB.Eshop.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<cazino3.Areas.Identity.Data.cazinoUser> _userManager;

        public UserService(UserManager<cazino3.Areas.Identity.Data.cazinoUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetNickNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return user?.NickName;
        }
    }
}
