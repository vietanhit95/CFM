using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound(string errorPath)
        {
            ViewData["error_path"] = errorPath;
            return View();
        }

        public ActionResult AccessDenied(string action)
        {
            ViewData["action"] = action;
            return View();
        }

        public ActionResult Exception(string errorMsg)
        {
            ViewData["error_msg"] = errorMsg;
            return View();
        }
    }
}