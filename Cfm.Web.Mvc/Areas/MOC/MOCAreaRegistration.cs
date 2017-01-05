using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.MOC
{
    public class MOCAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MOC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MOC_default",
                "MOC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}