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
    public partial class ReportViewer : System.Web.UI.Page
    {
        #region Parameter Report
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

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    GetParamReport();
                    switch (mParams.Report_code.ToString().Trim())
                    {
                        case "CD04":
                            Show04CD(mParams);
                            break;
                        case "CD03":
                            Show03CD(mParams);
                            break;
                        case "CD03BC":
                            Show03CDBC(mParams);
                            break;
                        case "CD02":
                            Show02CD(mParams);
                            break;                       
                        case "CD01":
                            Show01CD(mParams);
                            break;
                        case "TQ04":
                            Show04TQ(mParams);
                            break;
                        case "TQ03":
                            Show03TQ(mParams);
                            break;
                        case "TQ02":
                            Show02TQ(mParams);
                            break;
                        case "CD03FUND":
                            ShowCD03FUND(mParams);
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.Message);
                }
            }
        }

        public void imgbPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                #region Download Exist file extension
                //string sFile = System.Web.HttpContext.Current.Server.MapPath("~/ ") + "Areas\\CFMReport\\Rpts\\Counter\\CP.pdf";
                //Download_File(sFile);
                #endregion

                #region Export Report

                //ExportOptions crExportOpt;
                //DiskFileDestinationOptions crDiskFileDestinationOptions = new DiskFileDestinationOptions();
                //PdfFormatOptions crFormatTypeOptions = new PdfFormatOptions();
                //crDiskFileDestinationOptions.DiskFileName = "c:\\CD04.pdf";
                //crExportOpt = rd.ExportOptions;
                //{
                //    crExportOpt.ExportDestinationType = ExportDestinationType.DiskFile;
                //    crExportOpt.ExportFormatType = ExportFormatType.PortableDocFormat;
                //    crExportOpt.DestinationOptions = crDiskFileDestinationOptions;
                //    crExportOpt.FormatOptions = crFormatTypeOptions;
                //}
                //rd.Export();

                #endregion

                GetParamReport();
                GenericPDFFile(mParams);
            }
            catch (Exception )
            {
            }
        }

        protected void imgbExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GetParamReport();
                GenericExcelFile(mParams);
            }
            catch (Exception )
            {
            }
        }

        protected void imgbWord_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GetParamReport();
                GenericWordFile(mParams);
            }
            catch (Exception)
            {
            }
        }

        private void ReportMessage(string message)
        {
            //Response.Write("<div id="+"CFMMessage"+" class="+"bootbox modal fade in"+" role="+"dialog"+"><div class="+"modal - dialog modal - sm"+" role="+"document"+"><div class="+"modal - content"+">");
            //Response.Write("<div class="+"modal - header"+" style="+ "padding: 3px !important; background - color:#3498DB"+">");
            //Response.Write("<button type="+"button"+" style="+"margin - right:5px; margin - top:8px;"+" class="+"close"+" data-dismiss="+"modal"+" aria-hidden="+"true"+">×</button>");
            //Response.Write("<h4 style="+"color:#fff; margin-left:10px;"+">Thông báo</h4>");
            //Response.Write("</div><div class="+"modal - body"+ ">Không có dữ liệu báo cáo</div>");
            //Response.Write("<div class="+"modal - footer"+">");
            //Response.Write("<button type="+"button"+" class="+"btn btn - small btn - success"+" data - dismiss="+"modal"+" >OK</button>");
            //Response.Write("</div></div>");
            //Response.Write("<script type = 'text/javascript'>");
            //Response.Write("function(){");
            //Response.Write("$("+"#CFMMessage"+").modal('show');");
            //Response.Write("</script>");

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }
        
        private void ErrorMessage(string sError)
        {
            Response.Write("<H3>Xảy ra lỗi trong quá trình xử lý!</H3><hr/> ");
            Response.Write("<H4>ParamReport:</H4>");
            Response.Write("<div>ReportCode : " + mParams.Report_code + "</div>");
            Response.Write("<div>FromDate : " + mParams.From_date + "</div>");
            Response.Write("<div>ToDate : " + mParams.To_date + "</div>");
            Response.Write("<div>ViewType : " + mParams.ViewType + "</div>");
            Response.Write("<div>POID : " + mParams.Po_ID + "</div>");
            Response.Write("<div>TeamId : " + mParams.Term_id + "</div>");
            Response.Write("<div>MonthID : " + mParams.Month_id + "</div>");
            Response.Write("<div>YearId : " + mParams.Year_id + "</div><hr/>");
            Response.Write("<H4>Chi tiết:</H4>");
            Response.Write(sError.ToString());
            Response.Write("<hr/>");
        }
        #endregion

        #region Show Report
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
            else
            {
                #region Báo cáo chi tiết
                var pDataSource = Get04Detail(mParams);
                if (string.IsNullOrEmpty(mParams.File_name.ToString()))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Counter\\" + mParams.File_name.ToString();
                    rd.Load(strRptPath);
                    if (pDataSource.Count() > 0)
                    {
                        rd.SetDataSource(pDataSource);
                        if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                            rd.SetParameterValue("paraTime", "Ngày " + mParams.From_date.ToString());
                        crvReport.ReportSource = rd;
                    }
                    else
                    {                        
                        ReportMessage("Không có dữ liệu báo cáo.");
                    }
                }
                else
                {
                    ReportMessage("Không tồn tại file báo cáo.");
                }
                #endregion
            }
        }

        public void Show03CD(ParamsReport mParams)
        {
            bool isValid = true;
            if (mParams.ViewType == 0)
            {
                #region Báo cáo tổng hợp
                var pDataSource = Get03Total(mParams);
                if (string.IsNullOrEmpty(mParams.File_name.ToString()))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Manager\\" + mParams.File_name.ToString();
                    rd.Load(strRptPath);
                    if (pDataSource.Count() > 0)
                    {
                        rd.SetDataSource(pDataSource);
                        if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                            rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                        crvReport.ReportSource = rd;
                    }
                    else
                    {
                        ReportMessage("Không có dữ liệu báo cáo.");
                    }
                }
                else
                {
                    ReportMessage("Không tồn tại file báo cáo.");
                }
                #endregion
            }
            else
            {
                #region Báo cáo chi tiết
                var pDataSource = Get03Detail(mParams);
                if (string.IsNullOrEmpty(mParams.File_name.ToString()))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Manager\\" + mParams.File_name.ToString();
                    rd.Load(strRptPath);
                    if (pDataSource.Count() > 0)
                    {
                        rd.SetDataSource(pDataSource);
                        if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                            rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                        crvReport.ReportSource = rd;
                    }
                    else
                    {
                        ReportMessage("Không có dữ liệu báo cáo.");
                    }
            }
                else
                {
                    ReportMessage("Không tồn tại file báo cáo.");
                }
                #endregion
            }
        }

        public void Show03CDBC(ParamsReport mParams)
        {
            bool isValid = true;
            if (mParams.ViewType == 0)
            {
                #region Báo cáo tổng hợp
                var pDataSource = Get03CDBC(mParams);
                if (string.IsNullOrEmpty(mParams.File_name.ToString()))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Manager\\" + mParams.File_name.ToString();
                    rd.Load(strRptPath);
                    if (pDataSource.Count() > 0)
                    {
                        rd.SetDataSource(pDataSource);
                        if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                            rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                        crvReport.ReportSource = rd;
                    }
                    else
                    {
                        ReportMessage("Không có dữ liệu báo cáo.");
                    }
            }
                else
                {
                    ReportMessage("Không tồn tại file báo cáo.");
                }
                #endregion
            }           
        }
        
        public void Show02CD(ParamsReport mParams)
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
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Manager\\" + mParams.File_name.ToString();
                    rd.Load(strRptPath);
                    if (pDataSource.Count() > 0)
                    {
                        rd.SetDataSource(pDataSource);
                        if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                            rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                        crvReport.ReportSource = rd;
                    }
                    else
                    {
                        ReportMessage("Không có dữ liệu báo cáo.");
                    }
            }
                else
                {
                    ReportMessage("Không tồn tại file báo cáo.");
                }
                #endregion
            }
            else
            {
                #region Báo cáo chi tiết
                var pDataSource = Get04Total(mParams);

                if (string.IsNullOrEmpty(mParams.File_name.ToString()))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Manager\\" + mParams.File_name.ToString();
                    rd.Load(strRptPath);
                    if (pDataSource.Count() > 0)
                    {
                        rd.SetDataSource(pDataSource);
                        if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                            rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                        crvReport.ReportSource = rd;
                    }
                else
                    {
                        ReportMessage("Không có dữ liệu báo cáo.");
                    }
                }
                else
                {
                    ReportMessage("Không tồn tại file báo cáo.");
                }
                #endregion
            }
        }

        public void Show01CD(ParamsReport mParams)
        {
            bool isValid = true;
            // Call API function
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
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Center\\" + mParams.File_name.ToString();
                    rd.Load(strRptPath);
                    if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                        rd.SetDataSource(pDataSource);
                    //if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                    //    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                    crvReport.ReportSource = rd;
                }
                else
                {
                    ReportMessage("Không tồn tại file báo cáo.");
                }
                #endregion
            }
            else
            {
                #region Báo cáo chi tiết
                var pDataSource = Get04Total(mParams);

                if (string.IsNullOrEmpty(mParams.File_name.ToString()))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Center\\" + mParams.File_name.ToString();
                    rd.Load(strRptPath);
                    if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                        rd.SetDataSource(pDataSource);
                    //if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                    //    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                    crvReport.ReportSource = rd;
                }
                else
                {
                    ReportMessage("Không tồn tại file báo cáo.");
                }
                #endregion
            }
        }

        public void Show04TQ(ParamsReport mParams)
        {
            bool isValid = true;
            // Call API function
            var pDataSource = Get04TQ(mParams);

            if (string.IsNullOrEmpty(mParams.File_name.ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Counter\\" + mParams.File_name.ToString();
                rd.Load(strRptPath);
                if (pDataSource.Count() > 0)
                {
                    rd.SetDataSource(pDataSource);
                    if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                        rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                    crvReport.ReportSource = rd;
                }
                else
                {
                }
            }
            else
            {
                ReportMessage("Không tồn tại file báo cáo.");
            }
        }

        public void Show03TQ(ParamsReport mParams)
        {
            bool isValid = true;
            // Call API function
            var pDataSource = Get04Total(mParams);

            if (string.IsNullOrEmpty(mParams.File_name.ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Manager\\" + mParams.File_name.ToString();
                rd.Load(strRptPath);
                if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                    rd.SetDataSource(pDataSource);
                //if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                //    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                //rd.SetParameterValue("paraBC", mParams.Po_code.ToString() + " - " + mParams.Po_name.ToString());
                crvReport.ReportSource = rd;
            }
            else
            {
                ReportMessage("Không tồn tại file báo cáo.");
            }
        }

        public void Show02TQ(ParamsReport mParams)
        {
            bool isValid = true;
            // Call API function
            var pDataSource = Get04Total(mParams);

            if (string.IsNullOrEmpty(mParams.File_name.ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Manager\\" + mParams.File_name.ToString();
                rd.Load(strRptPath);
                if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                    rd.SetDataSource(pDataSource);
                //if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                //    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                //rd.SetParameterValue("paraBC", mParams.Po_code.ToString() + " - " + mParams.Po_name.ToString());
                crvReport.ReportSource = rd;
            }
            else
            {
                ReportMessage("Không tồn tại file báo cáo.");
            }
        }

        public void ShowCD03FUND(ParamsReport mParams)
        {
            bool isValid = true;
            if (mParams.ViewType == 0)
            {
                #region Báo cáo tổng hợp
                var pDataSource = GetCD03FUND(mParams);
                if (string.IsNullOrEmpty(mParams.File_name.ToString()))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReport\\Rpts\\Manager\\" + mParams.File_name.ToString();
                    rd.Load(strRptPath);
                    if (pDataSource.Count() > 0)
                    {
                        rd.SetDataSource(pDataSource);
                        if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                            rd.SetParameterValue("paraTime", "Ngày " + mParams.From_date.ToString());
                        crvReport.ReportSource = rd;
                    }
                    else
                    {
                        ReportMessage("Không có dữ liệu báo cáo.");
                    }
                }
                else
                {
                    ReportMessage("Không tồn tại file báo cáo.");
                }
                #endregion
            }
        }
        #endregion

        #region Download File & Export File
        private void Download_File(string FilePath)
        {
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FilePath));
            Response.WriteFile(FilePath);
            Response.End();
        }

        private void HTMLToExcel()
        {
            #region Export HTML to Excel
            //StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            //StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            //StrExport.Append("<DIV  style='font-size:12px;'>");
            //StrExport.Append(dvInfo.InnerHtml);
            //StrExport.Append("</div></body></html>");
            //string strFile = "CD04.xls";
            //string strcontentType = "application/excel";
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.BufferOutput = true;
            //Response.ContentType = strcontentType;
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            //Response.Write(StrExport.ToString());
            //Response.Flush();
            //Response.Close();
            //Response.End();
            #endregion
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
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "CD03":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "CD02":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "CD01":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "TQ04":
                    listObj = Get04TQ(mParams);
                    break;
                case "TQ03":
                    listObj = Get04Total(mParams);
                    break;
                case "TQ02":
                    listObj = Get04Total(mParams);
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
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "CD03":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "CD02":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "CD01":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "TQ04":
                    listObj = Get04TQ(mParams);
                    break;
                case "TQ03":
                    listObj = Get04Total(mParams);
                    break;
                case "TQ02":
                    listObj = Get04Total(mParams);
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
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "CD03":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "CD02":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "CD01":
                    if (mParams.ViewType == 0)
                    {
                        listObj = Get04Total(mParams);
                    }
                    else
                    {
                        listDetail = Get04Detail(mParams);
                    }
                    break;
                case "TQ04":
                    listObj = Get04TQ(mParams);
                    break;
                case "TQ03":
                    listObj = Get04Total(mParams);
                    break;
                case "TQ02":
                    listObj = Get04Total(mParams);
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
        #endregion

        #region Get value      

        #region Báo cáo cân đối
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

        private List<CDDetail> Get04Detail(ParamsReport mParams)
        {
            listDetail = new List<CDDetail>();
            var rs = Helper.Invoke("GET", string.Format("api/Reports/Get04CDDetail?id={0}&sFromDate={1}&sToDate={2}&iPO_ID={3}", new object[] { 0, mParams.From_date, mParams.To_date, mParams.Po_ID }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue.ToList())
                {
                    detail = new CDDetail();
                    
                    detail.Group_Type = dyn.Group_Type;
                    detail.STT = dyn.STT;
                    detail.Font_Bold = dyn.Font_Bold;
                    detail.Cap_hien_thi = dyn.Cap_hien_thi;
                    detail.Item_Code = dyn.Item_Code;
                    detail.Item_Name = dyn.Item_Name;
                    detail.Po_Code = dyn.Po_Code;
                    detail.Po_Name = dyn.Po_Name;
                    detail.Parent_Code = dyn.Parent_Code;
                    detail.Parent_Name = dyn.Parent_Name;
                    detail.GT_Thu = dyn.GT_Thu;
                    detail.GT_Thu_Usd = dyn.GT_Thu_Usd;
                    detail.DC_Thu = dyn.DC_Thu;
                    detail.DC_Thu_Usd = dyn.DC_Thu_Usd;
                    detail.GT_Tra = dyn.GT_Tra;
                    detail.GT_Tra_Usd = dyn.GT_Tra_Usd;
                    detail.DC_Tra = dyn.DC_Tra;
                    detail.DC_Tra_Usd = dyn.DC_Tra_Usd;
                    detail.Tong_Thu = dyn.Tong_Thu;
                    detail.Tong_Thu_Usd = dyn.Tong_Thu_Usd;
                    detail.Tong_Tra = dyn.Tong_Tra;
                    detail.Tong_Tra_Usd = dyn.Tong_Tra_Usd;

                    listDetail.Add(detail);
                }

            }

            return listDetail;
        }

        private List<CDTotal> Get03Total(ParamsReport mParams)
        {
            listObj = new List<CDTotal>();
            var rs = Helper.Invoke("GET", string.Format("api/Reports/Get03CDTotal?id={0}&sFromDate={1}&sToDate={2}&iPO_ID={3}", new object[] { 0, mParams.From_date, mParams.To_date, mParams.Po_ID }), null);
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

        private List<CDDetail> Get03Detail(ParamsReport mParams)
        {
            listDetail = new List<CDDetail>();
            var rs = Helper.Invoke("GET", string.Format("api/Reports/Get03CDDetail?id={0}&sFromDate={1}&sToDate={2}&iPO_ID={3}", new object[] { 0, mParams.From_date, mParams.To_date, mParams.Po_ID }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue.ToList())
                {
                    detail = new CDDetail();

                    detail.Group_Type = dyn.Group_Type;
                    detail.STT = dyn.STT;
                    detail.Font_Bold = dyn.Font_Bold;
                    detail.Cap_hien_thi = dyn.Cap_hien_thi;
                    detail.Item_Code = dyn.Item_Code;
                    detail.Item_Name = dyn.Item_Name;
                    detail.Po_Code = dyn.Po_Code;
                    detail.Po_Name = dyn.Po_Name;
                    detail.Parent_Code = dyn.Parent_Code;
                    detail.Parent_Name = dyn.Parent_Name;
                    detail.GT_Thu = dyn.GT_Thu;
                    detail.GT_Thu_Usd = dyn.GT_Thu_Usd;
                    detail.DC_Thu = dyn.DC_Thu;
                    detail.DC_Thu_Usd = dyn.DC_Thu_Usd;
                    detail.GT_Tra = dyn.GT_Tra;
                    detail.GT_Tra_Usd = dyn.GT_Tra_Usd;
                    detail.DC_Tra = dyn.DC_Tra;
                    detail.DC_Tra_Usd = dyn.DC_Tra_Usd;
                    detail.Tong_Thu = dyn.Tong_Thu;
                    detail.Tong_Thu_Usd = dyn.Tong_Thu_Usd;
                    detail.Tong_Tra = dyn.Tong_Tra;
                    detail.Tong_Tra_Usd = dyn.Tong_Tra_Usd;

                    listDetail.Add(detail);
                }

            }

            return listDetail;
        }

        private List<CDTotal> Get03CDBC(ParamsReport mParams)
        {
            listObj = new List<CDTotal>();
            var rs = Helper.Invoke("GET", string.Format("api/Reports/Get03CDBC?id={0}&sFromDate={1}&sToDate={2}&iPO_ID={3}", new object[] { 0, mParams.From_date, mParams.To_date, mParams.Po_ID }), null);
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

        #endregion

        #region Báo cáo TQ
        private List<CDTotal> Get04TQ(ParamsReport mParams)
        {
            listObj = new List<CDTotal>();
            var rs = Helper.Invoke("GET", string.Format("api/Reports/Get04TQTotal?id={0}&sFromDate={1}&sToDate={2}&iPO_ID={3}", new object[] { 0, mParams.From_date, mParams.To_date, mParams.Po_ID }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue.ToList())
                {
                    model = new CDTotal();

                    model.From_date = dyn.From_date;
                    model.Po_Code = dyn.Po_Code;
                    model.Po_Name = dyn.Po_Name;
                    model.Parent_Code = dyn.Parent_Code;
                    model.Parent_Name = dyn.Parent_Name;
                    model.STT = dyn.STT;
                    model.level_ph = dyn.level_ph;
                    model.Ma_thu = dyn.Ma_thu;
                    model.Ten_ma_thu = dyn.Ten_ma_thu;
                    model.thu_vnd = dyn.thu_vnd;

                    listObj.Add(model);
                }

            }

            return listObj;
        }
        #endregion

        #region Báo cáo quỹ
        private List<R_FundInfo> GetCD03FUND(ParamsReport mParams)
        {
            listFund = new List<R_FundInfo>();
            var rs = Helper.Invoke("GET", string.Format("api/Reports/GetCD03FUND?id={0}&sFromDate={1}&sToDate={2}&iPO_ID={3}", new object[] { 0, mParams.From_date, mParams.To_date, mParams.Po_ID }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue.ToList())
                {
                    fund = new R_FundInfo();

                    fund.PO_TYPE = dyn.PO_TYPE;
                    fund.PO_CODE = dyn.PO_CODE;
                    fund.PO_NAME = dyn.PO_NAME;
                    fund.FUND_TT = dyn.FUND_TT;
                    fund.FUND_TDV = dyn.FUND_TDV;
                    fund.FUND_KD = dyn.FUND_KD;
                    fund.FUND_TKBD = dyn.FUND_TKBD;
                    fund.FUND_TOTAL = dyn.FUND_TOTAL;
                    fund.FUND_NEED = dyn.FUND_NEED;
                    fund.FUND_NEED_TKBD = dyn.FUND_NEED_TKBD;

                    listFund.Add(fund);
                }

            }

            return listFund;
        }

        #endregion

        #endregion
    }
}