using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.Eshop.Domain.Entities;

namespace UTB.Eshop.Application.Abstraction
{
    public interface IProductService
    {
        IList<Product> Select();
        void Create(Product product);
        bool Delete(int id);
    }
}
