using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.MOC.Controllers
{
    public class DictionaryController : Controller
    {
        // GET: MOC/Dictionary
        public ActionResult Index()
        {
            return View();
        }

        #region Danh mục báo cáo
        public ActionResult ReportList()
        {
            return View();
        }

        public ActionResult ReportCreate()
        {
            return PartialView("_ReportCreate");
        }

        [HttpPost]
        public ActionResult SaveReport()
        {
            return RedirectToAction("Index");
        }

        #endregion

    }
}