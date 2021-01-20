using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _orderRepo;
        public OrdersService(IOrdersRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public void createOrder(string email, DateTime createdDate, double price)
        {
            Order order = new Order();
            order.OrderDate = createdDate;
            order.Email = email;
            order.Price = price;
            _orderRepo.CreateOrder(order);
        }
    }
}
