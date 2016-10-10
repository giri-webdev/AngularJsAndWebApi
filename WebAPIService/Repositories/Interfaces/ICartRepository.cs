using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.Repositories.Interfaces
{
    public interface ICartRepository
    {
        List<CartViewModel> ListProducts();
        bool AddToCart(CartViewModel viewModel);
    }
}