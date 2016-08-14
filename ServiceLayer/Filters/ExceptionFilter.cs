using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ServiceLayer.Filters
{
    public class ExceptionFilter:ExceptionFilterAttribute
    {
        /*
         * Handles the uncaught exception occurred in the
         * webapi controller and in it's action method.
         */ 
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content=new StringContent("Server error occurred.")
            };
        }
    }
}