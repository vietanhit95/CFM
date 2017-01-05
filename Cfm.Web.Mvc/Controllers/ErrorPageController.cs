using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Error404()
        {
            return View();
        } 
    }
}