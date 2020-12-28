using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace HomeAssignment.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsService;
        private ICategoriesService _categoriesService;
        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }
        public IActionResult Index()
        {
            var list = _productsService.GetProducts();
            return View(list);
        }

        public IActionResult Details(Guid id)
        {
            var myProduct = _productsService.GetProduct(id);
            return View(myProduct);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel data)
        {
            try
            {
                //if(data.Name == "") { throw new Exception("Name cannot be empty"); }

                _productsService.AddProduct(data);
                TempData["feedback"] = "Product was added successfully";
                ModelState.Clear();
            }
            catch(Exception ex)
            {
                TempData["danger"] = "Something went wrong";
            }

            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();
        }

        public IActionResult Delete(Guid id)
        {
            _productsService.DeleteProduct(id);
            TempData["feedback"] = "Product was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
