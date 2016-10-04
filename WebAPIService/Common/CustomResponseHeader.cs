using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ServiceLayer.Common
{
    //Message Handler Examples
    public class CustomResponseHeader:DelegatingHandler
    {
       async protected  override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
           CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            response.Headers.Add("X-Custom-Header", "This is my custom header.");

            return response;
        }
    }

    public class ValidateErrorHandelr:DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Hello Giri!")
            };

            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            tsc.SetResult(response);

            return tsc.Task;
        }
    }
}