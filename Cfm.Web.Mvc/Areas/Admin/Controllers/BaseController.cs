using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var session = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                var poSession = (PostOfficeViewModel)Session[Constant.PO_SESSION];

                if (session == null || poSession == null)
                {
                    ReturnActionLogin(ref filterContext);
                }

                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                RedirectToAction("Exception", "Error", new { area = "Admin", errorMsg = ex.Message });
            }
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

        public PostOfficeViewModel PoCurrent()
        {
            PostOfficeViewModel oCurrentPo = new PostOfficeViewModel();

            try
            {
                if (Session[Constant.PO_SESSION] != null)
                {
                    oCurrentPo = (PostOfficeViewModel)Session[Constant.PO_SESSION];
                }
                else
                {
                    oCurrentPo = null;
                }

            }
            catch
            {
                oCurrentPo = null;
            }

            return oCurrentPo;
        }

        public EmployeeViewModel UserCurrent()
        {
            EmployeeViewModel oCurrentUser = new EmployeeViewModel();

            try
            {
                if (Session[Constant.EMP_SESSION] != null)
                {
                    oCurrentUser = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                }
                else
                {
                    oCurrentUser = null;
                }

            }
            catch
            {
                oCurrentUser = null;
            }

            return oCurrentUser;
        }
    }
}