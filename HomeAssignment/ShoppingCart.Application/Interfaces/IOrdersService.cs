﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IOrdersService
    {
        void createOrder(string email, DateTime createdDate, double price);
    }
}
