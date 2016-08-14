using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace ServiceLayer.Filters
{
    public class ErrorHandler:ExceptionHandler
    {
        /*
         * It handles uncaught exceptions thrown in 
         * controller constructor etc..
         * --Global error handling
         */
        public override void Handle(ExceptionHandlerContext context)
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Server error occurred."),
                ReasonPhrase = "Internal server error"
            };
            response.Headers.Add("gp-error", "server exception");
            context.Result = new ResponseMessageResult(response);
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
    }

}