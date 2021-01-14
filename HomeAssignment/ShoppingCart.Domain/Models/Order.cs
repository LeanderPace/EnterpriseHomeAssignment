﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
    }
}
