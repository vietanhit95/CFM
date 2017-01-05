using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.CFMCounter
{
    public class CFMCounterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CFMCounter";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CFMCounter_default",
                "CFMCounter/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}