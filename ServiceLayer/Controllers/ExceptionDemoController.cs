using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceLayer.Controllers
{
    /*
    The below action methods won't invoke 
    the exception filer or exception handler.
    The exceptions are already handled by the action 
    method itself.
    */
    [RoutePrefix("api/ExceptionTest")]
    public class ExceptionDemoController : ApiController
    {
        /*To test exception handler,
        uncomment the below statement*/
        public ExceptionDemoController()
        {
            throw new NotImplementedException();
        }

        [Route("TestFilter")]
        [HttpGet]
        public IHttpActionResult TestExceptionFilter()
        {
            throw new FileNotFoundException();
        }

        //Returns HttpStatusCode 404
        [Route("FileNotFound")]
        [HttpGet]
        public IHttpActionResult FileNotFound()
        {
            return NotFound();
        }

        //Returns HttpStatusCode 500  
        [Route("InternalError")]
        [HttpGet]
        public IHttpActionResult InternalError()
        {
            return InternalServerError();
        }


        //Returns custom error message to the client
        [Route("ResponseException")]
        [HttpGet]
        public IHttpActionResult ResponseException()
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Server error occurred."),
                ReasonPhrase="Internal error occurred"
            };

            throw new HttpResponseException(response);
        }

        //Returns HttpError object with message to the client
        [Route("ErrorResponse")]
        [HttpGet]
        public HttpResponseMessage ErrorResponse()
        {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "HttpError Test");
        }
    }
}
