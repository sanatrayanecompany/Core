using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Core.Base
{
    public class CoreExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionContext)
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            if (Host.Config.ExceptionLog)
            {
                File.AppendAllText(Path.Combine(Host.Path, "exceptions.log"), string.Format("* {0}\n\t{1} {2}\n\t{3} {4}\n\n",
                    DateTime.Now.ToString(),
                    actionContext.ActionContext.Request.Method,
                    actionContext.ActionContext.Request.RequestUri.PathAndQuery,
                    500,
                    actionContext.Exception.Message)
                );
            }
            else
            {
                message.Content = new StringContent(actionContext.Exception.Message);
            }
            throw new HttpResponseException(message);
        }
    }
}