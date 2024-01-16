using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.Eshop.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }
    }
}
