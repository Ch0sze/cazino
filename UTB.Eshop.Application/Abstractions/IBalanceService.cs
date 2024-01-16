﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.Eshop.Application.Abstractions
{
    public interface IBalanceService
    {
        Task<int> GetBalanceAsync(string userName);
    }
}