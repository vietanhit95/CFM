using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Common;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center })]
    public class ReportConfigurationController : BaseController
    {
        // GET: Admin/ReportConfiguration
        public ActionResult Index()
        {
            return View();
        }

        #region Dampt Create

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ConfigReport(int ReportId = 0, string ReportType = "")
        {
            ViewBag.ReportId = ReportId;
            ViewBag.ReportType = ReportType;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ConfigTH(int ReportId = 0, string ReportType = "")
        {
            List<ItemGroupViewModel> ListItemGroup = new List<ItemGroupViewModel>();
            int Total = 0;

            try
            {
                string Url = string.Format("api/Dictionary/ItemGroupSearch/{0}?Code={1}&Name={2}&Status={3}&ReportId={4}&PageIndex={5}&PageSize={6}", 0, "", "", "", ReportId, 0, Constant.PageSize);
                var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                if (rs.Code == "00")
                {
                    int.TryParse(rs.Value.ToString(), out Total);
                    dynamic oValue = rs.ListValue;
                    foreach (var item in oValue)
                    {
                        ItemGroupViewModel oItemGroup = new ItemGroupViewModel();
                        oItemGroup.ID = Convert.ToInt32(item.ID);
                        oItemGroup.Code = (item.Code).ToString();
                        oItemGroup.Name = (item.Name).ToString();
                        oItemGroup.ReportId = Convert.ToInt32(item.ReportId);
                        oItemGroup.Status = (item.Status).ToString();
                        oItemGroup.VisibleIndex = Convert.ToInt32(item.VisibleIndex);
                        ListItemGroup.Add(oItemGroup);
                    }
                }
                else
                {
                    ListItemGroup = null;
                }
            }
            catch
            {
                ListItemGroup = null;
            }

            ViewBag.ReportType = ReportType;
            return PartialView(ListItemGroup);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ConfigTHCreate(int id = 0, int reportID = 0, int itemGroupID = 0, int parentId = 0, string reportType = "", int VisibleLevel = 1, int VisibleIndex = 1)
        {
            ReportConfigViewModel oReportConfigViewModel = new ReportConfigViewModel();


            List<SelectListItem> ListEditMode = new List<SelectListItem>();
            int Total = 0;
            #region ListEditMode Kiểu nhập liệu
            foreach (var item in Repository.ListItemEditMode)
            {
                ListEditMode.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString()
                });
            }
            #endregion

            if (id > 0)
            {
                var rs = Helper.Invoke("GET", string.Format("api/Configuration/GetReportConfig?id={0}&reportID ={1}&itemGroupID ={2}&isVisible={3}", new object[] { id, reportID, itemGroupID, string.Empty }), null);
                if (rs != null && rs.ListValue != null)
                {
                    dynamic dyn = rs.ListValue[0];
                    oReportConfigViewModel = new ReportConfigViewModel()
                    {
                        Id = dyn.Id,
                        ReportId = dyn.ReportId,
                        ItemId = dyn.ItemId,
                        ItemCode = dyn.ItemCode,
                        ItemName = dyn.ItemName,
                        DisplayName = dyn.DisplayName,
                        ParentId = dyn.ParentId,
                        VisibleLevel = dyn.VisibleLevel,
                        VisibleIndex = dyn.VisibleIndex,
                        IsVisible = dyn.IsVisible,
                        ItemGroupId = dyn.ItemGroupId,
                        IsEmpty = dyn.IsEmpty,
                        FontBold = dyn.FontBold,
                        EditMode = dyn.EditMode,
                        AllowSummary = dyn.AllowSummary,
                        AllowVnd = dyn.AllowVnd,
                        AllowUsd = dyn.AllowUsd,
                        AllowAccumulate = dyn.AllowAccumulate,
                        AccumulateUsd = dyn.AccumulateUsd,
                        AllowShowReport = dyn.AllowShowReport,
                        AllowSummaryBottom = dyn.AllowSummaryBottom
                    };
                }

            }
            else
            {
                oReportConfigViewModel = new ReportConfigViewModel();

                oReportConfigViewModel.ReportId = reportID;
                oReportConfigViewModel.ItemGroupId = itemGroupID;
                oReportConfigViewModel.ParentId = parentId;
                oReportConfigViewModel.VisibleIndex = VisibleIndex;
                oReportConfigViewModel.VisibleLevel = VisibleLevel;
            }

            #region Danh sách Dịch vụ
            try
            {
                string Url = string.Format("api/Dictionary/ItemListSearch/{0}?Code={1}&Name={2}&IsReceiptItem={3}&MoneyType={4}&ItemTypeId={5}&IsSummary={6}&ParentId={7}&PartnerId={8}&BudgetTypeId={9}&BorrowTypeId={10}&CashFlowID={11}&PageIndex={12}&PageSize={13}",
                    0, "", "", "", 0, 0, "", 0, 0, 0, 0, 0, 1, Constant.PageSize);
                var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                if (rs.Code == "00")
                {
                    oReportConfigViewModel.ListItem = new List<ItemListViewModel>();
                    int.TryParse(rs.Value.ToString(), out Total);
                    dynamic oValue = rs.ListValue;
                    foreach (var item in oValue)
                    {
                        ItemListViewModel oItem = new ItemListViewModel();
                        oItem.Id = int.Parse("0" + (item.Id ?? "0").ToString());
                        oItem.Code = (item.Code ?? "").ToString();
                        oItem.Name = (item.Name ?? "").ToString();
                        oItem.IsReceiptItem = (item.IsReceiptItem ?? "").ToString();
                        oItem.MoneyType = int.Parse("0" + (item.MoneyType ?? "0").ToString());
                        oItem.ItemTypeId = int.Parse("0" + (item.ItemTypeId ?? "0").ToString());
                        oItem.IsSummary = (item.IsSummary ?? "N").ToString();
                        oItem.bIsSummary = (oItem.IsSummary == "" || oItem.IsSummary == "N" ? false : true);
                        oItem.Formula = (item.Formula ?? "").ToString();
                        oItem.ParentId = int.Parse("0" + (item.ParentId ?? "0").ToString());
                        oItem.PartnerId = int.Parse("0" + (item.PartnerId ?? "0").ToString());
                        oItem.BudgetTypeId = int.Parse("0" + (item.BudgetTypeId ?? "0").ToString());
                        oItem.BorrowTypeId = int.Parse("0" + (item.BorrowTypeId ?? "0").ToString());
                        oItem.CashFlowId = int.Parse("0" + (item.CashFlowId ?? "0").ToString());
                        oItem.Description = (item.Description ?? "").ToString();
                        oReportConfigViewModel.ListItem.Add(oItem);
                    }
                }
                else
                {
                    oReportConfigViewModel.ListItem = null;
                }
            }
            catch
            {
                oReportConfigViewModel.ListItem = null;
            }
            #endregion


            ViewBag.PageIndex = 1;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.ListEditMode = ListEditMode;
            ViewBag.ReportType = reportType;
            return PartialView(oReportConfigViewModel);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ConfigTHGet(int reportID = 0, int itemGroupId = 0, string reportType = "")
        {
            List<ReportConfigViewModel> ListReportConfig = new List<ReportConfigViewModel>();
            int Total = 0;

            try
            {
                string Url = string.Format("api/Configuration/GetReportConfig?id={0}&reportID={1}&itemGroupID={2}&isVisible={3}", 0, reportID, itemGroupId, "");
                var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                if (rs.Code == "00")
                {
                    int.TryParse((rs.Value ?? "").ToString(), out Total);
                    dynamic oValue = rs.ListValue;
                    foreach (var item in oValue)
                    {
                        ReportConfigViewModel oItemReportConfig = new ReportConfigViewModel();
                        oItemReportConfig.Id = Convert.ToInt32("0" + (item.Id ?? "0").ToString());
                        oItemReportConfig.ReportId = int.Parse("0" + (item.ReportId ?? "0").ToString());
                        oItemReportConfig.ItemId = int.Parse("0" + (item.ItemId ?? "0").ToString());
                        oItemReportConfig.ItemCode = (item.ItemCode ?? "0").ToString();
                        oItemReportConfig.ItemName = (item.ItemName ?? "").ToString();
                        oItemReportConfig.DisplayName = (item.DisplayName ?? "").ToString();
                        oItemReportConfig.ParentId = int.Parse("0" + (item.ParentId ?? "0").ToString());
                        oItemReportConfig.VisibleLevel = int.Parse("0" + (item.VisibleLevel ?? "0").ToString());
                        oItemReportConfig.VisibleIndex = int.Parse("0" + (item.VisibleIndex ?? "0").ToString());
                        oItemReportConfig.IsVisible = bool.Parse((item.IsVisible ?? false).ToString());
                        oItemReportConfig.ItemGroupId = int.Parse("0" + (item.ItemGroupId ?? "0").ToString());
                        oItemReportConfig.IsEmpty = bool.Parse((item.IsEmpty ?? false).ToString());
                        oItemReportConfig.FontBold = bool.Parse((item.FontBold ?? false).ToString());
                        oItemReportConfig.EditMode = int.Parse("0" + (item.EditMode ?? "0").ToString());
                        oItemReportConfig.AllowSummary = bool.Parse((item.AllowSummary ?? false).ToString());
                        oItemReportConfig.AllowVnd = bool.Parse((item.AllowVnd ?? false).ToString());
                        oItemReportConfig.AllowUsd = bool.Parse((item.AllowUsd ?? false).ToString());
                        oItemReportConfig.AccumulateVnd = bool.Parse((item.AccumulateVnd ?? false).ToString());
                        oItemReportConfig.AllowAccumulate = bool.Parse((item.AllowAccumulate ?? false).ToString());
                        oItemReportConfig.AccumulateUsd = bool.Parse((item.AccumulateUsd ?? false).ToString());
                        oItemReportConfig.AllowShowReport = bool.Parse((item.AllowShowReport ?? false).ToString());
                        oItemReportConfig.AllowSummaryBottom = bool.Parse((item.AllowSummaryBottom ?? false).ToString());

                        ListReportConfig.Add(oItemReportConfig);
                    }
                }
                else
                {
                    ListReportConfig = null;
                }
            }
            catch
            {
                ListReportConfig = null;
            }

            ViewBag.ReportId = reportID;
            ViewBag.GroupId = itemGroupId;
            ViewBag.ReportType = reportType;
            return PartialView(ListReportConfig);
        }

        #endregion

        #region Cấu hình báo cáo cân đối

        public ActionResult ConfigCD(int reportID = 0, string reportName = "")
        {
            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            List<int> ListItemGroupID_TDV = new List<int>();
            List<int> ListItemGroupID_TKBD = new List<int>();
            List<int> ListItemGroupID_BS = new List<int>();
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
                }
            }

            ViewBag.ReportID = reportID;
            ViewBag.PageHeader = "Cấu hình " + reportName;
            ViewBag.ListItemGroupID = ListItemGroupID;
            ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
            ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
            ViewBag.ListItemGroupID_BS = ListItemGroupID_BS;
            return View();
        }

        public ActionResult Config04CD(int reportID = 0, string reportName = "")
        {
            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            List<int> ListItemGroupID_TDV = new List<int>();
            List<int> ListItemGroupID_TKBD = new List<int>();
            List<int> ListItemGroupID_BS = new List<int>();
            List<int> ListItemGroupID_General = new List<int>();
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
            ViewBag.PageHeader = "Cấu hình " + reportName;
            ViewBag.ListItemGroupID = ListItemGroupID;
            ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
            ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
            ViewBag.ListItemGroupID_BS = ListItemGroupID_BS;
            ViewBag.ListItemGroupID_General = ListItemGroupID_General;
            return View();
        }
        public ActionResult ConfigCDCreate(int id = 0, int reportID = 0, int itemGroupID = 0, int cashFllowID = 0, int parentId = 0, int VisibleLevel = 1, int VisibleIndex = 1)
        {
            //Get list Item
            List<ItemListViewModel> ListItem = new List<ItemListViewModel>();
            List<SelectListItem> ListItemEditMode = new List<SelectListItem>();
            int Total = 0;

            try
            {
                string Url = string.Format("api/Dictionary/ItemListSearch/{0}?Code={1}&Name={2}&IsReceiptItem={3}&MoneyType={4}&ItemTypeId={5}&IsSummary={6}&ParentId={7}&PartnerId={8}&BudgetTypeId={9}&BorrowTypeId={10}&CashFlowID={11}&PageIndex={12}&PageSize={13}",
                    0, "", "", "", 0, 0, "", 0, 0, 0, 0, cashFllowID, 1, Constant.PageSize);
                var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                if (rs.Code == "00")
                {
                    int.TryParse(rs.Value.ToString(), out Total);
                    dynamic oValue = rs.ListValue;
                    string sCode = "";
                    foreach (var item in oValue)
                    {
                        ItemListViewModel oItem = new ItemListViewModel();
                        oItem.Id = int.Parse("0" + (item.Id ?? "0").ToString());
                        sCode = (item.Code ?? "").ToString();
                        if (sCode.Length > 18)
                            oItem.CodeDisplay = sCode.Substring(0, 15) + "...";
                        else
                            oItem.CodeDisplay = sCode;
                        oItem.Code = sCode;
                        oItem.Name = (item.Name ?? "").ToString();
                        oItem.IsReceiptItem = (item.IsReceiptItem ?? "").ToString();
                        oItem.MoneyType = int.Parse("0" + (item.MoneyType ?? "0").ToString());
                        oItem.ItemTypeId = int.Parse("0" + (item.ItemTypeId ?? "0").ToString());
                        oItem.IsSummary = (item.IsSummary ?? "N").ToString();
                        oItem.bIsSummary = (oItem.IsSummary == "" || oItem.IsSummary == "N" ? false : true);
                        oItem.Formula = (item.Formula ?? "").ToString();
                        oItem.ParentId = int.Parse("0" + (item.ParentId ?? "0").ToString());
                        oItem.PartnerId = int.Parse("0" + (item.PartnerId ?? "0").ToString());
                        oItem.BudgetTypeId = int.Parse("0" + (item.BudgetTypeId ?? "0").ToString());
                        oItem.BorrowTypeId = int.Parse("0" + (item.BorrowTypeId ?? "0").ToString());
                        oItem.CashFlowId = int.Parse("0" + (item.CashFlowId ?? "0").ToString());
                        oItem.Description = (item.Description ?? "").ToString();
                        ListItem.Add(oItem);
                    }
                }
                else
                {
                    ListItem = null;
                }
            }
            catch
            {
                ListItem = null;
            }

            //Get Report config
            ReportConfigViewModel report = null;
            if (id > 0)
            {
                var rs = Helper.Invoke("GET", string.Format("api/Configuration/GetReportConfig?id={0}&reportID ={1}&itemGroupID ={2}&isVisible={3}", new object[] { id, reportID, itemGroupID, string.Empty }), null);
                if (rs != null && rs.ListValue != null)
                {
                    dynamic dyn = rs.ListValue[0];
                    var oReport = new ReportConfigViewModel()
                    {
                        Id = dyn.Id,
                        ReportId = dyn.ReportId,
                        ItemId = dyn.ItemId,
                        ItemCode = dyn.ItemCode,
                        DisplayName = dyn.DisplayName,
                        ParentId = dyn.ParentId,
                        VisibleLevel = dyn.VisibleLevel,
                        VisibleIndex = dyn.VisibleIndex,
                        IsVisible = dyn.IsVisible,
                        ItemGroupId = dyn.ItemGroupId,
                        IsEmpty = dyn.IsEmpty,
                        FontBold = dyn.FontBold,
                        EditMode = dyn.EditMode,
                        AllowSummary = dyn.AllowSummary,
                        AllowVnd = dyn.AllowVnd,
                        AllowUsd = dyn.AllowUsd,
                        AllowShowReport = dyn.AllowShowReport,
                        AllowSummaryBottom = dyn.AllowSummaryBottom,
                        DisableFormula=dyn.DisableFormula
                    };
                    report = oReport;

                }

            }
            else
            {
                report = new ReportConfigViewModel();
                report.ReportId = reportID;
                report.ItemGroupId = itemGroupID;
                report.VisibleLevel = VisibleLevel;
                report.VisibleIndex = VisibleIndex;
                report.AllowShowReport = true;
                report.AllowSummaryBottom = false;
                report.DisableFormula = false;
            }

            foreach (var item in Repository.ListItemEditMode)
            {
                ListItemEditMode.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }
            if (id == 0)
                report.ParentId = parentId;
            report.ListItem = ListItem;
            ViewBag.ListItemEditMode = ListItemEditMode;
            ViewBag.PageIndex = 1;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.reportID = reportID;
            ViewBag.itemGroupID = itemGroupID;
            return PartialView(report);
        }

        public ActionResult ConfigCDGet(int reportID = 0, int itemGroupId = 0, int cashFllowID = 0)
        {
            List<ReportConfigViewModel> list = new List<ReportConfigViewModel>();

            var rs = Helper.Invoke("GET", string.Format("api/Configuration/GetReportConfig?id={0}&reportID={1}&itemGroupID={2}&isVisible={3}", new object[] { 0, reportID, itemGroupId, "N" }), null);
            if (rs != null && rs.ListValue != null)
            {
                dynamic arrList = rs.ListValue;
                string sCode = "";
                foreach (dynamic dyn in arrList)
                {
                    sCode = dyn.ItemCode;
                    if (sCode.Length > 15)
                        sCode = sCode.Substring(0, 12) + "...";

                    var oReport = new ReportConfigViewModel()
                    {
                        Id = dyn.Id,
                        ReportId = dyn.ReportId,
                        ItemId = dyn.ItemId,
                        ItemCodeDisplay = sCode,
                        ItemCode = dyn.ItemCode,
                        DisplayName = dyn.DisplayName,
                        ParentId = dyn.ParentId,
                        VisibleLevel = dyn.VisibleLevel,
                        VisibleIndex = dyn.VisibleIndex,
                        IsVisible = dyn.IsVisible,
                        ItemGroupId = dyn.ItemGroupId,
                        IsEmpty = dyn.IsEmpty,
                        FontBold = dyn.FontBold,
                        EditMode = dyn.EditMode,
                        AllowSummary = dyn.AllowSummary,
                        AllowVnd = dyn.AllowVnd,
                        AllowUsd = dyn.AllowUsd,
                        AllowShowReport = dyn.AllowShowReport,
                        AllowSummaryBottom = dyn.AllowSummaryBottom,
                        DisableFormula=dyn.DisableFormula
                    };

                    if (!list.Contains(oReport))
                        list.Add(oReport);
                }

            }

            ViewBag.reportID = reportID;
            ViewBag.itemGroupID = itemGroupId;
            ViewBag.cashFllowID = cashFllowID;
            return PartialView(list);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        //[ValidateAntiForgeryToken]
        public JsonResult ConfigCDUpdate(ReportConfigViewModel report)
        {

            JsonResult jResult = new JsonResult();
            if ((report.DisplayName == "" || report.DisplayName == null) && !report.IsEmpty)
            {
                jResult = Json(new { Code = "05", Mes = "Tên hiển thị không được để trắng." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Lib.ResponseObject rs = null;

                // ItemListViewModel item = report.ListItem.Where(t => t.Selected == true).f();
                if (report.IsEmpty)
                {
                    report.ItemId = 0;
                    report.DisplayName = "";
                    report.AllowVnd = false;
                    report.AllowUsd = false;
                    report.FontBold = false;
                    report.AllowSummary = false;

                }
                //else
                //{
                //    foreach (ItemListViewModel item in report.ListItem)
                //    {
                //        if (item.Selected)
                //        {
                //            report.ItemId = Convert.ToInt32(item.Id);
                //            break;
                //        }
                //    }
                //}

                if (report.Id > 0)
                    rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Configuration/UpdateReportConfig", report);
                else
                    rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Configuration/AddReportConfig", report);
                if (rs != null)
                {
                    jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { Code = "96", Mes = "Lỗi hệ thống. Vui lòng liên hệ với quản trị." }, JsonRequestBehavior.AllowGet);
            }


            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult ConfigDelete(ReportConfigViewModel report)
        {
            JsonResult jResult = new JsonResult();

            Lib.ResponseObject rs = null;

            rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Configuration/DeleteReportConfig", report);

            if (rs != null)
            {
                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            else
                jResult = Json(new { Code = "96", Mes = "Lỗi hệ thống. Vui lòng liên hệ với quản trị." }, JsonRequestBehavior.AllowGet);

            return jResult;
        }

        #endregion

        #region Cấu hình báo cáo tiếp quỹ

        public ActionResult ConfigTQ(int reportID = 0, string reportName = "")
        {
            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            int Total = 0;

            string Url = string.Format("api/Dictionary/ItemGroupSearch/{0}?Code={1}&Name={2}&Status={3}&ReportId={6}&PageIndex={4}&PageSize={5}", 0, "", "", "", 0, Constant.PageSize, reportID);
            var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

            if (rs.Code == "00")
            {
                int.TryParse(rs.Value.ToString(), out Total);
                dynamic oValue = rs.ListValue;
                foreach (var item in oValue)
                {
                    ListItemGroupID.Add(Convert.ToInt32(item.ID));
                }
            }

            ViewBag.ReportID = reportID;
            ViewBag.PageHeader = "Cấu hình " + reportName;
            ViewBag.ListItemGroupID = ListItemGroupID;
            return View();
        }

        public ActionResult ConfigTQCreate(int id = 0, int reportID = 0, int itemGroupID = 0, int parentId = 0, int VisibleLevel = 1, int VisibleIndex = 1)
        {
            //Get list Item
            List<ItemListViewModel> ListItem = new List<ItemListViewModel>();
            List<SelectListItem> ListItemEditMode = new List<SelectListItem>();
            int Total = 0;

            try
            {
                string Url = string.Format("api/Dictionary/ItemListSearch/{0}?Code={1}&Name={2}&IsReceiptItem={3}&MoneyType={4}&ItemTypeId={5}&IsSummary={6}&ParentId={7}&PartnerId={8}&BudgetTypeId={9}&BorrowTypeId={10}&CashFlowID={11}&PageIndex={12}&PageSize={13}",
                    0, "", "", "", 0, 0, "", 0, 0, 0, 0, 0, 1, Constant.PageSize);
                var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                if (rs.Code == "00")
                {
                    int.TryParse(rs.Value.ToString(), out Total);
                    dynamic oValue = rs.ListValue;
                    foreach (var item in oValue)
                    {
                        ItemListViewModel oItem = new ItemListViewModel();
                        oItem.Id = int.Parse("0" + (item.Id ?? "0").ToString());
                        oItem.Code = (item.Code ?? "").ToString();
                        oItem.Name = (item.Name ?? "").ToString();
                        oItem.IsReceiptItem = (item.IsReceiptItem ?? "").ToString();
                        oItem.MoneyType = int.Parse("0" + (item.MoneyType ?? "0").ToString());
                        oItem.ItemTypeId = int.Parse("0" + (item.ItemTypeId ?? "0").ToString());
                        oItem.IsSummary = (item.IsSummary ?? "N").ToString();
                        oItem.bIsSummary = (oItem.IsSummary == "" || oItem.IsSummary == "N" ? false : true);
                        oItem.Formula = (item.Formula ?? "").ToString();
                        oItem.ParentId = int.Parse("0" + (item.ParentId ?? "0").ToString());
                        oItem.PartnerId = int.Parse("0" + (item.PartnerId ?? "0").ToString());
                        oItem.BudgetTypeId = int.Parse("0" + (item.BudgetTypeId ?? "0").ToString());
                        oItem.BorrowTypeId = int.Parse("0" + (item.BorrowTypeId ?? "0").ToString());
                        oItem.CashFlowId = int.Parse("0" + (item.CashFlowId ?? "0").ToString());
                        oItem.Description = (item.Description ?? "").ToString();
                        ListItem.Add(oItem);
                    }
                }
                else
                {
                    ListItem = null;
                }
            }
            catch
            {
                ListItem = null;
            }


            //Get Report config
            ReportConfigViewModel report = null;
            if (id > 0)
            {
                var rs = Helper.Invoke("GET", string.Format("api/Configuration/GetReportConfig?id={0}&reportID ={1}&itemGroupID ={2}&isVisible={3}", new object[] { id, reportID, itemGroupID, string.Empty }), null);
                if (rs != null && rs.ListValue != null)
                {
                    dynamic dyn = rs.ListValue[0];
                    var oReport = new ReportConfigViewModel()
                    {
                        Id = dyn.Id,
                        ReportId = dyn.ReportId,
                        ItemId = dyn.ItemId,
                        ItemCode = dyn.ItemCode,
                        DisplayName = dyn.DisplayName,
                        ParentId = dyn.ParentId,
                        VisibleLevel = dyn.VisibleLevel,
                        VisibleIndex = dyn.VisibleIndex,
                        IsVisible = dyn.IsVisible,
                        ItemGroupId = dyn.ItemGroupId,
                        IsEmpty = dyn.IsEmpty,
                        FontBold = dyn.FontBold,
                        EditMode = dyn.EditMode,
                        AllowSummary = dyn.AllowSummary,
                        AllowVnd = dyn.AllowVnd,
                        AllowUsd = dyn.AllowUsd,
                        AllowShowReport = dyn.AllowShowReport,
                        AllowSummaryBottom = dyn.AllowSummaryBottom
                    };
                    report = oReport;

                }

            }
            else
            {
                report = new ReportConfigViewModel();
                report.ReportId = reportID;
                report.ItemGroupId = itemGroupID;
                report.VisibleLevel = VisibleLevel;
                report.VisibleIndex = VisibleIndex;
            }


            foreach (var item in Repository.ListItemEditMode)
            {
                ListItemEditMode.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }

            if (id == 0)
                report.ParentId = parentId;

            report.ListItem = ListItem;
            ViewBag.ListItemEditMode = ListItemEditMode;
            ViewBag.PageIndex = 1;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.reportID = reportID;
            ViewBag.itemGroupID = itemGroupID;
            return PartialView(report);
        }

        public ActionResult ConfigTQGet(int reportID = 0, int itemGroupId = 0)
        {
            List<ReportConfigViewModel> list = new List<ReportConfigViewModel>();

            var rs = Helper.Invoke("GET", string.Format("api/Configuration/GetReportConfig?id={0}&reportID={1}&itemGroupID={2}&isVisible={3}", new object[] { 0, reportID, itemGroupId, "N" }), null);
            if (rs != null && rs.ListValue != null)
            {
                dynamic arrList = rs.ListValue;

                foreach (dynamic dyn in arrList)
                {
                    var oReport = new ReportConfigViewModel()
                    {
                        Id = dyn.Id,
                        ReportId = dyn.ReportId,
                        ItemId = dyn.ItemId,
                        ItemCode = dyn.ItemCode,
                        DisplayName = dyn.DisplayName,
                        ParentId = dyn.ParentId,
                        VisibleLevel = dyn.VisibleLevel,
                        VisibleIndex = dyn.VisibleIndex,
                        IsVisible = dyn.IsVisible,
                        ItemGroupId = dyn.ItemGroupId,
                        IsEmpty = dyn.IsEmpty,
                        FontBold = dyn.FontBold,
                        EditMode = dyn.EditMode,
                        AllowSummary = dyn.AllowSummary,
                        AllowVnd = dyn.AllowVnd,
                        AllowUsd = dyn.AllowUsd,
                        AllowShowReport = dyn.AllowShowReport,
                        AllowSummaryBottom = dyn.AllowSummaryBottom

                    };

                    if (!list.Contains(oReport))
                        list.Add(oReport);
                }

            }

            ViewBag.reportID = reportID;
            ViewBag.itemGroupID = itemGroupId;
            //ViewBag.cashFllowID = cashFllowID;
            return PartialView(list);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        //[ValidateAntiForgeryToken]
        public JsonResult ConfigTQUpdate(ReportConfigViewModel report)
        {

            JsonResult jResult = new JsonResult();
            if ((report.DisplayName == "" || report.DisplayName == null) && !report.IsEmpty)
            {
                jResult = Json(new { Code = "05", Mes = "Tên hiển thị không được để trắng." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Lib.ResponseObject rs = null;

                // ItemListViewModel item = report.ListItem.Where(t => t.Selected == true).f();
                if (report.IsEmpty)
                {
                    report.ItemId = 0;
                    report.DisplayName = "";
                    report.AllowVnd = false;
                    report.AllowUsd = false;
                    report.FontBold = false;
                    report.AllowSummary = false;

                }
                //else
                //{
                //    foreach (ItemListViewModel item in report.ListItem)
                //    {
                //        if (item.Selected)
                //        {
                //            report.ItemId = Convert.ToInt32(item.Id);
                //            break;
                //        }
                //    }
                //}

                if (report.Id > 0)
                    rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Configuration/UpdateReportConfig", report);
                else
                    rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Configuration/AddReportConfig", report);
                if (rs != null)
                {
                    jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { Code = "96", Mes = "Lỗi hệ thống. Vui lòng liên hệ với quản trị." }, JsonRequestBehavior.AllowGet);
            }


            return jResult;
        }

        public JsonResult ConfigTQDelete(ReportConfigViewModel report)
        {

            JsonResult jResult = new JsonResult();

            Lib.ResponseObject rs = null;

            rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Configuration/DeleteReportConfig", report);

            if (rs != null)
            {
                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            else
                jResult = Json(new { Code = "96", Mes = "Lỗi hệ thống. Vui lòng liên hệ với quản trị." }, JsonRequestBehavior.AllowGet);

            return jResult;
        }

        #endregion
    }
}