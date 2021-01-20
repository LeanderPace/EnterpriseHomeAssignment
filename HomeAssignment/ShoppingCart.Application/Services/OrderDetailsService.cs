using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private IOrderDetailsRepository _orderDetailsRepo;
        private IOrdersRepository _orderRepo;
        private ICartsService _cartService;
        private IProductsService _productService;
        
        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepo, IOrdersRepository orderRepo,
            ICartsService cartsService, IProductsService productService)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _orderRepo = orderRepo;
            _cartService = cartsService;
            _productService = productService;
        }
        public void FinalOrder(string email, DateTime createdDate)
        {           
            OrderDetails od;
            Cart cart;

            var getCart = _cartService.GetCartProducts(email).ToList();

            foreach (var p in getCart)
            {
                od = new OrderDetails
                {
                    OrderFk = _orderRepo.GetOrder(email, createdDate).Id,
                    ProductFk = p.Product.Id,
                    Quantity = p.Quantity,
                };

                cart = new Cart
                {
                    ProductFk = p.Product.Id
                };

                _orderDetailsRepo.AddOrderDetails(od);
                _cartService.DeleteCartProduct(email, cart.ProductFk);
                _productService.UpdateProduct(cart.ProductFk, od.Quantity);
            }
        }
    }
}
