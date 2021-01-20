using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    class OrderDetailsViewModel
    {
        public Guid Id { get; set; }
        public ProductViewModel Product { get; set; }
        public OrderViewModel Order { get; set; }
        public int Quantity { get; set; }
    }
}
