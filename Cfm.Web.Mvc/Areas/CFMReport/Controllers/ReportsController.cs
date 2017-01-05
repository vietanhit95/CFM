using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cfm.Web.Mvc.Areas.CFMReport.Models;
using Cfm.Web.Mvc.Common;
using Cfm.Web.Mvc.Areas.Admin.Models;

namespace Cfm.Web.Mvc.Areas.CFMReport.Controllers
{
    public class ReportsController : Controller
    {
        // GET: CFMReport/Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowGeneric(string txtFromDate, string txtToDate, int iPO_ID, string txtMaBaoCao)
        {
            ParamsReport param = new ParamsReport();
            param.Report_code = txtMaBaoCao;
            param.Term_id = 0;
            param.Year_id = 0;
            param.From_date = txtFromDate;
            param.To_date = txtToDate;
            param.Po_ID = iPO_ID;
            switch (txtMaBaoCao)
            {
                case "CD04":
                    param.File_name = "RPT_CD04.rpt";
                    break;
                case "CD03":
                    break;

                default:
                    break;
            }
            TempData["dt"] = param;
            return RedirectToAction("ShowReports", "GenericReport");
        }

        [HttpPost]
        public void ShowReportAsp(string txtFromDate, string txtToDate, int iPO_ID, string txtMaBaoCao)
        {
            Response.Redirect("~/ReportView/ReportViewer.aspx?Report_code=" + Server.UrlEncode(txtMaBaoCao) + "&From_date=" + Server.UrlEncode(txtFromDate) + "&To_date=" + Server.UrlEncode(txtToDate) + "&PO_ID=" + Server.UrlEncode(iPO_ID.ToString()));
        }

        [HttpPost]
        public void ShowReportNewWin(string txtFromDate, string txtToDate, string txtPoCode, string txtMaBaoCao)
        {
            this.HttpContext.Session["pFromDate"] = txtFromDate;
            this.HttpContext.Session["pToDate"] = txtToDate;
            this.HttpContext.Session["pPOCode"] = txtPoCode;
            this.HttpContext.Session["pPOName"] = "";
            this.HttpContext.Session["pReportCode"] = txtMaBaoCao;
            switch (txtMaBaoCao)
            {
                case "CD04":
                    this.HttpContext.Session["pFileName"] = "RPT_CD04.rpt";
                    break;

                default:
                    break;
            }
        }

    }
}