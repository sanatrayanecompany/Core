using System.Web.Http;

namespace Core.PSP
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            name: "API Area Default",
            routeTemplate: "api/Admin/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
        }
    }
}
