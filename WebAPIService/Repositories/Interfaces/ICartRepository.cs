using ServiceLayer.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Repositories.Interfaces
{
    public interface ICartRepository
    {
        List<CartViewModel> ListProducts(string userID);
        bool AddToCart(CartViewModel viewModel);

        bool DeleteItem(int cartId);
    }
}