using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfm.Web.Mvc.Areas.CFMReport.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Cfm.Web.Mvc.Common;

namespace Cfm.Web.Mvc.Areas.CFMReport.ReportView
{
    public partial class ReportView : System.Web.UI.Page
    {
        ReportDocument rd;
        StringBuilder StrHtmlGenerate = new StringBuilder();
        StringBuilder StrExport = new StringBuilder();
        ParamsReport mParams = null;
        CDTotal model = new CDTotal();
        CDDetail detail = new CDDetail();
        R_FundInfo fund = new R_FundInfo();
        List<CDTotal> listObj = new List<CDTotal>();
        List<CDDetail> listDetail = new List<CDDetail>();
        List<R_FundInfo> listFund = new List<R_FundInfo>();

        public void GetParamReport()
        {
            mParams = new ParamsReport();
            mParams.Report_code = Request.QueryString["Report_code"];
            switch (Request.QueryString["Report_code"].Trim())
            {
                #region CD Report
                case "CD04":
                    mParams.From_date = Request.QueryString["From_date"];
                    mParams.To_date = Request.QueryString["To_date"];
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.ViewType = int.Parse(Request.QueryString["ViewType"]);
                    if (Request.QueryString["ViewType"] == "0")
                    {
                        mParams.File_name = "RPT_CD04_NEW.rpt";
                    }
                    else
                    {
                        mParams.File_name = "RPT_CD04_CT.rpt";
                    }
                    mParams.Term_id = 0;
                    mParams.Month_id = 1;
                    mParams.Year_id = 2016;
                    break;
                case "CD03":
                    mParams.From_date = Request.QueryString["From_date"];
                    mParams.To_date = Request.QueryString["To_date"];
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.ViewType = int.Parse(Request.QueryString["ViewType"]);
                    if (Request.QueryString["ViewType"] == "0")
                    {
                        mParams.File_name = "RPT_CD03.rpt";
                    }
                    else
                    {
                        mParams.File_name = "RPT_CD03_CT.rpt";
                    }
                    mParams.Term_id = 0;
                    mParams.Month_id = 1;
                    mParams.Year_id = 2016;
                    break;
                case "CD03BC":
                    mParams.From_date = Request.QueryString["From_date"];
                    mParams.To_date = Request.QueryString["To_date"];
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.ViewType = int.Parse(Request.QueryString["ViewType"]);
                    if (Request.QueryString["ViewType"] == "0")
                    {
                        mParams.File_name = "RPT_CD03_BC.rpt";
                    }
                    else
                    {
                        mParams.File_name = "RPT_CD03_BC.rpt";
                    }
                    mParams.Term_id = 0;
                    mParams.Month_id = 1;
                    mParams.Year_id = 2016;
                    break;
                case "CD02":
                    mParams.From_date = Request.QueryString["From_date"];
                    mParams.To_date = Request.QueryString["To_date"];
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.ViewType = int.Parse(Request.QueryString["ViewType"]);
                    if (Request.QueryString["ViewType"] == "0")
                    {
                        mParams.File_name = "RPT_CD02.rpt";
                    }
                    else
                    {
                        mParams.File_name = "RPT_CD02_CT.rpt";
                    }
                    mParams.Term_id = 0;
                    mParams.Month_id = 1;
                    mParams.Year_id = 2016;
                    break;
                case "CD01":
                    mParams.From_date = Request.QueryString["From_date"];
                    mParams.To_date = Request.QueryString["To_date"];
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.ViewType = int.Parse(Request.QueryString["ViewType"]);
                    if (Request.QueryString["ViewType"] == "0")
                    {
                        mParams.File_name = "RPT_CD01.rpt";
                    }
                    else
                    {
                        mParams.File_name = "RPT_CD01_CT.rpt";
                    }
                    mParams.Term_id = 0;
                    mParams.Month_id = 1;
                    mParams.Year_id = 2016;
                    break;
                #endregion                
                #region TQ Report
                case "TQ04":
                    mParams.File_name = "RPT_TQ04.rpt";
                    mParams.From_date = Request.QueryString["From_date"];
                    mParams.To_date = Request.QueryString["To_date"];
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.ViewType = 0;
                    mParams.Term_id = 0;
                    mParams.Month_id = 1;
                    mParams.Year_id = 2016;
                    break;
                case "TQ03":
                    mParams.File_name = "RPT_TQ03.rpt";
                    mParams.From_date = Request.QueryString["From_date"];
                    mParams.To_date = Request.QueryString["To_date"];
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.ViewType = 0;
                    mParams.Term_id = 0;
                    mParams.Month_id = 1;
                    mParams.Year_id = 2016;
                    break;
                case "TQ02":
                    mParams.File_name = "RPT_TQ02.rpt";
                    mParams.From_date = Request.QueryString["From_date"];
                    mParams.To_date = Request.QueryString["To_date"];
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.ViewType = 0;
                    mParams.Term_id = 0;
                    mParams.Month_id = 1;
                    mParams.Year_id = 2016;
                    break;
                #endregion

                case "CD03FUND":
                    mParams.From_date = Request.QueryString["From_date"];
                    mParams.To_date = Request.QueryString["To_date"];
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.ViewType = 0;
                    mParams.File_name = "RPT_FUND_INFO_03.rpt";
                    mParams.Term_id = 0;
                    mParams.Month_id = 1;
                    mParams.Year_id = 2016;
                    break;
                default:
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgbPDF_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgbExcel_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgbWord_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnMucI_Click(object sender, EventArgs e)
        {
            rd = new ReportDocument();
            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Manager\\RPT_03TH_PI_1.rpt";
            rd.Load(strRptPath);
            crvReport.ReportSource = rd;
        }

        protected void btnMucII_Click(object sender, EventArgs e)
        {            
            rd = new ReportDocument();
            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Counter\\RPT_VAY_QUY.rpt";
            rd.Load(strRptPath);
            crvReport.ReportSource = rd;
        }

        protected void btnMucIII_Click(object sender, EventArgs e)
        {
            rd = new ReportDocument();
            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Manager\\RPT_03TH_PI_1.rpt";
            rd.Load(strRptPath);
            crvReport.ReportSource = rd;
        }
    }
}