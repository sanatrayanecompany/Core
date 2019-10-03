using System.Web.Mvc;
using System.Web.Routing;

namespace Core.PSP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "AngularApp",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Core.PSP.Controllers" }
            );
        }
    }
}

