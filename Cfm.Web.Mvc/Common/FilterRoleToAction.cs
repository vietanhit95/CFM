using Cfm.Web.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cfm.Web.Mvc.Common
{
    public class FilterRoleToAction : ActionFilterAttribute
    {
        public int[] Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                PostOfficeViewModel oPo = (PostOfficeViewModel)filterContext.HttpContext.Session.Contents[Constant.PO_SESSION];
                if (!Role.Contains(oPo.POLevel))
                {
                    ReturnActionLogin(ref filterContext);
                }

                //int[] RoleArea = new int[]
                //{
                //    (int)Constant.POLevel.Center,
                //    (int)Constant.POLevel.Branch,
                //    (int)Constant.POLevel.Counter,
                //    (int)Constant.POLevel.District
                //};


                //    var Areas = filterContext.RouteData.DataTokens["Area"].ToString().ToUpper();

                //    if (Areas == Constant.Areas.Admin.ToString().ToUpper())
                //    {
                //        if (!RoleArea.Intersect(Role).Any())
                //        {
                //            ReturnActionLogin(ref filterContext);
                //        }
                //    }
                //    else if (Areas == Constant.Areas.CFMBranch.ToString().ToUpper())
                //    {
                //        if (!RoleArea.Contains((int)Constant.POLevel.Branch))
                //        {
                //            ReturnActionLogin(ref filterContext);
                //        }
                //    }
                //    else if (Areas == Constant.Areas.CFMCounter.ToString().ToUpper())
                //    {
                //        if (!RoleArea.Contains((int)Constant.POLevel.Counter))
                //        {
                //            ReturnActionLogin(ref filterContext);
                //        }
                //    }
                //    else if (Areas == Constant.Areas.CFMDistrict.ToString().ToUpper())
                //    {
                //        if (!RoleArea.Contains((int)Constant.POLevel.District))
                //        {
                //            ReturnActionLogin(ref filterContext);
                //        }
                //    }
            }
            catch
            {

            }
            base.OnActionExecuting(filterContext);
        }

        private void ReturnActionLogin(ref ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "User",
                action = "Login",
                area = "Admin",
                returnUrl = filterContext.HttpContext.Request.RawUrl
            }));
        }
    }

    public class AuthoritiesFunc : ActionFilterAttribute
    {
        public int[] FunctionRole { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContextAccessDenied)
        {
            try
            {
                EmployeeViewModel oEmp = (EmployeeViewModel)filterContextAccessDenied.HttpContext.Session.Contents[Constant.EMP_SESSION];
                if(!oEmp.ListRole.Any(x => FunctionRole.Contains(x.Id)))
                {
                    ReturnActionAccessDenied(ref filterContextAccessDenied);
                }
            }
            catch
            {

            }

            base.OnActionExecuting(filterContextAccessDenied);
        }

        private void ReturnActionAccessDenied(ref ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "Home",
                action = "AccessDenied",
                area = "Admin",
                returnUrl = filterContext.HttpContext.Request.RawUrl
            }));
        }
    }
}