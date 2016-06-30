using ServiceLayer.CustomAttributes;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ServiceLayer.Controllers
{
    [RoutePrefix("api/Values")]
    [Authorize]
    [EnableCorsAttribute("http://localhost","*","*")]
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
    }
}
