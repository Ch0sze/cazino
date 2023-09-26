using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.Eshop.Domain.Entities
{
    public class OrderItem : Entity<int>
    {
        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public int Amount { get; set; }
        public double Price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
