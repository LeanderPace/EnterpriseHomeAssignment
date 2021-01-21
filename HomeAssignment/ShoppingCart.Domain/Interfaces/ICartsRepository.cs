using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartsRepository
    {
        IQueryable<Cart> GetCartProducts(string email);

        Cart GetCartProduct(string email, Guid id);

        void AddCartProduct(Cart cartProduct);

        void DeleteCartProduct(string email, Guid id);

        void UpdateCartProduct(Cart cart);  
    }
}
