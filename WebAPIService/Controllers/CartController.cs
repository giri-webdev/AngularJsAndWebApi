using Microsoft.AspNet.Identity;
using ServiceLayer.Repositories;
using ServiceLayer.Repositories.Interfaces;
using ServiceLayer.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace ServiceLayer.Controllers
{
    [RoutePrefix("api/Cart")]
    [Authorize]
    public class CartController : ApiController
    {
        private ICartRepository repository;
        private string userID;

        public CartController()
        {

            repository = new CartRepository();
        }

        [HttpGet]
        [Route("ListProducts")]
        public IHttpActionResult ListProducts()
        {
            userID = User.Identity.GetUserId();
            List<CartViewModel> list = repository.ListProducts(userID);
            return Ok(list);
        }

        [HttpPost]
        [Route("AddToCart")]
        public IHttpActionResult AddToCart(CartViewModel cartViewModel)
        {
            cartViewModel.UserId = userID;
            bool result = repository.AddToCart(cartViewModel);
            return Ok(result);
        }

        [HttpGet]
        [Route("DeleteItem")]
        public IHttpActionResult DeleteItem(int id)
        {
            return Ok();
        }
    }
}
