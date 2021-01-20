using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IOrderDetailsService
    {
        void FinalOrder(string email, DateTime createdDate);
    }
}
