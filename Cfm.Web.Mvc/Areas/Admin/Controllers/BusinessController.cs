using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center })]
    public class BusinessController : BaseController
    {
        // GET: Admin/Business
        public ActionResult Index()
        {
            return View();
        }
    }
}