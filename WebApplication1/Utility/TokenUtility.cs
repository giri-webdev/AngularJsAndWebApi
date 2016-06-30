using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Utility
{
    public static class TokenUtility
    {
        private const string AccessTokenIndex = "AccessToken";

        public static string AccessToken
        {
            set { HttpContext.Current.Session[AccessTokenIndex] = value; }
            get
            {
                return HttpContext.Current.Session[AccessTokenIndex] == null ? string.Empty : HttpContext.Current.Session[AccessTokenIndex].ToString();
            }
        }

        public static string UserName { get; set; }
    }
}