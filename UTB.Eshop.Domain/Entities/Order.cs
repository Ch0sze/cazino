using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.Eshop.Domain.Entities
{
    public class Order : Entity<int>
    {
        public DateTime DateTimeCreated { get; protected set; }

        public string OrderNumber { get; set; }

        public double TotalPrice { get; set; }

        public int UserId { get; set; }

        //public User User { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}
