using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cfm.Web.Mvc.Areas.Admin.Controllers;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Areas.CFMDistrict.Models.ViewModels;
using Cfm.Web.Mvc.Common;
using Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel;
using Cfm.Web.Mvc.Areas.CFMReport.Models;

namespace Cfm.Web.Mvc.Areas.CFMDistrict.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District })]
    public class AccountingController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        #region Báo cáo 03-CĐ
        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.Report_District_Report03CD })]
        public ActionResult Report03CD(int reportID = 0, string reportDate = "")
        {
            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            List<int> ListItemGroupID_TDV = new List<int>();
            List<int> ListItemGroupID_TKBD = new List<int>();
            List<int> ListItemGroupID_KD = new List<int>();
            CD03HeaderViewModel CD03Header = new CD03HeaderViewModel();
            CD03Header.ListDetail = new List<CD03DetailViewModel>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            CD03Header.PoId = oEmployee.POID;
            CD03Header.ApprovedEmpId = oEmployee.ID;
            //if (reportDate != "")
            //    CD03Header.ReportDate = reportDate;
            //else
            //    CD03Header.ReportDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
            int Total = 0;
            CD03Header.ReportDate = Session[Constant.TIMEWORK_SESSION].ToString();

            string Url = string.Format("api/Dictionary/ItemGroupGet/{0}?Code={1}&Name={2}&Status={3}&ReportId={6}&PageIndex={4}&PageSize={5}", 0, "", "", "", 0, Constant.PageSize, reportID);
            var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

            if (rs.Code == "00")
            {
                int.TryParse(rs.Value.ToString(), out Total);
                dynamic oValue = rs.ListValue;
                foreach (var item in oValue)
                {
                    if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Center)
                        ListItemGroupID.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Unit)
                        ListItemGroupID_TDV.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Saving)
                        ListItemGroupID_TKBD.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.BusinessService)
                        ListItemGroupID_KD.Add(Convert.ToInt32(item.ID));
                }
            }

            ViewBag.ReportID = reportID;
            ViewBag.ListItemGroupID = ListItemGroupID;
            ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
            ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
            ViewBag.ListItemGroupID_KD = ListItemGroupID_KD;
            ViewBag.PoID = oEmployee.POID;
            return View(CD03Header);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult Report03CDGetData(int reportID = 0, string reportDate = "", string isAgain = "N")
        {
            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            List<int> ListItemGroupID_TDV = new List<int>();
            List<int> ListItemGroupID_TKBD = new List<int>();
            List<int> ListItemGroupID_KD = new List<int>();
            CD03HeaderViewModel CD03Header = new CD03HeaderViewModel();
            CD03Header.ListDetail = new List<CD03DetailViewModel>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            CD03Header.PoId = oEmployee.POID;
            CD03Header.ApprovedEmpId = oEmployee.ID;
            if (reportDate != "")
                CD03Header.ReportDate = reportDate;
            else
                CD03Header.ReportDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
            int Total = 0;

            string Url = string.Format("api/Dictionary/ItemGroupGet/{0}?Code={1}&Name={2}&Status={3}&ReportId={6}&PageIndex={4}&PageSize={5}", 0, "", "", "", 0, Constant.PageSize, reportID);
            var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

            if (rs.Code == "00")
            {
                int.TryParse(rs.Value.ToString(), out Total);
                dynamic oValue = rs.ListValue;
                foreach (var item in oValue)
                {
                    if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Center)
                        ListItemGroupID.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Unit)
                        ListItemGroupID_TDV.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Saving)
                        ListItemGroupID_TKBD.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.BusinessService)
                        ListItemGroupID_KD.Add(Convert.ToInt32(item.ID));
                }
            }

            ViewBag.ReportID = reportID;
            ViewBag.ListItemGroupID = ListItemGroupID;
            ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
            ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
            ViewBag.ListItemGroupID_KD = ListItemGroupID_KD;
            ViewBag.isAgain = isAgain;
            ViewBag.PoID = oEmployee.POID;
            return PartialView(CD03Header);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult Report03CDGet(CD03HeaderViewModel CD03Header = null, int reportID = 0, int itemGroupId = 0, int cashFllowID = 0, string isAgain = "N")
        {
            var rs = Helper.Invoke("GET", string.Format("api/AccountingDistrict/Report03CDSummary?id={0}&reportID={1}&itemGroupID={2}&reportDate={3}&cashFllowId={4}&poId={5}&isAgain={6}", new object[] { 0, reportID, itemGroupId, CD03Header.ReportDate, cashFllowID, CD03Header.PoId, isAgain }), null);
            if (rs != null && rs.Value != null)
            {
                dynamic oValue = rs.Value;
                if (oValue.ListDetail != null)
                {
                    CD03Header = CD03Header ?? new CD03HeaderViewModel();

                    foreach (dynamic dyn in oValue.ListDetail)
                    {
                        var oDetail = new CD03DetailViewModel()
                        {
                            Id = dyn.Id,
                            ItemId = dyn.ItemId,
                            ItemCode = dyn.ItemCode,
                            Formula = dyn.Formula,
                            DisplayName = dyn.DisplayName,
                            ParentId = dyn.ParentId,
                            IsVisible = dyn.IsVisible,
                            ItemGroupId = dyn.ItemGroupId,
                            FontBold = dyn.FontBold,
                            EditMode = dyn.EditMode,
                            AllowSummary = dyn.AllowSummary,
                            AllowVnd = dyn.AllowVnd,
                            AllowUsd = dyn.AllowUsd,
                            AmountVnd = dyn.AmountVnd,
                            AmountUsd = dyn.AmountUsd,
                            VisibleLevel = dyn.VisibleLevel

                        };

                        if (!CD03Header.ListDetail.Contains(oDetail))
                            CD03Header.ListDetail.Add(oDetail);
                    }
                }

            }

            ViewBag.reportID = reportID;
            ViewBag.itemGroupID = itemGroupId;
            ViewBag.cashFllowID = cashFllowID;
            return PartialView(CD03Header);
        }

        public ActionResult Report03CDCreate()
        {
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult Report03CDUpdate(CD03HeaderViewModel CD03Header)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                var rs = Helper.Invoke("POST", "api/AccountingDistrict/ReportCD03Update", CD03Header);
                if (rs != null && rs.Value != null)
                {
                    jResult = Json(new { Code = rs.Code, Mes = rs.Message, Value = rs.Value }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        public ActionResult GetListMenuSummaryReport()
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
            return View(listReport);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.Report_District_Report03CDManage })]
        public ActionResult Report03CDManage()
        {
            List<SelectListItem> ListReportStatus = new List<SelectListItem>();
            foreach (var item in Repository.ListReportStatus)
            {
                ListReportStatus.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString()
                });
            }
            ViewBag.ListReportStatus = ListReportStatus;
            return PartialView();
        }

        public ActionResult Report03CDManageGet(string dateRange = "", string reportStatus = "", int PageIndex = 1)
        {
            string fromDate = "";
            string toDate = "";
            if (dateRange == "")
            {
                fromDate = Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            else
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            int Total = 0;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<CD03HeaderViewModel> listReportHeader = new List<CD03HeaderViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingDistrict/Report03CDManage?poId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}", new object[] { oEmployee.POID, fromDate, toDate, reportStatus, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                string statusName = "";
                foreach (dynamic dyn in rs.ListValue)
                {
                    if (dyn.ReportStatus == "C")
                        statusName = "Chưa lập báo cáo";
                    else if (dyn.ReportStatus == "L")
                        statusName = "Chưa xác nhận";
                    else if (dyn.ReportStatus == "A")
                        statusName = "Đã xác nhận";
                    else
                        statusName = "Trạng thái không xác định";
                    var oHeader = new CD03HeaderViewModel()
                    {
                        Id = dyn.Id,
                        PoId = dyn.PoId,
                        ApprovedDate = dyn.ApprovedDate,
                        ReportDate = dyn.ReportDate,
                        ReportStatus = dyn.ReportStatus,
                        ReportStatusName = statusName,
                        ApprovedEmpName = dyn.ApprovedEmpName

                    };

                    if (!listReportHeader.Contains(oHeader))
                        listReportHeader.Add(oHeader);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.PoID = oEmployee.POID;

            return PartialView(listReportHeader);
        }
        #endregion

        #region Báo cáo 03-CĐ chi tiết Bưu cục

        public ActionResult Report03CDPO(int poId = 0,int reportID = 0, string reportDate = "")
        {
            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            List<int> ListItemGroupID_TDV = new List<int>();
            List<int> ListItemGroupID_TKBD = new List<int>();
            List<int> ListItemGroupID_KD = new List<int>();
            CD03POHeaderViewModel CD03Header = new CD03POHeaderViewModel();
            CD03Header.ListDetail = new List<CD03PODetailViewModel>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            PostOfficeViewModel po = Repository.GetPOByID(poId);
            if(po != null)
            {
                CD03Header.PoId = poId;
                CD03Header.PoCode = po.Code;
                CD03Header.PoName = po.Name;
            }
         
            int Total = 0;
            CD03Header.ReportDate = Session[Constant.TIMEWORK_SESSION].ToString();

            string Url = string.Format("api/Dictionary/ItemGroupGet/{0}?Code={1}&Name={2}&Status={3}&ReportId={6}&PageIndex={4}&PageSize={5}", 0, "", "", "", 0, Constant.PageSize, reportID);
            var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

            if (rs.Code == "00")
            {
                int.TryParse(rs.Value.ToString(), out Total);
                dynamic oValue = rs.ListValue;
                foreach (var item in oValue)
                {
                    if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Center)
                        ListItemGroupID.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Unit)
                        ListItemGroupID_TDV.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Saving)
                        ListItemGroupID_TKBD.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.BusinessService)
                        ListItemGroupID_KD.Add(Convert.ToInt32(item.ID));
                }
            }

            ViewBag.ReportID = reportID;
            ViewBag.ListItemGroupID = ListItemGroupID;
            ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
            ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
            ViewBag.ListItemGroupID_KD = ListItemGroupID_KD;
            ViewBag.PoID = oEmployee.POID;
            ViewBag.reportDate = CD03Header.ReportDate;
            return View(CD03Header);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult Report03CDPOGetData(int poId =0,int reportID = 0, string reportDate = "", string isAgain = "N")
        {
            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            List<int> ListItemGroupID_TDV = new List<int>();
            List<int> ListItemGroupID_TKBD = new List<int>();
            List<int> ListItemGroupID_KD = new List<int>();
            CD03POHeaderViewModel CD03Header = new CD03POHeaderViewModel();
            CD03Header.ListDetail = new List<CD03PODetailViewModel>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
          
            PostOfficeViewModel po = Repository.GetPOByID(poId);
            if (po != null)
            {
                CD03Header.PoId = poId;
                CD03Header.PoCode = po.Code;
                CD03Header.PoName = po.Name;
            }

            if (reportDate != "")
                CD03Header.ReportDate = reportDate;
            else
                CD03Header.ReportDate = Session[Constant.TIMEWORK_SESSION].ToString();

            int Total = 0;

            string Url = string.Format("api/Dictionary/ItemGroupGet/{0}?Code={1}&Name={2}&Status={3}&ReportId={6}&PageIndex={4}&PageSize={5}", 0, "", "", "", 0, Constant.PageSize, reportID);
            var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

            if (rs.Code == "00")
            {
                int.TryParse(rs.Value.ToString(), out Total);
                dynamic oValue = rs.ListValue;
                foreach (var item in oValue)
                {
                    if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Center)
                        ListItemGroupID.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Unit)
                        ListItemGroupID_TDV.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Saving)
                        ListItemGroupID_TKBD.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.BusinessService)
                        ListItemGroupID_KD.Add(Convert.ToInt32(item.ID));
                }
            }

            ViewBag.ReportID = reportID;
            ViewBag.ListItemGroupID = ListItemGroupID;
            ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
            ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
            ViewBag.ListItemGroupID_KD = ListItemGroupID_KD;
            ViewBag.isAgain = isAgain;
            ViewBag.PoID = oEmployee.POID;
            ViewBag.reportDate = CD03Header.ReportDate;
            return PartialView(CD03Header);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult Report03CDPOGet(CD03POHeaderViewModel CD03Header = null, int reportID = 0, int itemGroupId = 0, int cashFllowID = 0, string isAgain = "N")
        {
            var rs = Helper.Invoke("GET", string.Format("api/AccountingDistrict/Report03CDPOSummary?id={0}&reportID={1}&itemGroupID={2}&reportDate={3}&cashFllowId={4}&poId={5}&isAgain={6}", new object[] { 0, reportID, itemGroupId, CD03Header.ReportDate, cashFllowID, CD03Header.PoId, isAgain }), null);
            if (rs != null && rs.Value != null)
            {
                dynamic oValue = rs.Value;
                if (oValue.ListDetail != null)
                {
                    CD03Header = CD03Header ?? new CD03POHeaderViewModel();

                    foreach (dynamic dyn in oValue.ListDetail)
                    {
                        var oDetail = new CD03PODetailViewModel()
                        {
                            Id = dyn.Id,
                            ItemId = dyn.ItemId,
                            ItemCode = dyn.ItemCode,
                            Formula = dyn.Formula,
                            DisplayName = dyn.DisplayName,
                            ParentId = dyn.ParentId,
                            IsVisible = dyn.IsVisible,
                            ItemGroupId = dyn.ItemGroupId,
                            FontBold = dyn.FontBold,
                            EditMode = dyn.EditMode,
                            AllowSummary = dyn.AllowSummary,
                            AllowVnd = dyn.AllowVnd,
                            AllowUsd = dyn.AllowUsd,
                            AmountVnd = dyn.AmountVnd,
                            AmountUsd = dyn.AmountUsd,
                            VisibleLevel = dyn.VisibleLevel

                        };

                        if (!CD03Header.ListDetail.Contains(oDetail))
                            CD03Header.ListDetail.Add(oDetail);
                    }
                }

            }

            ViewBag.reportID = reportID;
            ViewBag.itemGroupID = itemGroupId;
            ViewBag.cashFllowID = cashFllowID;
            return PartialView(CD03Header);
        }

        public ActionResult Report03CDPOCreate()
        {
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult Report03CDPOUpdate(CD03HeaderViewModel CD03Header)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                var rs = Helper.Invoke("POST", "api/AccountingDistrict/ReportCD03POUpdate", CD03Header);
                if (rs != null && rs.Value != null)
                {
                    jResult = Json(new { Code = rs.Code, Mes = rs.Message, Value = rs.Value }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        #endregion

        #region Nhập thay thế 04-CĐ
        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.Report_District_Report04CDReplace })]
        public ActionResult Report04CDReplace(int reportID = 0, int poId = 0, string reportDate = "")
        {
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                var poReplace = Repository.GetPOByID(poId);
                PostOfficeViewModel po = null;
                var poManage = Repository.GetPOByID(oEmployee.POID);
                if (poManage.POLevel == (int)Constant.POLevel.Branch)
                    po = Repository.GetPOByID(poReplace.ParentID);
                else
                    po = poReplace;

                if (po != null && po.ParentID == oEmployee.POID)
                {
                    List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
                    List<int> ListItemGroupID = new List<int>();
                    List<int> ListItemGroupID_TDV = new List<int>();
                    List<int> ListItemGroupID_TKBD = new List<int>();
                    List<int> ListItemGroupID_BS = new List<int>();
                    List<int> ListItemGroupID_General = new List<int>();
                    CD04HeaderViewModel CD04Header = new CD04HeaderViewModel();
                    CD04Header.PoName = poReplace.Name;
                    CD04Header.PoCode = poReplace.Code;
                    CD04Header.PoId = poReplace.ID;
                    CD04Header.IsApproved = true;

                    CD04Header.ListDetail = new List<CD04DetailViewModel>();

                    CD04Header.PoId = poId;
                    CD04Header.ApprovedEmpId = oEmployee.ID;
                    //if (reportDate != "")
                    //    CD04Header.ReportDate = reportDate;
                    //else
                    //    CD04Header.ReportDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    CD04Header.ReportDate = Session[Constant.TIMEWORK_SESSION].ToString();
                    int Total = 0;

                    string Url = string.Format("api/Dictionary/ItemGroupGet/{0}?Code={1}&Name={2}&Status={3}&ReportId={6}&PageIndex={4}&PageSize={5}", 0, "", "", "", 0, Constant.PageSize, reportID);
                    var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                    if (rs.Code == "00")
                    {
                        int.TryParse(rs.Value.ToString(), out Total);
                        dynamic oValue = rs.ListValue;
                        foreach (var item in oValue)
                        {
                            if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Center)
                                ListItemGroupID.Add(Convert.ToInt32(item.ID));
                            else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Unit)
                                ListItemGroupID_TDV.Add(Convert.ToInt32(item.ID));
                            else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Saving)
                                ListItemGroupID_TKBD.Add(Convert.ToInt32(item.ID));
                            else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.BusinessService)
                                ListItemGroupID_BS.Add(Convert.ToInt32(item.ID));
                            else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.General)
                                ListItemGroupID_General.Add(Convert.ToInt32(item.ID));
                        }
                    }

                    ViewBag.ReportID = reportID;
                    ViewBag.PoID = CD04Header.PoId;
                    ViewBag.ListItemGroupID = ListItemGroupID;
                    ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
                    ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
                    ViewBag.ListItemGroupID_BS = ListItemGroupID_BS;
                    ViewBag.ListItemGroupID_General = ListItemGroupID_General;
                    ViewBag.reportDate = CD04Header.ReportDate;
                    return View(CD04Header);
                }
                else
                {
                    return RedirectToAction("Logout", "User", new { Area = "Admin" });
                }
            }
            catch
            {
                return null;
            }
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult Report04CDReplaceGetData(int reportID = 0, int poId = 0, string reportDate = "", string isAgain = "N")
        {
            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            List<int> ListItemGroupID_TDV = new List<int>();
            List<int> ListItemGroupID_TKBD = new List<int>();
            List<int> ListItemGroupID_BS = new List<int>();
            List<int> ListItemGroupID_General = new List<int>();
            CD04HeaderViewModel CD04Header = new CD04HeaderViewModel();
            CD04Header.ListDetail = new List<CD04DetailViewModel>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            var poReplace = Repository.GetPOByID(poId);
            CD04Header.PoId = poId;
            CD04Header.PoCode = poReplace.Code;
            CD04Header.PoName = poReplace.Name;
            CD04Header.PoApprovedId = oEmployee.POID;
            CD04Header.IsApproved = true;
            CD04Header.ApprovedEmpId = oEmployee.ID;
            if (reportDate != "")
            {
                CD04Header.ApprovedDate = reportDate;
                CD04Header.ReportDate = reportDate;
            }
            else
            {
                CD04Header.ApprovedDate = Session[Constant.TIMEWORK_SESSION].ToString();
                CD04Header.ReportDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            int Total = 0;

            string Url = string.Format("api/Dictionary/ItemGroupGet/{0}?Code={1}&Name={2}&Status={3}&ReportId={6}&PageIndex={4}&PageSize={5}", 0, "", "", "", 0, Constant.PageSize, reportID);
            var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

            if (rs.Code == "00")
            {
                int.TryParse(rs.Value.ToString(), out Total);
                dynamic oValue = rs.ListValue;
                foreach (var item in oValue)
                {
                    if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Center)
                        ListItemGroupID.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Unit)
                        ListItemGroupID_TDV.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.Saving)
                        ListItemGroupID_TKBD.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.BusinessService)
                        ListItemGroupID_BS.Add(Convert.ToInt32(item.ID));
                    else if (Convert.ToInt32(item.CashFllowId) == (int)Constant.CashFllowType.General)
                        ListItemGroupID_General.Add(Convert.ToInt32(item.ID));
                }
            }

            ViewBag.ReportID = reportID;
            ViewBag.PoID = CD04Header.PoId;
            ViewBag.ListItemGroupID = ListItemGroupID;
            ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
            ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
            ViewBag.ListItemGroupID_BS = ListItemGroupID_BS;
            ViewBag.ListItemGroupID_General = ListItemGroupID_General;
            ViewBag.reportDate = CD04Header.ReportDate;
            ViewBag.isAgain = isAgain;
            ViewBag.PoID = poId;
            return PartialView(CD04Header);
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult Report04CDReplaceGet(CD04HeaderViewModel CD04Header = null, int reportID = 0, int itemGroupId = 0, int cashFllowID = 0, string isAgain = "N")
        {

            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04CDSummary?id={0}&reportID={1}&itemGroupID={2}&reportDate={3}&cashFllowId={4}&poId={5}&isAgain={6}", new object[] { 0, reportID, itemGroupId, CD04Header.ReportDate, cashFllowID, CD04Header.PoId, isAgain }), null);
            if (rs != null && rs.Value != null)
            {
                dynamic oValue = rs.Value;
                if (oValue.ListDetail != null)
                {

                    foreach (dynamic dyn in oValue.ListDetail)
                    {
                        var oDetail = new CD04DetailViewModel()
                        {
                            Id = dyn.Id,
                            ItemId = dyn.ItemId,
                            ItemCode = dyn.ItemCode,
                            Formula = dyn.Formula,
                            DisplayName = dyn.DisplayName,
                            ParentId = dyn.ParentId,
                            IsVisible = dyn.IsVisible,
                            ItemGroupId = dyn.ItemGroupId,
                            FontBold = dyn.FontBold,
                            EditMode = dyn.EditMode,
                            AllowSummary = dyn.AllowSummary,
                            AllowVnd = dyn.AllowVnd,
                            AllowUsd = dyn.AllowUsd,
                            AmountVnd = dyn.AmountVnd,
                            AmountUsd = dyn.AmountUsd,
                            VisibleLevel = dyn.VisibleLevel

                        };

                        if (!CD04Header.ListDetail.Contains(oDetail))
                            CD04Header.ListDetail.Add(oDetail);
                    }
                }

            }

            ViewBag.reportID = reportID;
            ViewBag.itemGroupID = itemGroupId;
            ViewBag.cashFllowID = cashFllowID;
            return PartialView(CD04Header);
        }

        public ActionResult Report04CDReplaceCreate()
        {
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult Report04CDReplaceUpdate(CD04HeaderViewModel CD04Header)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                var rs = Helper.Invoke("POST", "api/AccountingCounter/ReportCD04Update", CD04Header);
                if (rs != null && rs.Value != null)
                {
                    jResult = Json(new { Code = rs.Code, Mes = rs.Message, Value = rs.Value }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.Report_District_Report04CDReplaceManage })]
        public ActionResult Report04CDReplaceManage()
        {
            List<SelectListItem> ListReportStatus = new List<SelectListItem>();
            foreach (var item in Repository.ListReportStatus)
            {
                ListReportStatus.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString()
                });
            }
            ViewBag.ListReportStatus = ListReportStatus;
            return PartialView();
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult Report04CDReplaceManageGet(int PoId = 0, string dateRange = "", string reportStatus = "", int pageIndex = 1)
        {
            string fromDate = "";
            string toDate = "";
            if (dateRange == "")
            {
                fromDate = Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            else
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            int Total = 0;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<CD04HeaderViewModel> listReportHeader = new List<CD04HeaderViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04CDManage?poId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}&PoDistrictId={6}", new object[] { PoId, fromDate, toDate, reportStatus, pageIndex, Common.Constant.PageSize, oEmployee.POID }), null);
            if (rs != null && rs.ListValue != null)
            {
                string statusName = "";
                foreach (dynamic dyn in rs.ListValue)
                {
                    if (dyn.ReportStatus == "C")
                        statusName = "Chưa lập báo cáo";
                    else if (dyn.ReportStatus == "L")
                        statusName = "Chưa xác nhận";
                    else if (dyn.ReportStatus == "A")
                        statusName = "Đã xác nhận";
                    else
                        statusName = "Trạng thái không xác định";
                    var oHeader = new CD04HeaderViewModel()
                    {
                        Id = dyn.Id,
                        PoId = dyn.PoId,
                        PoName = dyn.PoCode.ToString() + " - " + dyn.PoName.ToString(),
                        ApprovedDate = dyn.ApprovedDate,
                        ReportDate = dyn.ReportDate,
                        ReportStatus = dyn.ReportStatus,
                        ReportStatusName = statusName,
                        ApprovedEmpName = dyn.ApprovedEmpName

                    };

                    if (!listReportHeader.Contains(oHeader))
                        listReportHeader.Add(oHeader);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;

            return PartialView(listReportHeader);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult Report04CDCancelApprove(int Id = 0, int poId = 0, string reportDate = "")
        {
            JsonResult jResult = new JsonResult();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            var CD04Header = new CD04HeaderViewModel
            {
                Id = Id,
                ReportDate = reportDate,
                PoId = poId
            };
            try
            {
                var rs = Helper.Invoke("POST", "api/AccountingCounter/ReportCDCancelApprove", CD04Header);
                if (rs != null)
                {
                    jResult = Json(new { Code = rs.Code, Mes = rs.Message, Value = rs.Value }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        #endregion

        #region Tình hình báo cáo Bưu cục

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.Report_District_ReportPOManage })]
        public ActionResult ReportPOManage()
        {
            List<SelectListItem> ListReportStatus = new List<SelectListItem>();
            foreach (var item in Repository.ListReportStatus)
            {
                ListReportStatus.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString()
                });
            }
            ViewBag.ListReportStatus = ListReportStatus;
            return View();
        }

        public ActionResult ReportPOManageGet(int PoId = 0, string dateRange = "", string reportStatus = "", int pageIndex = 1)
        {
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel.CD04HeaderViewModel> ListCD04 = new List<CD04HeaderViewModel>();
            string fromDate = "";
            string toDate = "";
            if (dateRange == "")
            {
                fromDate = Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            else
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            int Total = 0;

            List<CD04HeaderViewModel> listReportHeader = new List<CD04HeaderViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04CDManage?poId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}&PoDistrictId={6}", new object[] { PoId, fromDate, toDate, reportStatus, pageIndex, Common.Constant.PageSize, oEmployee.POID }), null);
            if (rs != null && rs.ListValue != null)
            {
                string statusName = "";
                foreach (dynamic dyn in rs.ListValue)
                {
                    if (dyn.ReportStatus == "C")
                        statusName = "Chưa lập báo cáo";
                    else if (dyn.ReportStatus == "L")
                        statusName = "Chưa xác nhận";
                    else if (dyn.ReportStatus == "A")
                        statusName = "Đã xác nhận";
                    else
                        statusName = "Trạng thái không xác định";
                    var oHeader = new CD04HeaderViewModel()
                    {
                        Id = dyn.Id,
                        PoId = dyn.PoId,
                        PoName = dyn.PoCode.ToString() + " - " + dyn.PoName.ToString(),
                        ApprovedDate = dyn.ApprovedDate,
                        ReportDate = dyn.ReportDate,
                        ReportStatus = dyn.ReportStatus,
                        ReportStatusName = statusName,
                        ApprovedEmpName = dyn.ApprovedEmpName

                    };

                    if (!listReportHeader.Contains(oHeader))
                        listReportHeader.Add(oHeader);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }

            ViewBag.Total = Total;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.PoID = oEmployee.POID;
            return PartialView(listReportHeader);
        }

        #endregion

        #region Đánh giá chất lượng báo cáo toàn Huyện

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.Quality_District_ReviewQualityPO })]
        public ActionResult ReviewQualityPO()
        {
            return View();
        }

        public ActionResult ReviewQualityPOGet(int PoId = 0, string dateRange = "", string reportStatus = "", int pageIndex = 1)
        {
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel.CD04HeaderViewModel> ListCD04 = new List<CD04HeaderViewModel>();
            string fromDate = "";
            string toDate = "";
            if (dateRange == "")
            {
                fromDate = Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            else
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            int Total = 0;

            List<CD04HeaderViewModel> listReportHeader = new List<CD04HeaderViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04CDManage?poId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}&PoDistrictId={6}", new object[] { PoId, fromDate, toDate, reportStatus, pageIndex, Common.Constant.PageSize, oEmployee.POID }), null);
            if (rs != null && rs.ListValue != null)
            {
                string statusName = "";
                string status = "";
                foreach (dynamic dyn in rs.ListValue)
                {
                    if (dyn.CreatedDate == "" || dyn.CreatedDate == null)
                    {
                        status = "C";
                        statusName = "Chưa lập báo cáo";
                    }
                    else if (Convert.ToDateTime(dyn.CreatedDate, new System.Globalization.CultureInfo("fr-FR")) > Convert.ToDateTime(dyn.ReportDate, new System.Globalization.CultureInfo("fr-FR")))
                    {
                        statusName = "Báo cáo chậm tiến độ";
                        status = "L";
                    }
                    else if (dyn.CreatedDate == dyn.ReportDate)
                    {
                        statusName = "Báo cáo đúng hạn";
                        status = "T";
                    }
                    else
                    {
                        statusName = "Trạng thái không xác định";
                        status = "N";
                    }

                    var oHeader = new CD04HeaderViewModel()
                    {
                        Id = dyn.Id,
                        PoId = dyn.PoId,
                        PoName = dyn.PoCode.ToString() + " - " + dyn.PoName.ToString(),
                        ReportDate = dyn.ReportDate,
                        CreatedDate = dyn.CreatedDate,
                        ReportStatus = status,
                        ReportStatusName = statusName

                    };

                    if (!listReportHeader.Contains(oHeader))
                        listReportHeader.Add(oHeader);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }

            ViewBag.Total = Total;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.PoID = oEmployee.POID;
            return PartialView(listReportHeader);
        }

        #endregion

        #region Chất lượng báo cáo toàn Huyện

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.Quality_District_ReviewQualityDistrict })]
        public ActionResult ReviewQualityDistrict()
        {
            return View();
        }

        public ActionResult ReviewQualityDistrictGet(string dateRange = "", int PageIndex = 1)
        {
            string fromDate = "";
            string toDate = "";
            if (dateRange == "")
            {
                fromDate = Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            else
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            int Total = 0;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<CD03HeaderViewModel> listReportHeader = new List<CD03HeaderViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingDistrict/Report03CDManage?poId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}", new object[] { oEmployee.POID, fromDate, toDate, "", PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                string statusName = "";
                string status = "";
                foreach (dynamic dyn in rs.ListValue)
                {
                    if (dyn.CreatedDate == "" || dyn.CreatedDate == null)
                    {
                        status = "C";
                        statusName = "Chưa lập báo cáo";
                    }
                    else if (Convert.ToDateTime(dyn.CreatedDate, new System.Globalization.CultureInfo("fr-FR")) > Convert.ToDateTime(dyn.ReportDate, new System.Globalization.CultureInfo("fr-FR")))
                    {
                        statusName = "Báo cáo chậm tiến độ";
                        status = "L";
                    }
                    else if (dyn.CreatedDate == dyn.ReportDate)
                    {
                        statusName = "Báo cáo đúng hạn";
                        status = "T";
                    }
                    else
                    {
                        statusName = "Trạng thái không xác định";
                        status = "N";
                    }
                    var oHeader = new CD03HeaderViewModel()
                    {
                        Id = dyn.Id,
                        PoId = dyn.PoId,
                        ReportDate = dyn.ReportDate,
                        CreatedDate = dyn.CreatedDate,
                        ReportStatus = status,
                        ReportStatusName = statusName

                    };

                    if (!listReportHeader.Contains(oHeader))
                        listReportHeader.Add(oHeader);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.PoID = oEmployee.POID;

            return PartialView(listReportHeader);
        }

        #endregion

        #region Quản trị báo cáo 03-TQ

        public ActionResult Report03TQManage(int reportId = 0)
        {
            ViewBag.reportId = reportId;
            return View();
        }

        public ActionResult Report03TQManageGet(string dateRange = "", int PageIndex = 1, int reportId = 0, int poId = 0)
        {
            string fromDate = "";
            string toDate = "";
            if (dateRange == "")
            {
                fromDate = Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            else
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            int Total = 0;

            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];

            //if (poId == 0)
            //    poId = oEmployee.POID;

            List<TQ04HeaderViewModel> listReportHeader = new List<TQ04HeaderViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04TQManage?poId={0}&fromDate={1}&toDate={2}&PageIndex={3}&PageSize={4}", new object[] { poId, fromDate, toDate, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {

                foreach (dynamic dyn in rs.ListValue)
                {

                    var oHeader = new TQ04HeaderViewModel()
                    {
                        Id = dyn.Id,
                        PoId = dyn.PoId,
                        PoCode = dyn.PoCode,
                        PoName = dyn.PoCode + " - " + dyn.PoName,
                        ReportDate = dyn.ReportDate,
                        DistrictPoId = dyn.DistrictPoId,
                        DistrictPoCode = dyn.DistrictPoCode,
                        DistrictPoName = dyn.DistrictPoCode + " - " + dyn.DistrictPoName,
                        AmndEmpName = dyn.AmndEmpName

                    };

                    if (!listReportHeader.Contains(oHeader))
                        listReportHeader.Add(oHeader);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.reportId = reportId;
            ViewBag.PoID = oEmployee.POID;
            return PartialView(listReportHeader);
        }

        public ActionResult Report03TQCreate(int id = 0, int reportId = 0,int poId =0)
        {
            TQ04HeaderViewModel TQ04Header = null;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            if (id == 0)
            {
                TQ04Header = new TQ04HeaderViewModel();

                PostOfficeViewModel oPo = (PostOfficeViewModel)Session[Constant.PO_SESSION];

                TQ04Header.ReportDate = Session[Constant.TIMEWORK_SESSION].ToString();
                TQ04Header.PoId = oEmployee.POID;
                TQ04Header.AmndEmpId = oEmployee.ID;
                TQ04Header.PoCode = oPo.Code;
                TQ04Header.PoName = oPo.Name;
                PostOfficeViewModel po = Repository.GetPOByID(oPo.ParentID);
                if (po != null)
                    TQ04Header.DistrictPoId = po.ID;
            }
            else
            {
                var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04TQGetById?id={0}&poId={1}", new object[] { id, poId }), null);
                if (rs != null && rs.Value != null)
                {
                    dynamic dyn = rs.Value;

                    var oHeader = new TQ04HeaderViewModel()
                    {
                        Id = dyn.Id,
                        PoId = dyn.PoId,
                        PoCode = dyn.PoCode,
                        PoName = dyn.PoCode + " - " + dyn.PoName,
                        ReportDate = dyn.ReportDate,
                        DistrictPoId = dyn.DistrictPoId,
                        DistrictPoCode = dyn.DistrictPoCode,
                        DistrictPoName = dyn.DistrictPoCode + " - " + dyn.DistrictPoName,
                        AmndEmpName = dyn.AmndEmpName

                    };
                    TQ04Header = oHeader;
                }
            }
            ViewBag.reportId = reportId;
            return PartialView(TQ04Header);
        }

        public ActionResult Report03TQGet(TQ04HeaderViewModel TQ04Header = null, int reportID = 0, string reportDate = "", int poId = 0, string isSummary = "N")
        {

            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            if (poId == 0)
                poId = oEmployee.POID;

            Lib.ResponseObject rs = null;
            if (poId == oEmployee.POID)
                rs = Helper.Invoke("GET", string.Format("api/AccountingDistrict/Report03TQSummary?reportID={0}&reportDate={1}&poId={2}&districtPoId={3}&empId={4}&isSummary={5}", new object[] { reportID, TQ04Header.ReportDate, poId, oEmployee.POID, oEmployee.ID, isSummary }), null);

            else
                //rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04TQSummary?id={0}&reportID={1}&reportDate={2}&poId={3}&isSummary={4}", new object[] { TQ04Header.Id, reportID, TQ04Header.ReportDate, poId, isSummary }), null);
             rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04TQSummary?reportID={0}&reportDate={1}&poId={2}&districtPoId={3}&empId={4}&isSummary={5}", new object[] { reportID, TQ04Header.ReportDate, poId, oEmployee.POID, oEmployee.ID, isSummary }), null);

            if (rs != null && rs.Value != null)
            {
                dynamic oValue = rs.Value;
                if (oValue.ListDetail != null)
                {
                    TQ04Header.ListDetail = new List<TQ04DetailViewModel>();
                    foreach (dynamic dyn in oValue.ListDetail)
                    {
                        var oDetail = new TQ04DetailViewModel()
                        {
                            Id = dyn.Id,
                            ItemId = dyn.ItemId,
                            ItemCode = dyn.ItemCode,
                            Formula = dyn.Formula,
                            DisplayName = dyn.DisplayName,
                            ParentId = dyn.ParentId,
                            IsVisible = dyn.IsVisible,
                            ItemGroupId = dyn.ItemGroupId,
                            FontBold = dyn.FontBold,
                            EditMode = dyn.EditMode,
                            AllowSummary = dyn.AllowSummary,
                            AllowVnd = dyn.AllowVnd,
                            Amount = dyn.Amount,
                            VisibleLevel = dyn.VisibleLevel
                        };

                        if (!TQ04Header.ListDetail.Contains(oDetail))
                            TQ04Header.ListDetail.Add(oDetail);
                    }
                }

            }

            ViewBag.reportID = reportID;
            return PartialView(TQ04Header);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Report04TQUpdate(TQ04HeaderViewModel TQ04Header)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                var rs = Helper.Invoke("POST", "api/AccountingCounter/ReportTQUpdate", TQ04Header);
                if (rs != null && rs.Value != null)
                {
                    jResult = Json(new { Code = rs.Code, Mes = rs.Message, Value = rs.Value }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        public JsonResult Report04TQDelete(int id)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                var datarequest = new TQ04HeaderViewModel
                {
                    Id = id,
                    PoId = oEmployee.POID
                };
                var rs = Helper.Invoke("POST", "api/AccountingCounter/ReportTQDelete", datarequest);
                if (rs != null)
                {
                    jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        #endregion

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.Report_District_ReportPOManage })]
        public ActionResult ReportFundTotal()
        {
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            ViewBag.PO_ID = oEmployee.POID;
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District })]
        [ValidateAntiForgeryToken]
        public ActionResult ReportFundTotal(FormCollection form)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                jResult = Json(new { Code = "00", Mes = "Tổng hợp dữ liệu thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult ReportFundTotalGet(int pageIndex = 1)
        {
            string fromDate = "";
            string toDate = "";
            int Total = 0;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<R_FundInfo> listFundTotal = new List<R_FundInfo>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/ReportFundTotalGet?poId={0}&fromDate={1}&toDate={2}&PageIndex={3}&PageSize={4}", new object[] { oEmployee.POID, fromDate, toDate, pageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    var oHeader = new R_FundInfo()
                    {
                        ID = dyn.ID,
                        PO_TYPE = 1,
                        PO_CODE = dyn.PO_CODE,
                        PO_NAME = dyn.PO_CODE.ToString() + " - " + dyn.PO_NAME.ToString(),
                        PARENT_CODE = dyn.PARENT_CODE,
                        PARENT_NAME = dyn.PARENT_NAME,
                        FUND_TT = dyn.FUND_TT,
                        FUND_TDV = dyn.FUND_TDV,
                        FUND_KD = dyn.FUND_KD,
                        FUND_TKBD = dyn.FUND_TKBD,
                        FUND_TOTAL = dyn.FUND_TOTAL,
                        FUND_NEED = dyn.FUND_NEED,
                        FUND_NEED_TKBD = dyn.FUND_NEED_TKBD

                    };

                    if (!listFundTotal.Contains(oHeader))
                        listFundTotal.Add(oHeader);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;

            return PartialView(listFundTotal);
        }
    }
}