using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.CFMBranch.Controllers
{
    public class FunctionController : Controller
    {
        // GET: CFMBranch/Function
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ConfigPO()
        {
            return View();
        }

        public ActionResult AccountingBranch()
        {
            List<ReportListViewModel> listReport = new List<ReportListViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/Dictionary/GetReportList?id={0}&PageIndex = {1}&PageSize ={2}", new object[] { 0, 0, Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {

                foreach (dynamic dyn in rs.ListValue)
                {
                    var report = new ReportListViewModel()
                    {
                        Id = dyn.Id,
                        Code = dyn.Code,
                        Name = dyn.Name,
                        On_Moc = dyn.On_Moc,
                        On_Province_PO = dyn.On_Province_PO,
                        On_District_PO = dyn.On_District_PO,
                        On_PO = dyn.On_PO,
                        AllowCreateEntry = dyn.AllowCreateEntry,
                        OfficeManage = dyn.OfficeManage,
                        Description = dyn.Description,
                        ReportType = dyn.ReportType
                    };
                    if (!listReport.Contains(report))
                    {
                        listReport.Add(report);
                    }
                }
            }
            return PartialView(listReport);
        }

        public ActionResult StatisticGeneral()
        {
            List<ReportListViewModel> listReport = new List<ReportListViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/Dictionary/GetReportList?id={0}&PageIndex = {1}&PageSize ={2}", new object[] { 0, 0, Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {

                foreach (dynamic dyn in rs.ListValue)
                {
                    var report = new ReportListViewModel()
                    {
                        Id = dyn.Id,
                        Code = dyn.Code,
                        Name = dyn.Name,
                        On_Moc = dyn.On_Moc,
                        On_Province_PO = dyn.On_Province_PO,
                        On_District_PO = dyn.On_District_PO,
                        On_PO = dyn.On_PO,
                        AllowCreateEntry = dyn.AllowCreateEntry,
                        OfficeManage = dyn.OfficeManage,
                        Description = dyn.Description,
                        ReportType = dyn.ReportType
                    };
                    if (!listReport.Contains(report))
                    {
                        listReport.Add(report);
                    }
                }
            }
            return PartialView(listReport);
        }

        public ActionResult BusinessSupport()
        {
            return PartialView();
        }
    }
}