using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;

namespace HomeAssignment.Controllers
{
    public class CartsController : Controller
    {
        private ICartsService _cartsService;
        private IOrdersService _ordersService;
        private IOrderDetailsService _orderDetailsService;
        public CartsController(ICartsService cartsService, IOrdersService ordersService,
            IOrderDetailsService orderDetailsService)
        {
            _cartsService = cartsService;
            _ordersService = ordersService;
            _orderDetailsService = orderDetailsService;
        }

        [Authorize]
        public IActionResult Index(string email)
        {
            var cartList = _cartsService.GetCartProducts(email);
            return View(cartList);
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            string email = User.Identity.Name;
            _cartsService.DeleteCartProduct(email, id);
            TempData["feedback"] = "Product was deleted successfully";
            return RedirectToAction("Index", new { Email = email });
        }

        [Authorize]
        public IActionResult CheckOut(double price)
        {
            string email = User.Identity.Name;
            DateTime createdDate = System.DateTime.Now;
            _ordersService.createOrder(email, createdDate, price);
            _orderDetailsService.FinalOrder(email, createdDate);
            return RedirectToAction("Index", "Products");
        }
    }
}
