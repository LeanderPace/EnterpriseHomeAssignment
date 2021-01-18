﻿using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }
        public Cart Cart { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
