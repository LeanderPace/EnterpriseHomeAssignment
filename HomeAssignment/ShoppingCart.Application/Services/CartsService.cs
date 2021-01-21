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
    public class CartsService : ICartsService
    {
        private ICartsRepository _cartRepo;
        private IProductsRepository _productRepo;
        private IMapper _mapper;
        public CartsService(ICartsRepository cartsRepo, IProductsRepository productRepo, IMapper mapper)
        {
            _cartRepo = cartsRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }
        public void AddCartProduct(Guid pId, string email)
        {
            Cart cart = _cartRepo.GetCartProduct(email, pId);

            if(cart == null)
            {
                Cart p = new Cart();
                p.Email = email;
                p.Quantity += 1;
                p.ProductFk = _productRepo.GetProduct(pId).Id;
                _cartRepo.AddCartProduct(p);
            } else
            {
                UpdateCartProduct(email, pId);
            }
        }

        public void DeleteCartProduct(string email, Guid id)
        {
            if (_cartRepo.GetCartProduct(email, id) != null)
            {
                _cartRepo.DeleteCartProduct(email, id);
            }
        }

        public CartViewModel GetCartProduct(string email, Guid id)
        {
            Cart cart = _cartRepo.GetCartProduct(email, id);
            var resultingCartViewModel = _mapper.Map<CartViewModel>(cart);

            return resultingCartViewModel;
        }

        public IQueryable<CartViewModel> GetCartProducts(string email)
        {
            return _cartRepo.GetCartProducts(email).Where(x => x.Email == email).ProjectTo<CartViewModel>(_mapper.ConfigurationProvider);
        }

        public void UpdateCartProduct(string email, Guid pId)
        {
            Cart beforeUpdate = _cartRepo.GetCartProduct(email, pId);
            beforeUpdate.Quantity +=1;
            _cartRepo.UpdateCartProduct(beforeUpdate);
        }
    }
}
