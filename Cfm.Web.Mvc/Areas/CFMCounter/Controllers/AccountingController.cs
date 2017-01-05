using Cfm.Web.Mvc.Areas.Admin.Controllers;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter })]
    public class AccountingController : BaseController
    {
        // GET: CFMCounter/Accounting
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.FunctionPO_Report04CD })]
        public ActionResult Report04CD(int reportID = 0, string reportDate = "")
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
            CD04Header.PoId = oEmployee.POID;
            CD04Header.ApprovedEmpId = oEmployee.ID;
            //if (reportDate != "")
            //    CD04Header.ReportDate = reportDate;
            //else
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
            ViewBag.PoID = oEmployee.POID;
            ViewBag.ListItemGroupID = ListItemGroupID;
            ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
            ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
            ViewBag.ListItemGroupID_BS = ListItemGroupID_BS;
            ViewBag.ListItemGroupID_General = ListItemGroupID_General;
            ViewBag.reportDate = CD04Header.ReportDate;
            return View(CD04Header);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch})]
        public ActionResult Report04CDGetData(int reportID = 0, string reportDate = "", string isAgain = "N", int PoId = 0)
        {
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            CD04HeaderViewModel CD04Header = new CD04HeaderViewModel();
            CD04Header.ListDetail = new List<CD04DetailViewModel>();

            if (PoId > 0 && oEmployee.POID != PoId)
            {
                var poReplace = Repository.GetPOByID(PoId);
                PostOfficeViewModel po = null;
                var poManage = Repository.GetPOByID(oEmployee.POID);
                if (poManage.POLevel == (int)Constant.POLevel.Branch)
                    po = Repository.GetPOByID(poReplace.ParentID);
                else
                    po = poReplace;

                //PostOfficeViewModel oPo = new PostOfficeViewModel();
                //oPo = Repository.GetPOByID(PoId);

                if (po == null || po.ParentID != oEmployee.POID)
                {
                    return null;
                }
                else
                {
                    CD04Header.PoId = PoId;
                    CD04Header.PoName = poReplace.Name;
                    CD04Header.PoCode = poReplace.Code;
                    CD04Header.IsApproved = true;
                }
            }
            else
            {
                CD04Header.PoId = oEmployee.POID;
            }

            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            List<int> ListItemGroupID_TDV = new List<int>();
            List<int> ListItemGroupID_TKBD = new List<int>();
            List<int> ListItemGroupID_BS = new List<int>();
            List<int> ListItemGroupID_General = new List<int>();

            CD04Header.ApprovedEmpId = oEmployee.ID;
            if (reportDate != "")
                CD04Header.ReportDate = reportDate;
            else
                CD04Header.ReportDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
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
            ViewBag.ListItemGroupID = ListItemGroupID;
            ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
            ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
            ViewBag.ListItemGroupID_BS = ListItemGroupID_BS;
            ViewBag.ListItemGroupID_General = ListItemGroupID_General;
            ViewBag.isAgain = isAgain;
            ViewBag.PoID = CD04Header.PoId;
            ViewBag.reportDate = CD04Header.ReportDate;
            return PartialView(CD04Header);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult Report04CDGet(CD04HeaderViewModel CD04Header = null, int reportID = 0, int itemGroupId = 0, int cashFllowID = 0, string isAgain = "N", string ReportDate = "", int PoId = 0)
        {
            // CD04HeaderViewModel CD04Header = new CD04HeaderViewModel();

            if (PoId > 0)
            {
                CD04Header.PoId = PoId;

            }

            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04CDSummary?id={0}&reportID={1}&itemGroupID={2}&reportDate={3}&cashFllowId={4}&poId={5}&isAgain={6}", new object[] { 0, reportID, itemGroupId, CD04Header.ReportDate, cashFllowID, CD04Header.PoId, isAgain }), null);
            if (rs != null && rs.Value != null)
            {
                dynamic oValue = rs.Value;
                if (oValue.ListDetail != null)
                {

                    CD04Header = CD04Header ?? new CD04HeaderViewModel();

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
                            VisibleLevel = dyn.VisibleLevel,
                            IsSummary = dyn.IsSummary,
                            AllowShowReport = dyn.AllowShowReport,
                            AllowSummaryBottom = dyn.AllowSummaryBottom
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

        public ActionResult Report04CDCreate()
        {
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District })]
        public JsonResult Report04CDUpdate(CD04HeaderViewModel CD04Header)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                PostOfficeViewModel oPo = (PostOfficeViewModel)Session[Constant.PO_SESSION];
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                if (oPo.POLevel == (int)Constant.POLevel.District)
                {
                    CD04Header.IsApproved = true;
                    CD04Header.PoApprovedId = oPo.ID;
                    CD04Header.ApprovedEmpId = oEmployee.ID;
                }

                var rs = Helper.Invoke("POST", "api/AccountingCounter/ReportCDUpdate", CD04Header);
                if (rs != null )
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

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Report04CDApprove(int poId = 0, string reportDate = "")
        {
            JsonResult jResult = new JsonResult();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            var CD04Header = new CD04HeaderViewModel
            {
                ReportDate = reportDate,
                PoApprovedId = oEmployee.POID,
                ApprovedEmpId = oEmployee.ID,
                PoId = poId
            };
            try
            {
                var rs = Helper.Invoke("POST", "api/AccountingCounter/ReportCDApprove", CD04Header);
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
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.FuntionPO_Report04CDManage })]
        public ActionResult Report04CDManage()
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

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District })]
        public ActionResult Report04CDManageGet(string dateRange = "", string reportStatus = "", int PageIndex = 1)
        {
            string fromDate = "";
            string toDate = "";
            if (dateRange == "")
            {
                fromDate = DateTime.Now.ToString("dd/MM/yyyy");
                toDate = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            int Total = 0;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<CD04HeaderViewModel> listReportHeader = new List<CD04HeaderViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04CDManage?poId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}", new object[] { oEmployee.POID, fromDate, toDate, reportStatus, PageIndex, Common.Constant.PageSize }), null);
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

        #region Báo cáo 04-TQ

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.FuntionPO_Report04TQManage })]
        public ActionResult Report04TQManage(int reportId = 0)
        {
            ViewBag.reportId = reportId;
            return View();
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District })]
        public ActionResult Report04TQManageGet(string dateRange = "", int PageIndex = 1, int reportId = 0)
        {
            string fromDate = "";
            string toDate = "";
            if (dateRange == "")
            {
                fromDate =Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            else
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            int Total = 0;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<TQ04HeaderViewModel> listReportHeader = new List<TQ04HeaderViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04TQManage?poId={0}&fromDate={1}&toDate={2}&PageIndex={3}&PageSize={4}", new object[] { oEmployee.POID, fromDate, toDate, PageIndex, Common.Constant.PageSize }), null);
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

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District })]
        public ActionResult Report04TQCreate(int id = 0, int reportId = 0)
        {
            TQ04HeaderViewModel TQ04Header = null;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            if (id == 0)
            {
                TQ04Header = new Models.ViewModel.TQ04HeaderViewModel();

                PostOfficeViewModel oPo = (PostOfficeViewModel)Session[Constant.PO_SESSION];
                
                TQ04Header.ReportDate = Session[Constant.TIMEWORK_SESSION].ToString();
                TQ04Header.PoId = oEmployee.POID;
                TQ04Header.AmndEmpId = oEmployee.ID;
                PostOfficeViewModel po = Repository.GetPOByID(oPo.ParentID);
                if (po != null)
                    TQ04Header.DistrictPoId = po.ID;
            }
            else
            {
                var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04TQGetById?id={0}&poId={1}", new object[] { id, oEmployee.POID }), null);
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

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District })]
        public ActionResult Report04TQGet(TQ04HeaderViewModel TQ04Header = null, int reportID = 0, string reportDate = "", string isSummary = "N")
        {

            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];

            PostOfficeViewModel po = Repository.GetPOByID(oEmployee.POID);
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04TQSummary?reportID={0}&reportDate={1}&poId={2}&districtPoId={3}&empId={4}&isSummary={5}", new object[] {  reportID, TQ04Header.ReportDate, oEmployee.POID, po.ParentID, oEmployee.ID, isSummary }), null);
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

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District })]
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
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District })]
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
    }
}