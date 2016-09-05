using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using ServiceLayer.Filters;
using System.Web.Http.ExceptionHandling;
using ServiceLayer.Common;

namespace ServiceLayer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.MessageHandlers.Add(new CustomResponseHeader());
            //config.MessageHandlers.Add(new ValidateErrorHandelr());

            //Add ExceptionFilter, ExceptionLogger and ExceptionHandler
            config.Filters.Add(new ExceptionFilter());
            config.Services.Add(typeof(IExceptionLogger), new LogException());
            config.Services.Replace(typeof(IExceptionHandler), new ErrorHandler());

            //Enable cors
            config.EnableCors();

            //Json Formatters
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
