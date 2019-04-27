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
            _token = new Token();// Resolver.GetType<IToken>();

            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;

            if (request.HttpMethod == HttpMethod.Options.Method)
            {
                response.AppendHeader("Access-Control-Allow-Origin", Host.Config.Origin.AccessControlAllowOrigin);
                response.AppendHeader("Access-Control-Allow-Methods", Host.Config.Origin.AccessControlAllowMethods);
                response.AppendHeader("Access-Control-Allow-Headers", Host.Config.Origin.AccessControlAllowHeaders);
                response.AppendHeader("Access-Control-Allow-Credentials", Host.Config.Origin.AccessControlAllowCredentials);

                response.SuppressContent = true;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Flush();
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.AppendHeader("Access-Control-Allow-Origin", Host.Config.Origin.AccessControlAllowOrigin);
                response.AppendHeader("Access-Control-Expose-Headers", "Authorization");
                response.AppendHeader("Access-Control-Allow-Credentials", "true");
            }
        }

        protected T GetCurrentUser<T>() where T : IPayLoad
        {
            return GetCurrentUser<T>("");
            //"eyJhbGciOiJBMTI4S1ciLCJlbmMiOiJBMTI4Q0JDLUhTMjU2IiwidHlwIjoiSldUIn0.bdwsUvvIhG21fhuX7_qNBJCEWY2tmjvX5r70_n7FuE5lX1vNGSK19A.NMRQ-lc0LKKGjESZdSQnGg.gBy9wJ0gc8SHHYt74lAFsqeVzTF8Qafzkn3yeUKYwChIt_KAdNqKRNnVmNu1bvFA38fzFzJPmqnM4UYscVDuqhU-_9iqgZBFNUUnyjDmEflr6QcUrtH3P4y02PDkI6dHRN97QgJN1Xi2tQ9G77EGk7gMJZ4E7hKytgIK9arW_mRuV2o4dh4MsUjvb5tW8sva6I_1eV8_8rzsd0ny8KvOEOuHAP9YuyQzgGJtdRa4afiWE-mjb5bgDTlwk_CUlw5k0iwwZ8B5TVGtqepKi8orNy7UlqiY4ZmE-uSJUAqfQYtLmeAeicsutLkWUESx3gdmOE5GkqKIwIEhQzQQXiSgtwRLC7B6W5v3W0yXOXQZufWPz8hDtqhiITQjsa4qO22H5gUJCHPIJQ7EXQlri0ie895_Ie0ZaIS4An6tWF5CATd72Glg4CUH1OeCQ_C9eAA0xAzNEppEjFaDMFvpHPemdE3xgFewjdwBLR8i3wGO93S7lfg83rLd3dMTKeKeT9tCp85uMS8-hKXV_gKss52w6esHU32tzIfgmgcLOWCbflISCG3oZ_NoqtU9fI6waidIEdIrn_BgB4JATAOA8VZbIQ.CVQ3KZYe-oseYSR7KUSMTw");
        }
        protected T GetCurrentWebUser<T>() where T : IPayLoad
        {
            return GetCurrentWebUser<T>("");
            //"eyJhbGciOiJBMTI4S1ciLCJlbmMiOiJBMTI4Q0JDLUhTMjU2IiwidHlwIjoiSldUIn0.bdwsUvvIhG21fhuX7_qNBJCEWY2tmjvX5r70_n7FuE5lX1vNGSK19A.NMRQ-lc0LKKGjESZdSQnGg.gBy9wJ0gc8SHHYt74lAFsqeVzTF8Qafzkn3yeUKYwChIt_KAdNqKRNnVmNu1bvFA38fzFzJPmqnM4UYscVDuqhU-_9iqgZBFNUUnyjDmEflr6QcUrtH3P4y02PDkI6dHRN97QgJN1Xi2tQ9G77EGk7gMJZ4E7hKytgIK9arW_mRuV2o4dh4MsUjvb5tW8sva6I_1eV8_8rzsd0ny8KvOEOuHAP9YuyQzgGJtdRa4afiWE-mjb5bgDTlwk_CUlw5k0iwwZ8B5TVGtqepKi8orNy7UlqiY4ZmE-uSJUAqfQYtLmeAeicsutLkWUESx3gdmOE5GkqKIwIEhQzQQXiSgtwRLC7B6W5v3W0yXOXQZufWPz8hDtqhiITQjsa4qO22H5gUJCHPIJQ7EXQlri0ie895_Ie0ZaIS4An6tWF5CATd72Glg4CUH1OeCQ_C9eAA0xAzNEppEjFaDMFvpHPemdE3xgFewjdwBLR8i3wGO93S7lfg83rLd3dMTKeKeT9tCp85uMS8-hKXV_gKss52w6esHU32tzIfgmgcLOWCbflISCG3oZ_NoqtU9fI6waidIEdIrn_BgB4JATAOA8VZbIQ.CVQ3KZYe-oseYSR7KUSMTw");
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
