using System.Web.Mvc;

namespace Core.PSP.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{*url}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Core.PSP.Areas.Admin.Controllers" }
            );


        }
    }
}