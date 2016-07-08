using ServiceLayer.CustomAttributes;
using ServiceLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;

namespace ServiceLayer.Controllers
{
    [RoutePrefix("api/Values")]
    [Authorize]
    //[EnableCorsAttribute("http://localhost:63437","*","*")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("Get")]
        [Authorize(Roles ="Manager")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Giri", "Bala","Ram" };
        }

        [EnableQuery()]
        [HttpGet]
        [Route("GetProducts")]
        public IQueryable<ProductModel> GetProducts(string name=null)
        {
            return Products().AsQueryable();
        }


        [HttpPost]
        [Route("AddProduct")]
        public IHttpActionResult AddProduct(ProductModel product)
        {
            return NotFound();
        }


        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpGet]
       [IdentityAuthorize(ClaimType ="userName",ClaimValue ="Giri")]
        public IHttpActionResult GetClaims()
        {
            var identity = User.Identity as ClaimsIdentity;
            var claims = from c in identity.Claims
                         select new ExternalLoginViewModel
                         {
                             Name = c.Subject.Name,
                             Url=c.Type,
                             State = c.Value
                         };

            return Ok(claims);
        }


       
        private List<ProductModel> Products()
        {
            List<ProductModel> products = new List<ProductModel>();

            products.Add(new ProductModel { Id = 1, Name = "Apple" });
            products.Add(new ProductModel { Id = 2, Name = "Orange" });
            products.Add(new ProductModel { Id = 3, Name = "Grapes" });
            products.Add(new ProductModel { Id = 4, Name = "Mango" });
            products.Add(new ProductModel { Id = 5, Name = "Cherry" });

            return products;
        }
    }
}
