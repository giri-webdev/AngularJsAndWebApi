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

        public CartController()
        {

            repository = new CartRepository();
        }

        [HttpGet]
        [Route("ListProducts")]
        public IHttpActionResult ListProducts()
        {
            List<CartViewModel> list = repository.ListProducts(User.Identity.GetUserId());
            return Ok(list);
        }

        [HttpPost]
        [Route("AddToCart")]
        public IHttpActionResult AddToCart(CartViewModel cartViewModel)
        {
            cartViewModel.UserId = User.Identity.GetUserId();
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
