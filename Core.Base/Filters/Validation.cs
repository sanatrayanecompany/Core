using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace Core.Base
{
    public class ValidationFilter : ActionFilterAttribute
    {
        private readonly bool _ShowErrorMessage;

        public ValidationFilter(bool ShowErrorMessage = true)
        {
            _ShowErrorMessage = ShowErrorMessage;
        }

        public override void OnActionExecuting(HttpActionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.BadRequest);

                List<string> errors = new List<string>();

                if (_ShowErrorMessage)
                {
                    foreach (ModelState val in context.ModelState.Values)
                    {
                        val.Errors.ToList().ForEach(x =>
                        {
                            errors.Add(x.ErrorMessage);
                        });
                    }
                    message.Content = new ObjectContent<Json>(new Json(null, false, errors.ToArray()), new JsonMediaTypeFormatter());
                }
                else
                    message.Content = null;

                throw new HttpResponseException(message);
            }
            base.OnActionExecuting(context);
        }
    }   
}