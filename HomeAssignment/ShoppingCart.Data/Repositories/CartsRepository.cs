using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class CartsRepository : ICartsRepository
    {
        private ShoppingCartDbContext _context;
        public CartsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }
        public void AddCartProduct(Cart cartProduct)
        {
            _context.Carts.Add(cartProduct);
            _context.SaveChanges();
        }

        public void DeleteCartProduct(string email, Guid id)
        {
            var myProduct = GetCartProduct(email, id);
            _context.Carts.Remove(myProduct);
            _context.SaveChanges();
        }

        public Cart GetCartProduct(string email, Guid id)
        {
            return _context.Carts.SingleOrDefault(x => x.ProductFk == id);
        }

        public IQueryable<Cart> GetCartProducts(string email)
        {
            return _context.Carts;
        }

        public void UpdateCartProduct(Cart cart)
        {
            _context.Update(cart);
            _context.SaveChanges();
        }
    }
}
