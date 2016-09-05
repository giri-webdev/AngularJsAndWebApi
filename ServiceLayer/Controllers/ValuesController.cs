using ServiceLayer.Common;
using ServiceLayer.CustomAttributes;
using ServiceLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.Http;
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
        [AllowAnonymous]
        public IQueryable<ProductModel> GetProducts(string name=null)
        {
           return Products().AsQueryable();
        }

        [HttpGet]
        [Route("GetCountries")]
        public IQueryable<ProductModel> GetCountries()
        {
            return Countries().AsQueryable();
        }


        [HttpPost]
        [Route("AddProduct")]
        public IHttpActionResult AddProduct(ProductModel product)
        {
            return NotFound();
        }


        //Html content media type formatter example
        [HttpGet]
        [Route("HtmlContent")]
        [AllowAnonymous]
        public HttpResponseMessage HtmlContent()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ObjectContent<string>("<span style='color:red;'>Hello World</span>",new HTMLFormatter());
            return response;
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

        private List<ProductModel> Countries()
        {
            List<ProductModel> countries = new List<ProductModel>();

            countries.Add(new ProductModel { Id = 1, Name = "India" });
            countries.Add(new ProductModel { Id = 2, Name = "U.S.A" });
            countries.Add(new ProductModel { Id = 3, Name = "France" });
            countries.Add(new ProductModel { Id = 4, Name = "Australia" });
            countries.Add(new ProductModel { Id = 5, Name = "Japan" });

            return countries;
        }


        //Content Negotiation Example
        public HttpResponseMessage ContentNegotiationExample()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            IContentNegotiator defaultNegotiator = this.Configuration.Services.GetContentNegotiator();
            ContentNegotiationResult negotiationResult = defaultNegotiator.Negotiate(typeof(string), this.Request,
                this.Configuration.Formatters);

            response.Content = new ObjectContent<string>("Hello Giri!", negotiationResult.Formatter, negotiationResult.MediaType);
            return response;
        }
        
        
         
    }
}
