using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    interface ICartRepository
    {
        IQueryable<Cart> GetCartProducts();

        Cart GetCartProduct(Guid id);

        void AddCartProduct(Cart cartProduct);

        void DeleteCartProduct(Guid id);

        void UpdateCartProduct(Cart cartProduct);

        void CheckOut(Order order);
     
    }
}
