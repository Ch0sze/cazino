using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.Eshop.Domain.Entities;

namespace UTB.Eshop.Infrastructure.Database
{
    public class DatabaseFake
    {
        public static List<Product> Products { get; set; }

        static DatabaseFake()
        {
            Products = new List<Product>();
            Products.Add(new Product
            {
                Id = 1,
                Name = "PC",
                Description = "nej PC na světě",
                Price = 10000,
                ImageSrc = ""
            });
            Products.Add(new Product
            {
                Id = 2,
                Name = "PC",
                Description = "nej PC na světě",
                Price = 10000,
                ImageSrc = ""
            });
            Products.Add(new Product
            {
                Id = 3,
                Name = "PC",
                Description = "nej PC na světě",
                Price = 10000,
                ImageSrc = ""
            });
            Products.Add(new Product
            {
                Id = 4,
                Name = "PC",
                Description = "nej PC na světě",
                Price = 10000,
                ImageSrc = ""
            });
        }
    }
}
