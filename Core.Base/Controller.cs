using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace Core.Base
{
    public abstract partial class _Controller : ApiController
    {
        private IToken _token;

        public _Controller()
        {
            _token = new Token(); // Resolver.GetType<IToken>();

            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;

            if (request.HttpMethod == HttpMethod.Options.Method)
            {
                response.AppendHeader("Access-Control-Allow-Origin", Host.Config.Origin.AccessControlAllowOrigin);
                response.AppendHeader("Access-Control-Allow-Methods", Host.Config.Origin.AccessControlAllowMethods);
                response.AppendHeader("Access-Control-Allow-Headers", Host.Config.Origin.AccessControlAllowHeaders);
                response.AppendHeader("Access-Control-Allow-Credentials",
                    Host.Config.Origin.AccessControlAllowCredentials);

                response.SuppressContent = true;
                response.StatusCode = (int) HttpStatusCode.OK;
                response.Flush();
            }
            else
            {
                response.StatusCode = (int) HttpStatusCode.OK;
                response.AppendHeader("Access-Control-Allow-Origin", Host.Config.Origin.AccessControlAllowOrigin);
                response.AppendHeader("Access-Control-Expose-Headers", "Authorization");
                response.AppendHeader("Access-Control-Allow-Credentials", "true");
            }
        }

        protected T GetCurrentUser<T>() where T : IPayLoad
        {
            return GetCurrentUser<T>("");
        }

        protected T GetCurrentWebUser<T>() where T : IPayLoad
        {
            return GetCurrentWebUser<T>("");
        }

        protected T GetCurrentUser<T>(string token) where T : IPayLoad
        {
            return _token.Decode<T>(token);
        }

        protected T GetCurrentWebUser<T>(string token) where T : IPayLoad
        {
            return _token.Decode<T>(token);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}