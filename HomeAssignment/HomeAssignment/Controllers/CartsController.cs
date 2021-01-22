using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Application.Interfaces;

namespace HomeAssignment.Controllers
{
    public class CartsController : Controller
    {
        private ICartsService _cartsService;
        private IOrdersService _ordersService;
        private IOrderDetailsService _orderDetailsService;
        private readonly ILogger<HomeController> _logger;
        public CartsController(ICartsService cartsService, IOrdersService ordersService,
            IOrderDetailsService orderDetailsService, ILogger<HomeController> logger)
        {
            _cartsService = cartsService;
            _ordersService = ordersService;
            _orderDetailsService = orderDetailsService;
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index(string email)
        {
            try
            {
                _logger.LogInformation("Accessing the Cart");
                var cartList = _cartsService.GetCartProducts(email);
                return View(cartList);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            string email = User.Identity.Name;

            try
            {        
                _cartsService.DeleteCartProduct(email, id);
                _logger.LogInformation("Successfully delete product " + id + " from Cart.");
                TempData["feedback"] = "Product was deleted successfully";
                return RedirectToAction("Index", new { Email = email });
            }
            catch(Exception ex)
            {
                TempData["danger"] = "Something went wrong";
                _logger.LogError(ex.Message);
                return RedirectToAction("Index", new { Email = email });
            }
        }

        [Authorize]
        public IActionResult CheckOut(double price)
        {
            string email = User.Identity.Name;
            DateTime createdDate = System.DateTime.Now;

            try
            {
                _ordersService.createOrder(email, createdDate, price);
                _orderDetailsService.FinalOrder(email, createdDate); 
                _logger.LogInformation("User Checked out successfully");
                return RedirectToAction("Index", "Products");

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error");
            }


        }
    }
}
