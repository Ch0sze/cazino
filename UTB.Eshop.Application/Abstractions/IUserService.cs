using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.Eshop.Application.Abstractions
{
    public interface IUserService
    {
        Task<string> GetNickNameAsync(string userName);
    }
}
