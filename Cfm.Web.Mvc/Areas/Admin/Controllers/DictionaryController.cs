using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center })]
    public class DictionaryController : BaseController
    {
        // GET: Admin/Dictionary
        public ActionResult Index()
        {
            return View();
        }

        #region Danh sách Báo cáo

        public ActionResult ReportList()
        {
            return View();
        }

        public ActionResult ReportCreate(int id)
        {
            ReportListViewModel report = null;
            List<SelectListItem> ListReportType = new List<SelectListItem>();
            foreach (var item in Repository.ListReportType)
            {
                ListReportType.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }

            if (id > 0)
            {
                var rs = Helper.Invoke("GET", string.Format("api/Dictionary/GetReportList?id={0}", new object[] { id }), null);
                if (rs != null && rs.ListValue != null)
                {
                    dynamic dyn = rs.ListValue[0];
                    var oReport = new ReportListViewModel()
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
                    report = oReport;

                }

            }
            else
                report = new ReportListViewModel();
            ViewBag.ListReportType = ListReportType;
            return PartialView(report);
        }

        public ActionResult ReportGet(int PageIndex = 1)
        {
            int Total = 0;
            List<ReportListViewModel> listReport = new List<ReportListViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/Dictionary/GetReportList?id={0}&PageIndex = {1}&PageSize ={2}", new object[] { 0, PageIndex, Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                int.TryParse(rs.Value.ToString(), out Total);

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

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            return PartialView(listReport);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult ReportSave(ReportListViewModel report)
        {
            JsonResult jResult = new JsonResult();
            if (Session[Constant.EMP_SESSION] != null)
                report.EmpID = ((EmployeeViewModel)Session[Constant.EMP_SESSION]).ID;

            Lib.ResponseObject rs = null;
            if (report.Id > 0)
                rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Dictionary/UpdateReport", report);
            else
                rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Dictionary/AddReport", report);
            if (rs != null)
            {
                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            else
                jResult = Json(new { Code = "96", Mes = "Lỗi hệ thống. Vui lòng liên hệ với quản trị." }, JsonRequestBehavior.AllowGet);

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult ReportDelete(int id)
        {
            JsonResult jResult = new JsonResult();
            Lib.ResponseObject rs = null;
            var request = new
            {
                Id = id
            };
            rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Dictionary/DeleteReport", request);
            jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            return jResult;
        }

        public ActionResult GetListMenuConfigReport(int PageIndex = 0)
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

        #endregion

        #region Loại chỉ tiêu

        public ActionResult ItemType()
        {
            return View();
        }

        public ActionResult ItemTypeCreate(int id)
        {
            ItemTypeViewModel itemType = null;
            if (id > 0)
            {
                var rs = Helper.Invoke("GET", string.Format("api/Dictionary/GetItemType?id={0}", new object[] { id }), null);
                if (rs != null && rs.ListValue != null)
                {
                    dynamic dyn = rs.ListValue[0];
                    var oItemType = new ItemTypeViewModel()
                    {
                        Id = dyn.Id,
                        Code = dyn.Code,
                        Name = dyn.Name,
                        VisibleIndex = dyn.VisibleIndex
                    };
                    itemType = oItemType;

                }

            }
            else
                itemType = new ItemTypeViewModel();

            return PartialView(itemType);
        }

        public ActionResult ItemTypeGet(int PageIndex = 1)
        {
            int Total = 0;
            List<ItemTypeViewModel> listItemType = new List<ItemTypeViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/Dictionary/GetItemType?id={0}&PageIndex = {1}&PageSize ={2}", new object[] { 0, PageIndex, Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                int.TryParse(rs.Value.ToString(), out Total);

                foreach (dynamic dyn in rs.ListValue)
                {
                    var itemType = new ItemTypeViewModel()
                    {
                        Id = dyn.Id,
                        Code = dyn.Code,
                        Name = dyn.Name,
                        VisibleIndex = dyn.VisibleIndex
                    };
                    if (!listItemType.Contains(itemType))
                    {
                        listItemType.Add(itemType);
                    }
                }
            }

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            return PartialView(listItemType);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult ItemTypeUpdate(ItemTypeViewModel itemType)
        {
            JsonResult jResult = new JsonResult();
            if (Session[Constant.EMP_SESSION] != null)
                itemType.EmpId = ((EmployeeViewModel)Session[Constant.EMP_SESSION]).ID;

            Lib.ResponseObject rs = null;
            if (itemType.Id > 0)
                rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Dictionary/UpdateItemType", itemType);
            else
                rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Dictionary/AddItemType", itemType);
            if (rs != null)
            {
                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            else
                jResult = Json(new { Code = "96", Mes = "Lỗi hệ thống. Vui lòng liên hệ với quản trị." }, JsonRequestBehavior.AllowGet);

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult ItemTypeDelete(int id)
        {
            JsonResult jResult = new JsonResult();
            Lib.ResponseObject rs = null;
            var request = new
            {
                Id = id
            };
            rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/Dictionary/DeleteItemType", request);
            jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            return jResult;
        }

        #endregion

        #region Nhóm Dịch Vụ
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ItemGroup()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ItemGroupGet(string Code = "", string Name = "", int PageIndex = 1)
        {
            List<ItemGroupViewModel> ListItemGroup = new List<ItemGroupViewModel>();
            int Total = 0;

            try
            {
                string Url = string.Format("api/Dictionary/ItemGroupSearch/{0}?Code={1}&Name={2}&Status={3}&PageIndex={4}&PageSize={5}", 0, Code, Name, "", PageIndex, Constant.PageSize);
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
                        oItemGroup.ReportName = (item.ReportName).ToString();
                        oItemGroup.Status = (item.Status).ToString();
                        oItemGroup.VisibleIndex = Convert.ToInt32(item.VisibleIndex);
                        oItemGroup.CashFllowId = Convert.ToInt32(item.CashFllowId);
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

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            return PartialView(ListItemGroup);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CreateItemGroup(string Id = "")
        {
            ItemGroupViewModel oItemGroup = new ItemGroupViewModel();
            List<SelectListItem> ListReport = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();

            #region ListCashFlow Phân loại dòng tiền

            foreach (var item in Repository.ListCashFlow)
            {
                ListCashFlow.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }
            #endregion

            ListReport.Add(new SelectListItem()
            {
                Text = "---Loại Báo cáo---",
                Value = ""
            });

            try
            {
                if (!string.IsNullOrEmpty(Id.Trim()))
                {
                    string Url = string.Format("api/Dictionary/ItemGroupSearch/{0}?Code={1}&Name={2}&Status={3}&PageIndex={4}&PageSize={5}", Id, "", "", "", 1, 1);
                    var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                    int Total = 0;
                    if (rs.Code == "00")
                    {
                        int.TryParse(rs.Value.ToString(), out Total);
                        dynamic oValue = rs.ListValue;
                        foreach (var item in oValue)
                        {
                            oItemGroup.ID = Convert.ToInt32(item.ID);
                            oItemGroup.Code = (item.Code).ToString();
                            oItemGroup.Name = (item.Name).ToString();
                            oItemGroup.ReportId = Convert.ToInt32(item.ReportId);
                            oItemGroup.Status = (item.Status).ToString();
                            oItemGroup.VisibleIndex = Convert.ToInt32(item.VisibleIndex);
                            oItemGroup.CashFllowId = Convert.ToInt32(item.CashFllowId);
                        }
                    }
                }

                var res = Helper.Invoke("GET", string.Format("api/Dictionary/GetReportList?id={0}&PageIndex={1}&PageSize={2}", new object[] { 0, 0, Constant.PageSize }), null);
                if (res != null && res.ListValue != null)
                {

                    foreach (dynamic dyn in res.ListValue)
                    {
                        ListReport.Add(new SelectListItem()
                        {
                            Text = dyn.Name.ToString(),
                            Value = dyn.Id.ToString()
                        });
                    }
                }
            }
            catch
            {
                oItemGroup = new ItemGroupViewModel();
                oItemGroup.CashFllowId = 1;
            }

            ViewBag.ListReport = ListReport;
            ViewBag.ListCashFlow = ListCashFlow;
            return PartialView(oItemGroup);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateItemGroup(ItemGroupViewModel oItemGroup)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                if (ModelState.IsValid)
                {
                    string Url = string.Empty;
                    if (oItemGroup.ID > 0)
                    {
                        Url = "api/Dictionary/ItemGroupUpdate";
                    }
                    else
                    {
                        Url = "api/Dictionary/ItemGroupAdd";
                    }

                    var rs = Helper.Invoke(Constant.Method.POST.ToString(), Url, oItemGroup);

                    jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var message = (from state in ModelState.Values
                                   from error in state.Errors
                                   select error.ErrorMessage).Take(1);

                    jResult = Json(new { Code = "-100", Mes = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu." }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult ItemGroupDelete(string Id)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                string Url = "api/Dictionary/ItemGroupDelete";
                var request = new
                {
                    ID = Id
                };

                var rs = Helper.Invoke(Constant.Method.POST.ToString(), Url, request);

                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu." }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }
        #endregion

        #region Chỉ tiêu dịch vụ
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ItemList()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ItemListGet(string Code = "", string Name = "", int PageIndex = 1, int forConfig = 0, string searchContent = "", int reportid = 0)
        {
            List<ItemListViewModel> ListItem = new List<ItemListViewModel>();
            int Total = 0;
            int iPageSize = 0;

            iPageSize = Constant.PageSize;
            try
            {
                string Url = string.Format("api/Dictionary/ItemListSearch/{0}?Code={1}&Name={2}&IsReceiptItem={3}&MoneyType={4}&ItemTypeId={5}&IsSummary={6}&ParentId={7}&PartnerId={8}&BudgetTypeId={9}&BorrowTypeId={10}&CashFlowID={11}&PageIndex={12}&PageSize={13}&searchContent={14}",
                    0, Code, Name, "", 0, 0, "", 0, 0, 0, 0, 0, PageIndex, iPageSize, searchContent.ToUpper());
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
                        oItem.Cfm_Code = (item.Cfm_Code ?? "").ToString();
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

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = iPageSize;
            ViewBag.ToTal = Total;

            if (forConfig == 1)
            {
                return PartialView("ItemListGetForConfig", ListItem);
            }
            else
            {
                return PartialView(ListItem);
            }

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ItemListCreate(string Id = "")
        {
            ItemListViewModel oItem = new ItemListViewModel();
            List<SelectListItem> ListReceiptItem = new List<SelectListItem>();
            List<SelectListItem> ListMoneyType = new List<SelectListItem>();
            List<SelectListItem> ListItemType = new List<SelectListItem>();
            List<SelectListItem> ListPartner = new List<SelectListItem>();
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListBorrowType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();

            #region ListReceiptItem Kiểu dịch vụ
            //ListReceiptItem.Add(new SelectListItem
            //{
            //    Text = "---Chỉ tiêu thu chi---",
            //    Value = ""
            //});

            foreach (var item in Repository.ListReceiptItem)
            {
                ListReceiptItem.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }
            #endregion

            #region ListMoneyType Kiểu tiền
            ListMoneyType.Add(new SelectListItem
            {
                Text = "---Kiểu tiền---",
                Value = ""
            });

            foreach (var item in Repository.ListMoneyType)
            {
                ListMoneyType.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }
            #endregion

            #region ListItemType Loại chỉ tiêu
            ListItemType.Add(new SelectListItem
            {
                Text = "---Loại chỉ tiêu---",
                Value = ""
            });

            var rs = Helper.Invoke("GET", string.Format("api/Dictionary/GetItemType?id={0}&PageIndex={1}&PageSize={2}", new object[] { 0, 0, Constant.PageSize }), null);
            if (rs != null && rs.Code == "00" && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    ListItemType.Add(new SelectListItem
                    {
                        Text = (dyn.Code ?? "").ToString() + " - " + (dyn.Name ?? "").ToString(),
                        Value = (dyn.Id ?? "").ToString()
                    });
                }
            }
            #endregion

            #region ListPartner Đối tác
            ListPartner.Add(new SelectListItem
            {
                Text = "---Đối tác---",
                Value = "0"
            });
            #endregion

            #region ListBudgetType Loại quỹ
            ListBudgetType.Add(new SelectListItem
            {
                Text = "---Loại quỹ---",
                Value = "0"
            });

            foreach (var item in Repository.ListBudgetType)
            {
                ListBudgetType.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }
            #endregion

            #region ListBorrowType Loại vay trả
            ListBorrowType.Add(new SelectListItem
            {
                Text = "---Loại vay trả---",
                Value = "0"
            });

            foreach (var item in Repository.ListBorrowType)
            {
                ListBorrowType.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }
            #endregion

            #region ListCashFlow Phân loại dòng tiền
            ListCashFlow.Add(new SelectListItem
            {
                Text = "---Dòng tiền---",
                Value = ""
            });

            foreach (var item in Repository.ListCashFlow)
            {
                ListCashFlow.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }
            #endregion

            try
            {
                if (!string.IsNullOrEmpty(Id.Trim()))
                {
                    string Url = string.Format("api/Dictionary/ItemListSearch/{0}?Code={1}&Name={2}&IsReceiptItem={3}&MoneyType={4}&ItemTypeId={5}&IsSummary={6}&ParentId={7}&PartnerId={8}&BudgetTypeId={9}&BorrowTypeId={10}&CashFlowID={11}&PageIndex={12}&PageSize={13}",
                    Id, "", "", "", 0, 0, "", 0, 0, 0, 0, 0, 1, 1);
                    var res = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                    int Total = 0;
                    if (res.Code == "00")
                    {
                        int.TryParse(res.Value.ToString(), out Total);
                        dynamic oValue = res.ListValue;
                        foreach (var item in oValue)
                        {
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
                            oItem.Cfm_Code = (item.Cfm_Code ?? "").ToString();
                        }
                    }
                }
            }
            catch
            {
                oItem = new ItemListViewModel();
            }

            ViewBag.ListReceiptItem = ListReceiptItem;
            ViewBag.ListMoneyType = ListMoneyType;
            ViewBag.ListItemType = ListItemType;
            ViewBag.ListPartner = ListPartner;
            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListBorrowType = ListBorrowType;
            ViewBag.ListCashFlow = ListCashFlow;

            return PartialView(oItem);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult ItemListUpdate(ItemListViewModel oItemList)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                if (ModelState.IsValid)
                {
                    string Url = string.Empty;
                    if (oItemList.Id > 0)
                    {
                        Url = "api/Dictionary/ItemListUpdate";
                    }
                    else
                    {
                        Url = "api/Dictionary/ItemListAdd";
                    }

                    if (oItemList.bIsSummary == true)
                    {
                        oItemList.IsSummary = "Y";
                    }
                    else
                    {
                        oItemList.IsSummary = "N";
                    }
                    oItemList.Code = oItemList.Code.ToUpper();
                    if (oItemList.Formula != null)
                        oItemList.Formula = oItemList.Formula.ToUpper();
                    var rs = Helper.Invoke(Constant.Method.POST.ToString(), Url, oItemList);

                    jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var message = (from state in ModelState.Values
                                   from error in state.Errors
                                   select error.ErrorMessage).Take(1);

                    jResult = Json(new { Code = "-100", Mes = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception )
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu." }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult ItemListDelete(int id)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                string Url = "api/Dictionary/ItemListDelete";
                var request = new
                {
                    Id = id
                };

                var rs = Helper.Invoke(Constant.Method.POST.ToString(), Url, request);

                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu." }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }
        #endregion
    }
}