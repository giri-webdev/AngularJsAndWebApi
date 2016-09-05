using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http.ExceptionHandling;
namespace ServiceLayer.Filters
{
    public class LogException:ExceptionLogger
    {
        //Log the exception occurred in the application
        public override void Log(ExceptionLoggerContext context)
        {
            try
            {
                //base.Log(context);
                Exception exc = context.ExceptionContext.Exception;
                string source = null;
                //delete the logfiles on weekly basis
                string delete = WebConfigurationManager.AppSettings["DeleteLogFiles"].ToString();
                if (delete.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    DateTime dt = DateTime.Now;
                    if (dt.DayOfWeek == DayOfWeek.Monday)
                        DeleteLogFiles();
                }

                string filePath = Path.Combine(@"~/App_Data/", "ErrorLog_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt");
                using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(filePath), true))
                {
                    sw.WriteLine("*Error Occurred on  {0} :-*", DateTime.Now);
                    sw.WriteLine();
                    if (exc.InnerException != null)
                    {
                        sw.Write("Inner Exception Type:- ");
                        sw.WriteLine(exc.InnerException.GetType().ToString());
                        sw.WriteLine("Inner Exception:- ");
                        sw.WriteLine(exc.InnerException.Message);
                        sw.WriteLine("Inner Source:- ");
                        sw.WriteLine(exc.InnerException.Source);
                        if (exc.InnerException.StackTrace != null)
                        {
                            sw.WriteLine("Inner Stack Trace:- ");
                            sw.WriteLine(exc.InnerException.StackTrace);
                        }

                    }

                    
                    sw.WriteLine("Exception Type:- ");
                    sw.WriteLine(exc.GetType().ToString());
                    sw.WriteLine("Exception:- ");
                    sw.WriteLine(exc.Message);
                    sw.WriteLine("Source:- ");
                    sw.WriteLine(source);
                    sw.WriteLine("Stack Trace:- ");
                    if (exc.StackTrace != null)
                    {
                        sw.WriteLine(exc.StackTrace);
                        sw.WriteLine();
                    }

                }

               SendEmail(exc, source);

            }
            catch (Exception)
            { }
        }

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            //return base.LogAsync(context, cancellationToken);

            Exception exc = context.ExceptionContext.Exception;
            if (exc != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("\n\n*******************************");
                builder.AppendLine("\nException type:- " + exc.InnerException.GetType().ToString());
                builder.AppendLine("\nException:- " + exc.Message);
                if(exc.StackTrace != null)
                {
                    builder.AppendLine("\nStack Trace:- " + exc.StackTrace);
                }
               
                Logger logger = LogManager.GetLogger("LogException");
                logger.Log(LogLevel.Error,builder);
            }
            return Task.FromResult(0);
        }



        public static void SendEmail(Exception exc, string source)
        {
            try
            {
                string subject = "Error occurred in giri-webdev.net";
                string message = "";
                message += "<b>Exception Type:- </b>" + exc.GetType().ToString() + "<br/>";
                message += "<b>Exception:- </b>" + exc.Message + "<br/>";
                message += "<b>Source:- </b>" + source + "<br/><br/>";
                if (exc.StackTrace != null)
                    message += "<b>StackTrace:- </b><br/>" + exc.StackTrace + "<br/>";

                MailMessage errorMessage = new MailMessage("user@giri-webdev.net", "giri.webdev@gmail.com", subject, message);
                errorMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new NetworkCredential("giri.webev@gmail.com", "dfsakdfj");
                smtp.EnableSsl = true;
                smtp.Send(errorMessage);
            }
            catch (Exception)
            { }
        }

        private static void DeleteLogFiles()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/App_Data/"));
                foreach (var file in d.GetFiles("*.txt"))
                {
                    file.Delete();
                }
            }
            catch (Exception)
            { }
        }
    }
}