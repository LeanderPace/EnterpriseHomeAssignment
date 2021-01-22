using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartsService
    {
        void AddCartProduct(Guid pId, string email);

        void CheckOut(Order order);

        void DeleteCartProduct(string email, Guid id);

        CartViewModel GetCartProduct(string email, Guid id);

        IQueryable<CartViewModel> GetCartProducts(string email);

        void UpdateCartProduct(string email, Guid pId);
    }
}
