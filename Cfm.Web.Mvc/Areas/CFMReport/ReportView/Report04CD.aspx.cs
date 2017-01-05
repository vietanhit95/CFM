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
    public partial class Report04CD : System.Web.UI.Page
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
        public int iTabIndx = 1;

        public void GetParamReport(int iTab)
        {
            mParams = new ParamsReport();
            mParams.Report_code = Request.QueryString["Report_code"];
            switch (Request.QueryString["Report_code"].Trim())
            {
                case "CD04":
                    mParams.From_date = Request.QueryString["From_date"];
                    mParams.To_date = Request.QueryString["To_date"];
                    mParams.Po_ID = int.Parse(Request.QueryString["PO_ID"]);
                    mParams.ViewType = int.Parse(Request.QueryString["ViewType"]);
                    if (Request.QueryString["ViewType"] == "0")
                    {
                        switch (iTab)
                        {
                            case 1:
                                mParams.File_name = "RPT_CD04_1.rpt";
                                break;
                            case 2:
                                mParams.File_name = "RPT_CD04_2.rpt";
                                break;
                            case 3:
                                mParams.File_name = "RPT_CD04_3.rpt";
                                break;
                            default:
                                mParams.File_name = "RPT_CD04_1.rpt";
                                break;
                        }
                    }
                    else
                    {
                        mParams.File_name = "RPT_CD04_CT.rpt";
                    }
                    mParams.Term_id = 0;
                    mParams.Month_id = 1;
                    mParams.Year_id = 2016;
                    break;                
                default:
                    break;
            }
            mParams.Tab_Indx = iTab;
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                Session["sTabIndx"] = 1;
                GetParamReport(1);
                Show04CD(mParams);
            }
        }

        protected void imgbPDF_Click(object sender, ImageClickEventArgs e)
        {
            iTabIndx = int.Parse(Session["sTabIndx"].ToString());
            GetParamReport(iTabIndx);
            GenericPDFFile(mParams);
        }

        protected void imgbExcel_Click(object sender, ImageClickEventArgs e)
        {
            iTabIndx = int.Parse(Session["sTabIndx"].ToString());
            GetParamReport(iTabIndx);
            GenericExcelFile(mParams);
        }

        protected void imgbWord_Click(object sender, ImageClickEventArgs e)
        {
            iTabIndx = int.Parse(Session["sTabIndx"].ToString());
            GetParamReport(iTabIndx);
            GenericWordFile(mParams);
        }

        protected void btnMucI_Click(object sender, EventArgs e)
        {
            iTabIndx = 1;
            Session["sTabIndx"] = 1;
            GetParamReport(1);
            Show04CD(mParams);
            this.Title = "Mục I - Tổng hợp cân đối thu chi";
        }

        protected void btnMucII_Click(object sender, EventArgs e)
        {
            iTabIndx = 2;
            Session["sTabIndx"] = 2;
            GetParamReport(2);
            Show04CD(mParams);
            this.Title = "Mục II - Chi tiết thông tin các dịch vụ";
        }

        protected void btnMucIII_Click(object sender, EventArgs e)
        {
            iTabIndx = 3;
            Session["sTabIndx"] = 3;
            GetParamReport(3);
            Show04CD(mParams);
            this.Title = "Mục III - Một số thông tin chi tiết tham khảo";
        }

        public void Show04CD(ParamsReport mParams)
        {
            bool isValid = true;
            if (mParams.ViewType == 0)
            {
                #region Báo cáo tổng hợp
                var pDataSource = Get04Total(mParams);
                if (string.IsNullOrEmpty(mParams.File_name.ToString()))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    rd = new ReportDocument();                    
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Counter\\" + mParams.File_name.ToString();
                    rd.Load(strRptPath);
                    if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                        rd.SetDataSource(pDataSource);
                    if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                        rd.SetParameterValue("paraTime", "Ngày " + mParams.From_date.ToString());
                    crvReport.ReportSource = rd;
                }
                else
                {
                    Response.Write("<H4>Nothing Found; No Report name found</H4>");
                }
                #endregion
            }
        }

        private List<CDTotal> Get04Total(ParamsReport mParams)
        {
            listObj = new List<CDTotal>();
            var rs = Helper.Invoke("GET", string.Format("api/Reports/Get04CDTotal?id={0}&sFromDate={1}&sToDate={2}&iPO_ID={3}", new object[] { 0, mParams.From_date, mParams.To_date, mParams.Po_ID }), null);
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
                    model.Parent_Code = dyn.Parent_Code;
                    model.Parent_Name = dyn.Parent_Name;
                    model.STT = dyn.STT;
                    model.font_bold_ph = dyn.font_bold_ph;
                    model.level_ph = dyn.level_ph;
                    model.CFM_Code_thu = dyn.CFM_Code_thu;
                    model.Ma_thu = dyn.Ma_thu;
                    model.Ten_ma_thu = dyn.Ten_ma_thu;
                    model.thu_vnd = dyn.thu_vnd;
                    model.thu_usd = dyn.thu_usd;
                    model.thu_qd_vnd = dyn.thu_qd_vnd;
                    model.font_bold_tra = dyn.font_bold_tra;
                    model.level_tra = dyn.level_tra;
                    model.CFM_Code_chi = dyn.CFM_Code_chi;
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

        private void GenericPDFFile(ParamsReport mParams)
        {
            bool isValid = true;
            switch (mParams.Report_code.Trim())
            {
                case "CD04":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    break;
                default:
                    break;
            }
            if (string.IsNullOrEmpty(mParams.File_name.ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Counter\\" + mParams.File_name.ToString();
                rd.Load(strRptPath);
                if (mParams.ViewType == 0)
                {
                    if (listObj != null && listObj.GetType().ToString() != "System.String")
                        rd.SetDataSource(listObj);
                }
                else
                {
                    if (listDetail != null && listDetail.GetType().ToString() != "System.String")
                        rd.SetDataSource(listDetail);
                }
                if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "PDF File");
            }
            else
            {
                Response.Write("<H2>Nothing Found; No Report name found</H2>");
            }
        }

        private void GenericExcelFile(ParamsReport mParams)
        {
            bool isValid = true;
            switch (mParams.Report_code.Trim())
            {
                case "CD04":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    break;
                default:
                    break;
            }
            if (string.IsNullOrEmpty(mParams.File_name.ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Counter\\" + mParams.File_name.ToString();
                rd.Load(strRptPath);
                if (mParams.ViewType == 0)
                {
                    if (listObj != null && listObj.GetType().ToString() != "System.String")
                        rd.SetDataSource(listObj);
                }
                else
                {
                    if (listDetail != null && listDetail.GetType().ToString() != "System.String")
                        rd.SetDataSource(listDetail);
                }
                if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());

                rd.ExportToHttpResponse(ExportFormatType.Excel, System.Web.HttpContext.Current.Response, false, "Excel File");
            }
            else
            {
                Response.Write("<H2>Nothing Found; No Report name found</H2>");
            }
        }

        private void GenericWordFile(ParamsReport mParams)
        {
            bool isValid = true;
            switch (mParams.Report_code.Trim())
            {
                case "CD04":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }                   
                    break;               
                default:
                    break;
            }
            if (string.IsNullOrEmpty(mParams.File_name.ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Counter\\" + mParams.File_name.ToString();
                rd.Load(strRptPath);
                if (mParams.ViewType == 0)
                {
                    if (listObj != null && listObj.GetType().ToString() != "System.String")
                        rd.SetDataSource(listObj);
                }
                else
                {
                    if (listDetail != null && listDetail.GetType().ToString() != "System.String")
                        rd.SetDataSource(listDetail);
                }
                if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());

                rd.ExportToHttpResponse(ExportFormatType.WordForWindows, System.Web.HttpContext.Current.Response, false, "Word File");
            }
            else
            {
                Response.Write("<H2>Nothing Found; No Report name found</H2>");
            }
        }
    }
}