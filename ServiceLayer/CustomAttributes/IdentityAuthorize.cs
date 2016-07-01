using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ServiceLayer.CustomAttributes
{
    public class IdentityAuthorize:AuthorizationFilterAttribute
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext,CancellationToken cancellationToken)
        {
            //base.OnAuthorization(actionContext);

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if(!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            if(!(principal.HasClaim(x=>x.Type == ClaimType && x.Value == ClaimValue)))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            return Task.FromResult<object>(null);

        }
    }
}