using System;
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
    public partial class Report03TH : System.Web.UI.Page
    {
        #region Parameter Report
        ParamsReport mParams = null;
        CDTotal model = new CDTotal();
        List<CDTotal> listObj = new List<CDTotal>();
        public void GetParamReport()
        {
            mParams = new ParamsReport();
            mParams.Report_code = Request.QueryString["Report_code"];
            switch (Request.QueryString["Report_code"].Trim())
            {
                #region TH Report
                case "TH03":
                    mParams.File_name = "RPT_CD04_NEW.rpt";
                    mParams.From_date = "";
                    mParams.To_date = "";
                    mParams.ViewType = 0;
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.Term_id = int.Parse(Request.QueryString["Term_id"]);
                    mParams.Month_id = int.Parse(Request.QueryString["Month_id"]);
                    mParams.Year_id = int.Parse(Request.QueryString["Year_id"]);
                    break;
                case "TH02":
                    mParams.File_name = "RPT_TH02.rpt";
                    mParams.From_date = "";
                    mParams.To_date = "";
                    mParams.ViewType = 0;
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.Term_id = int.Parse(Request.QueryString["Term_id"]);
                    mParams.Month_id = int.Parse(Request.QueryString["Month_id"]);
                    mParams.Year_id = int.Parse(Request.QueryString["Year_id"]);
                    break;
                case "TH01":
                    mParams.File_name = "RPT_TH01.rpt";
                    mParams.From_date = "";
                    mParams.To_date = "";
                    mParams.ViewType = 0;
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.Term_id = int.Parse(Request.QueryString["Term_id"]);
                    mParams.Month_id = int.Parse(Request.QueryString["Month_id"]);
                    mParams.Year_id = int.Parse(Request.QueryString["Year_id"]);
                    break;
                #endregion               
                default:
                    break;
            }
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //GetParamReport();
                btnPhanI.CssClass = "Clicked";
                btnPhanII.CssClass = "Initial";
                btnPhanIII.CssClass = "Initial";
                btnPage1.CssClass = "Clicked";
                btnPage2.CssClass = "Initial";
                btnPage3.CssClass = "Initial";
                MainView.ActiveViewIndex = 0;
                SubMain.ActiveViewIndex = 0;
            }
        }
               
        protected void btnPhanI_Click(object sender, EventArgs e)
        {
            btnPhanI.CssClass = "Clicked";
            btnPhanII.CssClass = "Initial";
            btnPhanIII.CssClass = "Initial";
            btnPage1.CssClass = "Clicked";
            btnPage2.CssClass = "Initial";
            btnPage3.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
            SubMain.ActiveViewIndex = 0;            
        }

        protected void btnPhanII_Click(object sender, EventArgs e)
        {
            //GetParamReport();
            btnPhanI.CssClass = "Initial";
            btnPhanII.CssClass = "Clicked";
            btnPhanIII.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
            //switch (mParams.Report_code.ToString().Trim())
            //{
            //    case "TH03":
            //        Show03TH(mParams);
            //        break;
            //    default:
            //        break;
            //}
        }

        protected void btnPhanIII_Click(object sender, EventArgs e)
        {
            //GetParamReport();
            btnPhanI.CssClass = "Initial";
            btnPhanII.CssClass = "Initial";
            btnPhanIII.CssClass = "Clicked";
            MainView.ActiveViewIndex = 2;
            //switch (mParams.Report_code.ToString().Trim())
            //{
            //    case "TH03":
            //        Show03TH(mParams);
            //        break;
            //    default:
            //        break;
            //}
        }

        protected void btnPage1_Click(object sender, EventArgs e)
        {
            //GetParamReport();
            btnPage1.CssClass = "Clicked";
            btnPage2.CssClass = "Initial";
            btnPage3.CssClass = "Initial";
            SubMain.ActiveViewIndex = 0;
            //switch (mParams.Report_code.ToString().Trim())
            //{
            //    case "TH03":
            //        Show03TH(mParams);
            //        break;
            //    default:
            //        break;
            //}
        }

        protected void btnPage2_Click(object sender, EventArgs e)
        {
            //GetParamReport();
            btnPage1.CssClass = "Initial";
            btnPage2.CssClass = "Clicked";
            btnPage3.CssClass = "Initial";
            SubMain.ActiveViewIndex = 1;
            //switch (mParams.Report_code.ToString().Trim())
            //{
            //    case "TH03":
            //        Show03TH(mParams);
            //        break;
            //    default:
            //        break;
            //}
        }

        protected void btnPage3_Click(object sender, EventArgs e)
        {
            //GetParamReport();
            btnPage1.CssClass = "Initial";
            btnPage2.CssClass = "Initial";
            btnPage3.CssClass = "Clicked";
            SubMain.ActiveViewIndex = 2;
            //switch (mParams.Report_code.ToString().Trim())
            //{
            //    case "TH03":
            //        Show03TH(mParams);
            //        break;
            //    default:
            //        break;
            //}
        }

        #endregion

        #region Get value       
        private List<CDTotal> Show03TH(ParamsReport mParams)
        {
            listObj = new List<CDTotal>();
            var rs = Helper.Invoke("GET", string.Format("api/Reports/Get03THTotal?id={0}&sFromDate={1}&sToDate={2}&PO_ID={3}", new object[] { 0, mParams.From_date, mParams.To_date, mParams.Po_ID }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue.ToList())
                {
                    model = new CDTotal();

                    model.Service_type = dyn.Service_type;
                    model.Group_type = dyn.Group_type;
                    model.Trans_type = dyn.Trans_type;
                    model.From_date = dyn.From_date;
                    model.To_date = dyn.To_date;
                    model.Po_Code = dyn.Po_Code;
                    model.Po_Name = dyn.Po_Name;
                    model.STT = dyn.STT;
                    model.font_bold_ph = dyn.font_bold_ph;
                    model.level_ph = dyn.level_ph;
                    model.Ma_thu = dyn.Ma_thu;
                    model.Ten_ma_thu = dyn.Ten_ma_thu;
                    model.thu_vnd = dyn.thu_vnd;
                    model.thu_usd = dyn.thu_usd;
                    model.thu_qd_vnd = dyn.thu_qd_vnd;
                    model.font_bold_tra = dyn.font_bold_tra;
                    model.level_tra = dyn.level_tra;
                    model.Ma_chi = dyn.Ma_chi;
                    model.Ten_ma_chi = dyn.Ten_ma_chi;
                    model.chi_vnd = dyn.chi_vnd;
                    model.chi_usd = dyn.chi_usd;
                    model.chi_qd_vnd = dyn.chi_qd_vnd;
                    model.Ty_gia = dyn.Ty_gia;
                    model.Chenh_lech_vnd = dyn.Chenh_lech_vnd;
                    model.Chenh_lech_usd = dyn.Chenh_lech_usd;

                    listObj.Add(model);
                }

            }

            return listObj;
        }

        #endregion
    }
}