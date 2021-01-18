using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;

namespace HomeAssignment.Controllers
{
    public class CartsController : Controller
    {
        private ICartsService _cartsService;
        public CartsController(ICartsService cartsService)
        {
            _cartsService = cartsService;
        }
        public IActionResult Index(string email)
        {
            var cartList = _cartsService.GetCartProducts(email);
            return View(cartList);
        }
        public IActionResult Delete(Guid id)
        {
            string email = User.Identity.Name;
            _cartsService.DeleteCartProduct(email, id);
            TempData["feedback"] = "Product was deleted successfully";
            return RedirectToAction("Index", new { Email = email });
        }
    }
}
