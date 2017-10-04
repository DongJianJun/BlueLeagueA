using System.Web.Mvc;

namespace NETApi.Areas.Admin_Areas
{
    public class Admin_AreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin_Areas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Areas_default",
                "Admin_Areas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
