using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cfm.Web.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              "Default",
              "{controller}/{action}/{id}",
              new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              new[] { "Cfm.Web.Mvc.Areas.Admin.Controllers" }
          ).DataTokens.Add("Area", "Admin");


            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
