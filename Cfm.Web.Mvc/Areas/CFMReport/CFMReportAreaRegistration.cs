using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.CFMReport
{
    public class CFMReportAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CFMReport";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CFMReport_default",
                "CFMReport/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}