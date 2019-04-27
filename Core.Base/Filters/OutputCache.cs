using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Core.Base
{
    public class OutputCache : ActionFilterAttribute
    {
        private int _minute;

        private int _clientMinute;

        private string _cacheKey;

        private bool _userCache;

        private static readonly ObjectCache WebApiCache = MemoryCache.Default;

        public OutputCache(int minute, bool userCache = true)
        {
            _minute = minute;

            _clientMinute = minute;

            _userCache = userCache;
        }

        private bool isCacheable(HttpActionContext actionContext)
        {
            return (_minute > 0 && _clientMinute > 0 && actionContext.Request.Method == HttpMethod.Get);
        }

        private CacheControlHeaderValue setClientCache()
        {
            CacheControlHeaderValue cacheControl = new CacheControlHeaderValue();
            cacheControl.MaxAge = TimeSpan.FromMinutes(_clientMinute);
            cacheControl.MustRevalidate = true;
            return cacheControl;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext != null)
            {
                if (isCacheable(actionContext))
                {
                    if (_userCache)
                    {
                        _cacheKey = string.Join(":", new string[] { actionContext.Request.RequestUri.AbsoluteUri, actionContext.Request.Headers.Authorization.Scheme.ToString() });
                    }
                    else
                    {
                        _cacheKey = actionContext.Request.RequestUri.AbsoluteUri;
                    }
                    if (WebApiCache.Contains(_cacheKey))
                    {
                        string val = (string)WebApiCache.Get(_cacheKey);
                        if (val != null)
                        {
                            actionContext.Response = actionContext.Request.CreateResponse();
                            actionContext.Response.Content = new StringContent(val);
                            MediaTypeHeaderValue contentType = (MediaTypeHeaderValue)WebApiCache.Get(_cacheKey + ":response-ct");
                            if (contentType == null)
                            {
                                contentType = new MediaTypeHeaderValue(_cacheKey.Split(':')[1]);
                            }
                            actionContext.Response.Content.Headers.ContentType = contentType;
                            actionContext.Response.Headers.CacheControl = setClientCache();
                            return;
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("actionContext");
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            if (!(WebApiCache.Contains(_cacheKey)))
            {
                var body = actionContext.Response.Content.ReadAsStringAsync().Result;
                WebApiCache.Add(_cacheKey, body, DateTime.Now.AddMinutes(_minute));
                WebApiCache.Add(_cacheKey + ":response-ct", actionContext.Response.Content.Headers.ContentType, DateTime.Now.AddMinutes(_minute));
            }
            if (isCacheable(actionContext.ActionContext))
            {
                actionContext.ActionContext.Response.Headers.CacheControl = setClientCache();
            }
        }
    }
}