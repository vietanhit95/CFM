using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Cfm.Web.Mvc.Areas.CFMReport.Models;

namespace Cfm.Web.Mvc.Areas.CFMReport.Controllers
{
    public class GenericReportController : Controller
    {
        //Generic File
        #region Avaiable        
        public string[] mCFMParameters = null;
        public string[] mParameters = null;

        public string[] GetParameter(string[] mParams)
        {
            string[] mParameters = new string[6];

            mParameters[0] = System.Web.HttpContext.Current.Session["pFileName"].ToString();
            mParameters[1] = System.Web.HttpContext.Current.Session["pFromDate"].ToString();
            mParameters[2] = System.Web.HttpContext.Current.Session["pToDate"].ToString();
            mParameters[3] = System.Web.HttpContext.Current.Session["pPOCode"].ToString();
            mParameters[4] = System.Web.HttpContext.Current.Session["pPOName"].ToString();
            mParameters[5] = System.Web.HttpContext.Current.Session["pReportCode"].ToString();

            return mParameters;
        }

        public void ResetSession()
        {
            Session["pFileName"] = null;
            Session["pFromDate"] = null;
            Session["pToDate"] = null;
            Session["pPOCode"] = null;
            Session["pPOName"] = null;
            Session["pReportCode"] = null;
        }

        #endregion

