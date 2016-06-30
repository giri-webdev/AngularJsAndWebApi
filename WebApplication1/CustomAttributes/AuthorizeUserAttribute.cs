using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Utility;

namespace WebApplication1.CustomAttributes
{
    public class AuthorizeUserAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;

            if(string.IsNullOrEmpty(TokenUtility.UserName) || string.IsNullOrEmpty(TokenUtility.AccessToken))
            {

                if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                    return;

                base.OnAuthorization(filterContext);

                if (filterContext.Result.GetType() == typeof(HttpUnauthorizedResult))
                {
                    var url = new UrlHelper(filterContext.RequestContext);
                    var loginUrl = url.Content("~/Account/Login");
                    session.RemoveAll();
                    session.Clear();
                    session.Abandon();
                    filterContext.HttpContext.Response.Redirect(loginUrl, true);
                }//ajax request return status code
                else if (filterContext.Result.GetType() == typeof(HttpUnauthorizedResult) &&
                     filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new ContentResult();
                    filterContext.HttpContext.Response.StatusCode = 403;
                }
            }
        
        }
    }
}