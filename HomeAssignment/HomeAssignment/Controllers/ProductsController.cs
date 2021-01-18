using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace HomeAssignment.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsService;
        private ICategoriesService _categoriesService;
        private ICartsService _cartsService;
        private IWebHostEnvironment _environment;
        public ProductsController(IProductsService productsService, ICartsService cartsService,
            ICategoriesService categoriesService, IWebHostEnvironment environment)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _cartsService = cartsService;
            _environment = environment;
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductViewModel data, IFormFile file)
        {
            try
            {
                if(file != null)
                {
                    if(file.Length > 0)
                    {
                        string newFileName = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                        string absolutePath = _environment.WebRootPath + @"\images\";

                        using (var stream = System.IO.File.Create(absolutePath + newFileName)) 
                        {
                            file.CopyTo(stream);
                        }

                        data.ImageUrl = @"\images\" + newFileName;
                    }
                }

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

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            _productsService.DeleteProduct(id);
            TempData["feedback"] = "Product was deleted successfully";
            return RedirectToAction("Index");
        }

        public IActionResult AddToCart(Guid pId)
        {
            string email = User.Identity.Name;
            _cartsService.AddCartProduct(pId, email);
            TempData["feedback"] = "Product added to cart successfully";
            return RedirectToAction("Index");
        }
    }
}