        #region Show reports
        public void ShowReports()
        {
            try
            {
                ParamsReport mParams = TempData["dt"] as ParamsReport;
                switch (mParams.Report_code.ToString().Trim())
                {
                    case "CD04":
                        Show04CD(mParams);
                        break;
                    case "CD03":
                        Show03CD(mCFMParameters);
                        break;
                    case "TH03":
                        Show03TH(mCFMParameters);
                        break;
                    case "CD02":
                        Show02CD(mCFMParameters);
                        break;
                    case "TH02":
                        Show02TH(mCFMParameters);
                        break;
                    case "CD01":
                        Show01CD(mCFMParameters);
                        break;
                    case "TH01":
                        Show01TH(mCFMParameters);
                        break;
                    case "TQ04":
                        break;
                    case "TQ03":
                        break;
                    case "TQ02":
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        public void Show04CD(ParamsReport mParams)
        {
            bool isValid = true;
            // Call API function
            var pDataSource = Get04CD();

            if (string.IsNullOrEmpty(mParams.File_name.ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReports\\Rpts\\Counter\\" + mParams.File_name.ToString();
                rd.Load(strRptPath);
                if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                    rd.SetDataSource(pDataSource);
                if (!string.IsNullOrEmpty(mParams.From_date.ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams.From_date.ToString());
                rd.SetParameterValue("paraBC", "");

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "CD04");
            }
            else
            {
                Response.Write("<H2>Nothing Found; No Report name found</H2>");
            }
        }

        public void Show03CD(string[] mParams)
        {
            bool isValid = true;

            var pDataSource = System.Web.HttpContext.Current.Session["pDataSource"];

            if (string.IsNullOrEmpty(mParams[0].ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReports\\Rpts\\Manager\\" + mParams[0].ToString();
                rd.Load(strRptPath);
                if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                    rd.SetDataSource(pDataSource);
                if (!string.IsNullOrEmpty(mParams[1].ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams[1].ToString());
                rd.SetParameterValue("paraBC", mParams[3].ToString() + " - " + mParams[4].ToString());
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "CD03");

                // Clear all sessions value
                ResetSession();
            }
            else
            {
                Response.Write("<H2>Nothing Found; No Report name found</H2>");
            }
        }

        public void Show03TH(string[] mParams)
        {
            bool isValid = true;

            var pDataSource = System.Web.HttpContext.Current.Session["pDataSource"];

            if (string.IsNullOrEmpty(mParams[0].ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReports\\Rpts\\Manager\\" + mParams[0].ToString();
                rd.Load(strRptPath);
                if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                    rd.SetDataSource(pDataSource);
                if (!string.IsNullOrEmpty(mParams[1].ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams[1].ToString());
                rd.SetParameterValue("paraBC", mParams[3].ToString() + " - " + mParams[4].ToString());
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "TH03");

                // Clear all sessions value
                ResetSession();
            }
            else
            {
                Response.Write("<H2>Nothing Found; No Report name found</H2>");
            }
        }

        public void Show02CD(string[] mParams)
        {
            bool isValid = true;

            var pDataSource = System.Web.HttpContext.Current.Session["pDataSource"];

            if (string.IsNullOrEmpty(mParams[0].ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReports\\Rpts\\Manager\\" + mParams[0].ToString();
                rd.Load(strRptPath);
                if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                    rd.SetDataSource(pDataSource);
                if (!string.IsNullOrEmpty(mParams[1].ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams[1].ToString());
                rd.SetParameterValue("paraBC", mParams[3].ToString() + " - " + mParams[4].ToString());
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "CD02");

                // Clear all sessions value
                ResetSession();
            }
            else
            {
                Response.Write("<H2>Nothing Found; No Report name found</H2>");
            }
        }

        public void Show02TH(string[] mParams)
        {
            bool isValid = true;

            var pDataSource = System.Web.HttpContext.Current.Session["pDataSource"];

            if (string.IsNullOrEmpty(mParams[0].ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReports\\Rpts\\Manager\\" + mParams[0].ToString();
                rd.Load(strRptPath);
                if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                    rd.SetDataSource(pDataSource);
                if (!string.IsNullOrEmpty(mParams[1].ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams[1].ToString());
                rd.SetParameterValue("paraBC", mParams[3].ToString() + " - " + mParams[4].ToString());
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "TH02");

                // Clear all sessions value
                ResetSession();
            }
            else
            {
                Response.Write("<H2>Nothing Found; No Report name found</H2>");
            }
        }

        public void Show01CD(string[] mParams)
        {
            bool isValid = true;

            var pDataSource = System.Web.HttpContext.Current.Session["pDataSource"];

            if (string.IsNullOrEmpty(mParams[0].ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReports\\Rpts\\Center\\" + mParams[0].ToString();
                rd.Load(strRptPath);
                if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                    rd.SetDataSource(pDataSource);
                if (!string.IsNullOrEmpty(mParams[1].ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams[1].ToString());
                rd.SetParameterValue("paraBC", mParams[3].ToString() + " - " + mParams[4].ToString());
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "CD01");

                // Clear all sessions value
                ResetSession();
            }
            else
            {
                Response.Write("<H2>Nothing Found; No Report name found</H2>");
            }
        }

        public void Show01TH(string[] mParams)
        {
            bool isValid = true;

            var pDataSource = System.Web.HttpContext.Current.Session["pDataSource"];

            if (string.IsNullOrEmpty(mParams[0].ToString()))
            {
                isValid = false;
            }

            if (isValid)
            {
                ReportDocument rd = new ReportDocument();
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CFMReports\\Rpts\\Center\\" + mParams[0].ToString();
                rd.Load(strRptPath);
                if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                    rd.SetDataSource(pDataSource);
                if (!string.IsNullOrEmpty(mParams[1].ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams[1].ToString());
                rd.SetParameterValue("paraBC", mParams[3].ToString() + " - " + mParams[4].ToString());
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "TH01");

                // Clear all sessions value
                ResetSession();
            }
            else
            {
                Response.Write("<H2>Nothing Found; No Report name found</H2>");
            }
        }

        #endregion

        #region Get value       
        private List<CDTotal> Get04CD()
        {
            return new List<CDTotal>() {
            new CDTotal(){Service_type=1,Group_type=1,Trans_type=1,From_date="06/10/2016",To_date="06/10/2016",Po_Code="871400", Po_Name="", font_bold_ph=1,Ma_thu="I.Tiền mặt đầu kỳ",thu_vnd= 10000000,thu_usd= 100,thu_qd_vnd= 12000000,Ma_chi="1.Trả các dịch vụ",chi_vnd= 10000000,chi_usd= 100,chi_qd_vnd= 12000000,Ty_gia=20000},
            new CDTotal(){Service_type=1,Group_type=1,Trans_type=1,From_date="06/10/2016",To_date="06/10/2016",Po_Code="871406", Po_Name="", font_bold_ph=1,Ma_thu="1.1 Tại bưu cục",thu_vnd=10000000,thu_usd=100,thu_qd_vnd=12000000,Ma_chi="1.Trả các dịch vụ",chi_vnd=10000000,chi_usd=100,chi_qd_vnd=12000000,Ty_gia=20000}
            };
        }

        #endregion
    }
}