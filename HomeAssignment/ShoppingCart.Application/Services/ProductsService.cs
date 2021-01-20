using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class ProductsService : IProductsService
    {
        private IProductsRepository _productRepo;
        private IMapper _mapper;
        public ProductsService(IProductsRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public IQueryable<ProductViewModel> GetProducts()
        {
            return _productRepo.GetProducts().ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);
        }
        public ProductViewModel GetProduct(Guid id)
        {
            Product product = _productRepo.GetProduct(id);
            var resultingProductViewModel = _mapper.Map<ProductViewModel>(product);

            return resultingProductViewModel;
        }

        public void AddProduct(ProductViewModel data)
        {
            var p = _mapper.Map<Product>(data);

            _productRepo.AddProduct(p);
        }

        public void DeleteProduct(Guid id)
        {
            if(_productRepo.GetProduct(id) != null)
            {
                _productRepo.DeleteProduct(id);
            }
        }

        public void UpdateProduct(Guid id, int quantity)
        {
            Product p = _productRepo.GetProduct(id);
            p.Quantity =- quantity;
            _productRepo.UpdateProduct(p);
        }
    }
}
