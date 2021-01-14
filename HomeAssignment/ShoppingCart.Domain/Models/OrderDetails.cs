using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public Guid ProductFk { get; set; }
        public virtual Product Product { get; set; }
        public Guid OrderFk { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
