using ServiceLayer.Repositories;
using ServiceLayer.Repositories.Interfaces;
using ServiceLayer.ViewModels;
using System.Collections.Generic;
using System.Web.Http;


namespace ServiceLayer.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private IProductRepository repository;
        public ProductController()
        {
            repository = new ProductRepository();
        }

        [HttpGet]
        [Route("ListProducts")]
        public IHttpActionResult ListProducts()
        {
            List<CategoryViewModel> products = repository.ListProducts();
            return Ok(products);
        }

    }
}
