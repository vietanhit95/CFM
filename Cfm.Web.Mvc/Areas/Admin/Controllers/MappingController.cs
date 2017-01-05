using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Common;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{
    public class MappingController : Controller
    {
        #region Mapping
        // GET: Admin/Mapping
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MappingManager()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MappingGet(string Code = "", string Map_Code = "", int PageIndex = 1, int forConfig = 0, int reportid = 0, string searchContent = "")
        {
            List<MappingViewModel> ListItem = new List<MappingViewModel>();
            int Total = 0;
            int iPageSize = 0;

            iPageSize = Constant.PageSize;
            try
            {
                string Url = string.Format("api/Mapping/MappingSearch/{0}?Code={1}&Map_Code={2}&PageIndex={3}&PageSize={4}&searchContent={5}", 0, Code, Map_Code, PageIndex, iPageSize, searchContent);
                var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                if (rs.Code == "00")
                {
                    int.TryParse(rs.Value.ToString(), out Total);
                    dynamic oValue = rs.ListValue;
                    foreach (var item in oValue)
                    {
                        MappingViewModel oItem = new MappingViewModel();
                        oItem.Id = int.Parse("0" + (item.Id ?? "0").ToString());
                        oItem.Map_Code = (item.Map_Code ?? "").ToString();
                        oItem.Item_Id = int.Parse("0" + (item.Item_Id ?? "0").ToString());
                        oItem.Item_Code = (item.Item_Code ?? "").ToString();
                        oItem.Item_Name = (item.Item_Name ?? "").ToString();
                        oItem.Pa_Code = (item.Pa_Code ?? "").ToString();
                        oItem.Map_Type = int.Parse("0"+(item.Map_Type??"0").ToString());
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
                return PartialView("MappingGetConfig", ListItem);
            }
            else
            {
                return PartialView(ListItem);
            }

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MappingCreate(string Id = "")
        {
            MappingViewModel oItem = new MappingViewModel();
            List<SelectListItem> ListItem = new List<SelectListItem>();
            List<SelectListItem> ListProvider = new List<SelectListItem>();

            #region ListItemType Chỉ tiêu
            ListItem.Add(new SelectListItem
            {
                Text = "---Chỉ tiêu---",
                Value = ""
            });

            var rs = Helper.Invoke("GET", string.Format("api/Dictionary/ItemListSearch/{0}?Code={1}&Name={2}&IsReceiptItem={3}&MoneyType={4}&ItemTypeId={5}&IsSummary={6}&ParentId={7}&PartnerId={8}&BudgetTypeId={9}&BorrowTypeId={10}&CashFlowID={11}&PageIndex={12}&PageSize={13}&searchContent={14}", new object[] { 0, "", "", "", 0, 0, "", 0, 0, 0, 0, 0, 1, 1, "" }), null);
            if (rs != null && rs.Code == "00" && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    ListItem.Add(new SelectListItem
                    {
                        Text = (dyn.Code ?? "").ToString() + " - " + (dyn.Name ?? "").ToString(),
                        Value = (dyn.Id ?? "").ToString()
                    });
                }
            }
            #endregion

            try
            {
                if (!string.IsNullOrEmpty(Id.Trim()))
                {
                    string Url = string.Format("api/Mapping/MappingSearch/{0}?Code={1}&Map_Code={2}&PageIndex={3}&PageSize={4}", Id, "", "", 1, 1);
                    var res = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                    int Total = 0;
                    if (res.Code == "00")
                    {
                        int.TryParse(res.Value.ToString(), out Total);
                        dynamic oValue = res.ListValue;
                        foreach (var item in oValue)
                        {
                            oItem.Id = int.Parse("0" + (item.Id ?? "0").ToString());
                            oItem.Map_Code = (item.Map_Code ?? "").ToString();
                            oItem.Item_Id = int.Parse("0" + (item.Item_Id ?? "0").ToString());
                            oItem.Item_Code = (item.Item_Code ?? "").ToString();
                            oItem.Item_Name = (item.Item_Name ?? "").ToString();
                            oItem.Pa_Code = (item.Pa_Code ?? "").ToString();
                            oItem.Map_Type = int.Parse("0"+(item.Map_Type??"0").ToString());
                        }
                    }
                }
            }
            catch
            {
                oItem = new MappingViewModel();
            }

            ViewBag.ListItem = ListItem;
            ViewBag.ListProvider = ListProvider;

            return PartialView(oItem);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult MappingUpdate(MappingViewModel oMapping)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                string Url = string.Empty;
                if (oMapping.Id > 0)
                {
                    Url = "api/Mapping/MappingUpdate";
                }
                else
                {
                    Url = "api/Mapping/MappingAdd";
                }

                oMapping.Map_Code = oMapping.Map_Code.ToUpper();
                var rs = Helper.Invoke(Constant.Method.POST.ToString(), Url, oMapping);

                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu." }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult MappingDelete(int id)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                string Url = "api/Mapping/MappingDelete";
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

        #region But toan ket chuyen

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TransferEntryManager()
        {
            List<SelectListItem> ListReportType = new List<SelectListItem>();
            List<SelectListItem> ListTransferType = new List<SelectListItem>();

            #region ReportType
            ListReportType.Add(new SelectListItem
            {
                Text = "---Loại kết chuyển---",
                Value = "0"
            });

            foreach (var item in Repository.ListReportType)
            {

                ListReportType.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }

            #endregion

            #region ListTransferType
            ListTransferType.Add(new SelectListItem
            {
                Text = "---Loại báo cáo---",
                Value = "0"
            });
            foreach (var item in Repository.ListTransferType)
            {
                ListTransferType.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }

            #endregion
            ViewBag.ListReportType = ListReportType;
            ViewBag.ListTransferType = ListTransferType;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TransferEntryGet(string reportType = "", string transferType = "", int PageIndex = 1, string searchContent = "")
        {
            List<TransferEntryViewModel> ListTransfer = new List<TransferEntryViewModel>();
            int Total = 0;
            int iPageSize = 0;

            iPageSize = Constant.PageSize;
            try
            {
                string Url = string.Format("api/Mapping/TransferEntrySearch/{0}?reportType={1}&transferType={2}&PageIndex={3}&PageSize={4}&searchContent={5}", 0, reportType, transferType, PageIndex, iPageSize, searchContent);
                var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                if (rs.Code == "00")
                {
                    int.TryParse(rs.Value.ToString(), out Total);
                    dynamic oValue = rs.ListValue;
                    foreach (var item in oValue)
                    {
                        TransferEntryViewModel oItem = new TransferEntryViewModel();
                        oItem.Id = int.Parse("0" + (item.Id ?? "0").ToString());
                        oItem.ReportType = (item.ReportType ?? "").ToString();
                        oItem.SourceItemId = int.Parse("0" + (item.SourceItemId ?? "0").ToString());
                        oItem.SourceItemCode = (item.SourceItemCode ?? "").ToString();
                        oItem.SourceItemName = (item.SourceItemName ?? "").ToString();
                        oItem.TargetItemId = int.Parse("0" + (item.TargetItemId ?? "0").ToString());
                        oItem.TargetItemCode = (item.TargetItemCode ?? "").ToString();
                        oItem.TargetItemName = (item.TargetItemName ?? "").ToString();
                        oItem.TransferType = int.Parse("0" + (item.TransferType ?? "0").ToString());
                        oItem.TransferTypeName = Repository.ListTransferType[oItem.TransferType].ToString();
                        ListTransfer.Add(oItem);
                    }
                }
                else
                {
                    ListTransfer = null;
                }
            }
            catch
            {
                ListTransfer = null;
            }

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = iPageSize;
            ViewBag.ToTal = Total;

            return PartialView(ListTransfer);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TransferEntryCreate(string Id = "")
        {
            TransferEntryViewModel oItem = new TransferEntryViewModel();
            List<SelectListItem> ListReportType = new List<SelectListItem>();
            List<SelectListItem> ListTransferType = new List<SelectListItem>();

            #region ReportType

            foreach (var item in Repository.ListReportType)
            {
                ListReportType.Add(new SelectListItem
                {
                    Text = item.Value.ToString(),
                    Value = item.Key.ToString()
                });
            }

            #endregion

            #region ListTransferType

            foreach (var item in Repository.ListTransferType)
            {
                ListTransferType.Add(new SelectListItem
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
                    string Url = string.Format("api/Mapping/TransferEntrySearch/{0}?reportType={1}&transferType={2}&PageIndex={3}&PageSize={4}&searchContent={5}", Id, "", 0, 1, 1, "");
                    var res = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

                    int Total = 0;
                    if (res.Code == "00")
                    {
                        int.TryParse(res.Value.ToString(), out Total);
                        dynamic oValue = res.ListValue;
                        foreach (var item in oValue)
                        {

                            oItem.Id = int.Parse("0" + (item.Id ?? "0").ToString());
                            oItem.ReportType = (item.ReportType ?? "").ToString();
                            oItem.SourceItemId = int.Parse("0" + (item.SourceItemId ?? "0").ToString());
                            oItem.SourceItemCode = (item.SourceItemCode ?? "").ToString();
                            oItem.SourceItemName = (item.SourceItemName ?? "").ToString();
                            oItem.TargetItemId = int.Parse("0" + (item.TargetItemId ?? "0").ToString());
                            oItem.TargetItemCode = (item.TargetItemCode ?? "").ToString();
                            oItem.TargetItemName = (item.TargetItemName ?? "").ToString();
                            oItem.TransferType = int.Parse("0" + (item.TransferType ?? "0").ToString());

                        }
                    }
                }
            }
            catch
            {
                oItem = new TransferEntryViewModel();
            }

            ViewBag.ListReportType = ListReportType;
            ViewBag.ListTransferType = ListTransferType;

            return PartialView(oItem);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult TransferEntryUpdate(TransferEntryViewModel oTransfer)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                string Url = string.Empty;
                if (oTransfer.Id > 0)
                {
                    Url = "api/Mapping/TransferEntryUpdate";
                }
                else
                {
                    Url = "api/Mapping/TransferEntryAdd";
                }

                var rs = Helper.Invoke(Constant.Method.POST.ToString(), Url, oTransfer);

                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu." }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult TransferEntryDelete(int id)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                string Url = "api/Mapping/TransferEntryDelete";
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

        public ActionResult ItemSelect(string reportType = "", string searchContent = "",string ItemType ="1",int TransferType=1)
        {
            //List<ItemListViewModel> ListItem = new List<ItemListViewModel>();
            //int Total = 0;
            //int iPageSize = 0;

            //iPageSize = Constant.PageSize;
            //try
            //{
            //    string Url = string.Format("api/Mapping/ItemListSearch?reportType={0}&searchContent={1}&PageIndex={2}&PageSize={3}",
            //        reportType, searchContent.ToUpper(), 1, iPageSize);
            //    var rs = Helper.Invoke(Constant.Method.GET.ToString(), Url, null);

            //    if (rs.Code == "00")
            //    {
            //        int.TryParse(rs.Value.ToString(), out Total);
            //        dynamic oValue = rs.ListValue;
            //        string sCode = "";
            //        foreach (var item in oValue)
            //        {
            //            ItemListViewModel oItem = new ItemListViewModel();
            //            oItem.Id = int.Parse("0" + (item.Id ?? "0").ToString());
            //            sCode = (item.Code ?? "").ToString();
            //            if (sCode.Length > 18)
            //                oItem.CodeDisplay = sCode.Substring(0, 15) + "...";
            //            else
            //                oItem.CodeDisplay = sCode;
            //            oItem.Code = sCode;
            //            oItem.Name = (item.Name ?? "").ToString();
            //            oItem.IsReceiptItem = (item.IsReceiptItem ?? "").ToString();
            //            oItem.MoneyType = int.Parse("0" + (item.MoneyType ?? "0").ToString());
            //            oItem.ItemTypeId = int.Parse("0" + (item.ItemTypeId ?? "0").ToString());
            //            oItem.IsSummary = (item.IsSummary ?? "N").ToString();
            //            oItem.bIsSummary = (oItem.IsSummary == "" || oItem.IsSummary == "N" ? false : true);
            //            oItem.Formula = (item.Formula ?? "").ToString();
            //            oItem.ParentId = int.Parse("0" + (item.ParentId ?? "0").ToString());
            //            oItem.PartnerId = int.Parse("0" + (item.PartnerId ?? "0").ToString());
            //            oItem.BudgetTypeId = int.Parse("0" + (item.BudgetTypeId ?? "0").ToString());
            //            oItem.BorrowTypeId = int.Parse("0" + (item.BorrowTypeId ?? "0").ToString());
            //            oItem.CashFlowId = int.Parse("0" + (item.CashFlowId ?? "0").ToString());
            //            oItem.Description = (item.Description ?? "").ToString();
            //            oItem.Cfm_Code = (item.Cfm_Code ?? "").ToString();
            //            ListItem.Add(oItem);
            //        }
            //    }
            //    else
            //    {
            //        ListItem = null;
            //    }
            //}
            //catch
            //{
            //    ListItem = null;
            //}

            ViewBag.Total = 0;
            ViewBag.PageIndex = 1;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.reportType = reportType;
            ViewBag.ItemType = ItemType;
            ViewBag.TransferType = TransferType;
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ItemListGet(string reportType = "",string itemType ="1",int transferType =1, int PageIndex = 1, string searchContent = "")
        {
            List<ItemListViewModel> ListItem = new List<ItemListViewModel>();
            int Total = 0;
            int iPageSize = 0;

            iPageSize = Constant.PageSize;
            try
            {
                string Url = string.Format("api/Mapping/ItemListSearch?reportType={0}&itemType={1}&transferType={2}&searchContent={3}&PageIndex={4}&PageSize={5}",
                    reportType,itemType, transferType, searchContent.ToUpper(), PageIndex, iPageSize);
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

            return PartialView(ListItem);

        }

        #endregion

    }

}