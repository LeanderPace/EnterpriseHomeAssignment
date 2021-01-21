using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductsService productsService, ICartsService cartsService,
            ICategoriesService categoriesService, IWebHostEnvironment environment, ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _cartsService = cartsService;
            _environment = environment;
            _logger = logger;
        }
        public IActionResult Index(string category)
        {
            try
            {
                _logger.LogInformation("Loading Products");
                var list = _productsService.GetProducts(category);
                return View(list);
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Details(Guid id)
        {
            try
            {
                _logger.LogInformation("Accessing Product Details");
                var myProduct = _productsService.GetProduct(id);
                return View(myProduct);
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            try
            {
                _logger.LogInformation("Accessing Create Product");
                var catList = _categoriesService.GetCategories();
                ViewBag.Categories = catList;
                return View();
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }       
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
                _logger.LogInformation("Successfully Added a Product");
                ModelState.Clear();
            }
            catch(Exception ex)
            {
                TempData["danger"] = "Something went wrong"; 
                _logger.LogWarning("Something went wrong");
                _logger.LogError(ex.Message);
            }

            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _productsService.DeleteProduct(id);
                TempData["feedback"] = "Product was deleted successfully"; 
                _logger.LogInformation("Deleted Product " + id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Something went wrong");
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize]
        public IActionResult AddToCart(Guid pId)
        {
            try
            {
                string email = User.Identity.Name;
                _cartsService.AddCartProduct(pId, email);
                _logger.LogInformation("Product was added to cart " + pId);
                TempData["feedback"] = "Product added to cart successfully";
            }
            catch(Exception ex)
            {
                TempData["danger"] = "Something went wrong";
                _logger.LogWarning("Something went wrong");
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
