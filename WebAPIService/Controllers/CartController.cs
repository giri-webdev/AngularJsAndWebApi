using ServiceLayer.Repositories;
using ServiceLayer.Repositories.Interfaces;
using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            List<CartViewModel> list = repository.ListProducts();
            return Ok(list);
        }

        [HttpPost]
        [Route("AddToCart")]
        public IHttpActionResult AddToCart(CartViewModel cartViewModel)
        {
            bool result = repository.AddToCart(cartViewModel);
            return Ok(result);
        }
    }
}
