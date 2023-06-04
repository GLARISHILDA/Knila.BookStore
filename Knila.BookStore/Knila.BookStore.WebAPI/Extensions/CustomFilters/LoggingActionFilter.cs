namespace Knila.BookStore.WebAPI.Extensions.CustomFilters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using NLog;
    using System.Diagnostics;

    public class LoggingActionFilter : IAsyncActionFilter
    {
        private Logger _nlogger = LogManager.GetCurrentClassLogger(); // creates a logger using the class name

        public LoggingActionFilter(ILoggerFactory loggerFactory)
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //context.HttpContext.Response.Headers.Add("_Application-Name", this._appSetting.ApplicationName);
            //context.HttpContext.Response.Headers.Add("_Powered-By", this._appSetting.HttpHeaderSetting.PoweredBy);

            var localTimeZone = System.TimeZoneInfo.Local;
            string responseTimeZome = localTimeZone.Id + " - " + localTimeZone.DisplayName;

            context.HttpContext.Response.Headers.Add("_RequestTime", DateTime.Now.ToString());
            context.HttpContext.Response.Headers.Add("_Server-TimeZone", responseTimeZome);
            context.HttpContext.Response.Headers.Add("_RequestId", context.HttpContext.TraceIdentifier);
            //context.HttpContext.Response.Headers.Add("X-Xss-Protection", this._appSetting.HttpHeaderSetting.XXssProtection);
            //context.HttpContext.Response.Headers.Add("X-Frame-Options", this._appSetting.HttpHeaderSetting.XFrameOptions);

            //if (this._appSetting.IsWebsiteUnderMaintenance)
            //{
            //    var templete = @"Our website is currently under maintenance and upgrades, but we'll return shortly.";

            //    context.HttpContext.Response.WriteAsync(templete).Wait();

            //    return;
            //}

            this._nlogger = LogManager.GetLogger(context.ActionDescriptor.DisplayName);

            try
            {
                this._nlogger.Info(
                     "Executing Method - " + context.ActionDescriptor.DisplayName);

                var resultContext = await next();

                if (resultContext.Exception == null)
                {
                    this._nlogger.Info(
                             "Successfuly Executed Method - " + context.ActionDescriptor.DisplayName);
                }
                else
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    this._nlogger.Error("Error Occured on Executing Method - " + resultContext.Exception.Message, resultContext.Exception);
#pragma warning restore CS0618 // Type or member is obsolete

                    StackTrace st = new StackTrace(resultContext.Exception, true);
                    StackFrame frame = st.GetFrame(0);
                    string fileName = frame.GetFileName();
                    string methodName = frame.GetMethod().Name;
                    int line = frame.GetFileLineNumber();
                    int col = frame.GetFileColumnNumber();

                    throw new Exception("Exception Thrown : "
                       + "\n  Type :    " + resultContext.Exception.GetType().Name
                       + "\n  Message : " + resultContext.Exception.Message
                       + "\n  Project : " + resultContext.Exception.Source
                       + "\n  Source : " + fileName + " | Method : " + methodName + " | Line :" + line.ToString() + " | " + col.ToString());
                }
            }
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            {
                throw;
            }
        }
    }
}