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
        private readonly ILogger<ProductsController> _logger;
        public CartsController(ICartsService cartsService, IOrdersService ordersService,
            IOrderDetailsService orderDetailsService, ILogger<ProductsController> logger)
        {
            _cartsService = cartsService;
            _ordersService = ordersService;
            _orderDetailsService = orderDetailsService;
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index(string email)
        {
            var cartList = _cartsService.GetCartProducts(email);

            try
            {
                _logger.LogInformation("Accessing Cart");
                return View(cartList);
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            try
            {
                string email = User.Identity.Name;
                _cartsService.DeleteCartProduct(email, id);
                TempData["feedback"] = "Product was deleted successfully";
                _logger.LogInformation("Product was deleted from cart successfully " + id);
                return RedirectToAction("Index", new { Email = email });

            }
            catch(Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }

        [Authorize]
        public IActionResult CheckOut(double price)
        {
            try
            {
                string email = User.Identity.Name;
                DateTime createdDate = System.DateTime.Now;
                _ordersService.createOrder(email, createdDate, price);
                _orderDetailsService.FinalOrder(email, createdDate); 
                _logger.LogInformation("User checked out successfully");
                return RedirectToAction("Index", "Products");
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
            
        }
    }
}
