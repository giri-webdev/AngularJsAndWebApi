using ServiceLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceLayer.ViewModels;

namespace ServiceLayer.Repositories
{
    public class CartRepository : ICartRepository
    {
        public List<CartViewModel> ListProducts()
        {
            throw new NotImplementedException();
        }
    }
}