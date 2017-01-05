using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.CFMBranch
{
    public class CFMBranchAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CFMBranch";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CFMBranch_default",
                "CFMBranch/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}