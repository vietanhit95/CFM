using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Cfm.Web.Mvc.Models;

namespace Cfm.Web.Mvc.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowGeneric(string txtFromDate,string txtToDate, string txtPoCode, string txtMaBaoCao)
        {
            
            this.HttpContext.Session["pFromDate"] = txtFromDate;
            this.HttpContext.Session["pToDate"] = txtToDate;
            this.HttpContext.Session["pPOCode"] = txtPoCode;
            this.HttpContext.Session["pPOName"] = "";
            this.HttpContext.Session["pReportCode"] = txtMaBaoCao;
            switch(txtMaBaoCao)
            {
                case "CD04":
                this.HttpContext.Session["pFileName"] = "CFM_PO_CD04.rpt";
                this.HttpContext.Session["pDataSource"] = Get04CD();
                    break;

                default:
                    break;
            }


            return RedirectToAction("ShowReports", "GenericReport");
        }

        /// <summary>
        /// This is used for preprocess report data and next generic report called from java script block
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        [HttpPost]
        public void ShowReportNewWin(string txtFromDate,string txtToDate, string txtPoCode, string txtMaBaoCao)
        {
            this.HttpContext.Session["pFromDate"] = txtFromDate;
            this.HttpContext.Session["pToDate"] = txtToDate;
            this.HttpContext.Session["pPOCode"] = txtPoCode;
            this.HttpContext.Session["pPOName"] = "";
            this.HttpContext.Session["pReportCode"] = txtMaBaoCao;
            switch (txtMaBaoCao)
            {
                case "CD04":
                    this.HttpContext.Session["pFileName"] = "CFM_PO_CD04.rpt";
                    this.HttpContext.Session["pDataSource"] = Get04CD();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Get Data from CFM.API
        /// View demo Get04CD
        /// </summary>
        /// <returns></returns>
        private List<CFMReport> Get04CD()
        {
            return new List<CFMReport>() {
            new CFMReport(){Service_type=1,Group_type=1,Trans_type=1,From_date="06/10/2016",To_date="06/10/2016",Po_Code="871400", Po_Name="", font_bold=1,Ma_thu="I.Tiền mặt đầu kỳ",thu_vnd= 10000000,thu_usd= 100,thu_qd_vnd= 12000000,Ma_chi="1.Trả các dịch vụ",chi_vnd= 10000000,chi_usd= 100,chi_qd_vnd= 12000000,Ty_gia=20000},
            new CFMReport(){Service_type=1,Group_type=1,Trans_type=1,From_date="06/10/2016",To_date="06/10/2016",Po_Code="871406", Po_Name="", font_bold=1,Ma_thu="1.1 Tại bưu cục",thu_vnd=10000000,thu_usd=100,thu_qd_vnd=12000000,Ma_chi="1.Trả các dịch vụ",chi_vnd=10000000,chi_usd=100,chi_qd_vnd=12000000,Ty_gia=20000}
            };
        }
    }
}