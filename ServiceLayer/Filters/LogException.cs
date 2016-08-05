using System.Diagnostics;
using System.Web.Http.ExceptionHandling;
namespace ServiceLayer.Filters
{
    public class LogException:ExceptionLogger
    {
        /*
         * Logs the exception occured in the application
         */
        public override void Log(ExceptionLoggerContext context)
        {
            //base.Log(context);
            Debug.WriteLine(context.ExceptionContext.Exception.ToString());
            //Write your loging logic here
        }
    }
}