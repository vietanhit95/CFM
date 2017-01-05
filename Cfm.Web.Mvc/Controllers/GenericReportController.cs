using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Cfm.Web.Mvc.Controllers
{
    public class GenericReportController : Controller
    {
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

        public void ShowReports()
        {
            try
            {
                mCFMParameters = null;
                mParameters = null;

                mCFMParameters = GetParameter(mParameters);
                switch (mCFMParameters[4].ToString().Trim())
                {
                    case "CD04":
                        Show04CD(mCFMParameters);
                        break;
                    case "CD03":
                        Show03CD(mCFMParameters);
                        break;
                    case "03TH":
                        Show03TH(mCFMParameters);
                        break;
                    case "02CD":
                        Show02CD(mCFMParameters);
                        break;
                    case "02TH":
                        Show02TH(mCFMParameters);
                        break;
                    case "01CD":
                        Show02CD(mCFMParameters);
                        break;
                    case "01TH":
                        Show02TH(mCFMParameters);
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

        public void Show04CD(string[] mParams)
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
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Templates\\rpts\\" + mParams[0].ToString();
                rd.Load(strRptPath);
                if (pDataSource != null && pDataSource.GetType().ToString() != "System.String")
                    rd.SetDataSource(pDataSource);
                if (!string.IsNullOrEmpty(mParams[1].ToString()))
                    rd.SetParameterValue("paraTime", "Lập cho ngày " + mParams[1].ToString());
                rd.SetParameterValue("paraBC", mParams[3].ToString() + " - " + mParams[4].ToString());
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "CD04");


                // Clear all sessions value
                ResetSession();
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
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Templates\\rpts\\" + mParams[0].ToString();
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
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Templates\\rpts\\" + mParams[0].ToString();
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
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Templates\\rpts\\" + mParams[0].ToString();
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
                string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Templates\\rpts\\" + mParams[0].ToString();
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
    }
}