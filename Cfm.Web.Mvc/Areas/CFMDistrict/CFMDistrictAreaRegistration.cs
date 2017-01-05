using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.CFMDistrict
{
    public class CFMDistrictAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CFMDistrict";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CFMDistrict_default",
                "CFMDistrict/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}