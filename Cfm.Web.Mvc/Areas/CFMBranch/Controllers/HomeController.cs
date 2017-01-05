using Cfm.Web.Mvc.Areas.Admin.Controllers;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.CFMBranch.Controllers
{
    public class HomeController : BaseController
    {
        // GET: CFMBranch/Home
        public ActionResult Index()
        {
            return View();
        }
        #region DashBoard_Notifi
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DashBoardNotifiGet(NotifiCationViewModels notifi, int id = 0, int PageIndex = 1, string status = "", int refType = 0, string oderby = "", string date = "", string type = "")
        {
            EmployeeViewModel Empl = new EmployeeViewModel();
            Empl = UserCurrent();
            int Total = 0;
            string fromDate = "";
            string toDate = "";
            string[] arrdate = date.Split('-');
            if (!string.IsNullOrEmpty(date))
            {
                fromDate = date.Substring(0, 10);
                toDate = arrdate[1];
            }
            List<NotifiCationViewModels> lstNotifiCation = new List<NotifiCationViewModels>();
            NotifiCationViewModels Noti = new NotifiCationViewModels();
            var rs = Helper.Invoke("GET", string.Format("api/Notify/GetFundNotify?Id={0}&SendPoId={1}&ReceivePoId={2}&Status={3}&Type={4}&FromDate={5}&ToDate={6}&OrderBy={7}&PageIndex={8}&PageSize={9}", new object[] {
                Noti.Id,
                Noti.SendPoId,
                Empl.POID,
                status,
                type,
                fromDate,
                toDate,
                oderby,
                PageIndex,
                Constant.PageSize
            }), null);
            if (rs != null && rs.ListValue != null)
            {

                foreach (dynamic NotifiCation in rs.ListValue)
                {
                    string sTatus = "";
                    string daTe = NotifiCation.CreateDate;
                    if (NotifiCation.Status == "Y")
                        sTatus = "Đã xử lý";
                    else if (NotifiCation.Status == "N")
                        sTatus = "Chưa xử lý";
                    var Notifi = new NotifiCationViewModels();
                    Notifi.Id = NotifiCation.Id;
                    Notifi.CreateDate = daTe.Substring(0, 10);
                    Notifi.Description = NotifiCation.Description;
                    Notifi.SendPoId = NotifiCation.SendPoId;
                    Notifi.SendPoName = NotifiCation.SendPoName;
                    Notifi.ReceivePoId = NotifiCation.ReceivePoId;
                    Notifi.SendPoCode = NotifiCation.SendPoCode;
                    Notifi.ReceivePoName = NotifiCation.ReceivePoName;
                    Notifi.ReceivePoCode = NotifiCation.ReceivePoCode;
                    Notifi.PassLimits = NotifiCation.PassLimits;
                    Notifi.Status = sTatus;
                    Notifi.Type = NotifiCation.Type;
                    Notifi.IsReaded = NotifiCation.IsReaded;
                    Notifi.RefId = NotifiCation.RefId;
                    if (!lstNotifiCation.Contains(Notifi))
                        lstNotifiCation.Add(Notifi);
                }
                if (rs.Value != null)
                {
                    int.TryParse(rs.Value.ToString(), out Total);
                }

            }
            ViewBag.RefType = refType;
            ViewBag.Total = Total;
            ViewBag.PageIndex = PageIndex;
            ViewBag.Model = lstNotifiCation.ToList();
            ViewBag.PageSize = Constant.PageSize;
            return PartialView(lstNotifiCation);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateNotifi(NotifiCationViewModels Notifi, int Id = 0, string decripstion = "", string decriptionres = "", string type = "", string status = "")
        {
            EmployeeViewModel Empl = new EmployeeViewModel();
            Empl = UserCurrent();
            Notifi.ReceivePoId = Empl.POID;
            JsonResult jResul = new JsonResult();
            try
            {
                if (ModelState.IsValid)
                {
                    string Url = string.Empty;
                    if (Notifi.Id > 0 || Notifi.DescriptionRes == "")
                    {
                        Url = "api/Notify/ResponseFundNotify";
                    }
                    var rs = Helper.Invoke(Constant.Method.POST.ToString(), Url, Notifi);
                    jResul = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var message = (from state in ModelState.Values
                                   from error in state.Errors
                                   select error.ErrorMessage).Take(1);

                    jResul = Json(new { Code = "-100", Mes = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                jResul = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu." }, JsonRequestBehavior.AllowGet);
            }

            return jResul;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DashboardNotifiDetails(int Id, int ref_id)
        {
            List<NotifiCationViewModels> lstNotifiCation = new List<NotifiCationViewModels>();
            NotifiCationViewModels Noti = new NotifiCationViewModels();
            var rs = Helper.Invoke("GET", string.Format("api/Notify/GetFundNotify?Id={0}&SendPoId={1}&ReceivePoId={2}&Status={3}&Type={4}&FromDate={5}&ToDate={6}&OrderBy={7}&PageIndex={8}&PageSize={9}", new object[] {
                Id,
                0,
                0,
                "",
                "",
                "",
                "",
                "",
                1,
                1
            }), null);
            {
                if (rs != null && rs.ListValue != null)
                {
                    foreach (dynamic item in rs.ListValue)
                    {
                        string date = item.CreateDate;
                        Noti.Id = Id;
                        Noti.SendPoId = item.SendPoId;
                        Noti.ReceivePoId = item.ReceivePoId;
                        Noti.Description = item.Description;
                        Noti.DescriptionRes = item.DescriptionRes;
                        Noti.Status = item.Status;
                        Noti.Type = item.Type;
                        Noti.CreateDate = date.Substring(0, 10);
                        Noti.IsReaded = item.IsReaded;
                        Noti.RefId = item.RefId;
                    }
                }
            }


            List<SelectListItem> ListStatus = new List<SelectListItem>();
            ListStatus.Add(new SelectListItem
            {
                Text = "Chưa xử lý",
                Value = "N"
            });
            ListStatus.Add(new SelectListItem
            {
                Text = "Đã xử lý",
                Value = "Y"
            });
            List<SelectListItem> ListType = new List<SelectListItem>();
            ListType.Add(new SelectListItem
            {
                Text = "Vượt hạn mức lưu quỹ",
                Value = "1"
            });
            ViewBag.ListStatus = ListStatus;
            ViewBag.ListType = ListType;
            return PartialView(Noti);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DashBoard_AllocateFundDetail(int Id, int ref_id, string status, int refType)
        {
            AccountingEntryViewModel Accounting = new AccountingEntryViewModel();
            List<SelectListItem> ListPo = new List<SelectListItem>();
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
            List<SelectListItem> ListBorrowMethod = new List<SelectListItem>();
            EmployeeViewModel oEmployee = new EmployeeViewModel();
            oEmployee = UserCurrent();
            if (refType == (int)Constant.AccountingRefType.FundsSend)
            {
                var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchAccountingEntry?id={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&refTypeId={6}&PageIndex={7}&PageSize={8}", new object[] { 907, 9, "", "", 0, 0, refType, 1, 1 }), null);
                if (rs != null && rs.ListValue != null)
                {
                    foreach (dynamic dyn in rs.ListValue)
                    {
                        Accounting.Id = int.Parse("0" + dyn.Id.ToString());
                        Accounting.TransNumber = dyn.TransNumber;
                        Accounting.AmndEmpId = int.Parse("0" + dyn.AmndEmpId.ToString());
                        Accounting.PoId = int.Parse("0" + dyn.PoId.ToString());
                        Accounting.PoCode = dyn.PoCode;


                        if (dyn.IsLvp != null && Convert.ToBoolean(dyn.IsLvp.ToString()))
                        {
                            Accounting.SendPoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                            Accounting.SendPoCode = dyn.ReceivePoCode;
                            Accounting.SendPoName = "Ngân hàng Liên Việt";
                        }
                        else
                        {
                            Accounting.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                            Accounting.ReceivePoCode = dyn.ReceivePoCode;
                            Accounting.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;
                        }

                        Accounting.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                        Accounting.ReceivePoCode = dyn.ReceivePoCode;
                        Accounting.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;

                        Accounting.RefType = int.Parse("0" + dyn.RefType.ToString());
                        Accounting.TransDate = dyn.TransDate;
                        Accounting.RefTransNumber = dyn.RefTransNumber;
                        Accounting.AmountVnd = decimal.Parse(dyn.AmountVnd.ToString());
                        Accounting.AmountUsd = decimal.Parse(dyn.AmountUsd.ToString());
                        Accounting.BudgetTypeId = int.Parse(dyn.BudgetTypeId.ToString());
                        Accounting.CashFllowId = int.Parse(dyn.CashFllowId.ToString());
                        Accounting.Description = dyn.Description;
                        Accounting.CurrencyType = dyn.CurrencyType;
                        if (dyn.IsLvp != null && Convert.ToBoolean(dyn.IsLvp.ToString()))
                            Accounting.IsLvp = true;
                        else
                            Accounting.IsLvp = false;

                        Accounting.OrdinalNumberString = dyn.OrdinalNumber.ToString();
                        if (dyn.CurrencyType.ToString() == "VND")
                            Accounting.Amount = dyn.AmountVnd.ToString();
                        else
                            Accounting.Amount = dyn.AmountUsd.ToString();
                        Accounting.Description = dyn.Description;
                        Accounting.CurrencyType = dyn.CurrencyType;
                        Accounting.CurrencyTypeUnit = dyn.CurrencyTypeUnit;
                        if (dyn.CurrencyTypeUnit.ToString() == "VND")
                            Accounting.AmountUnitString = dyn.AmountUnitVnd.ToString();
                        else
                            Accounting.AmountUnitString = dyn.AmountUnitUsd.ToString();
                        Accounting.CurrencyTypeSaving = dyn.CurrencyTypeSaving;
                        if (dyn.CurrencyTypeSaving.ToString() == "VND")
                            Accounting.AmountSavingString = dyn.AmountSavingVnd.ToString();
                        else
                            Accounting.AmountSavingString = dyn.AmountSavingUsd.ToString();
                        Accounting.CurrencyTypeBS = dyn.CurrencyTypeBS;
                        if (dyn.CurrencyTypeBS.ToString() == "VND")
                            Accounting.AmountBSString = dyn.AmountBSVnd.ToString();
                        else
                            Accounting.AmountBSString = dyn.AmountBSUsd.ToString();
                        Accounting.BorrowMethod = dyn.BorrowMethod;
                        Accounting.BorrowMethodUnit = dyn.BorrowMethodUnit;
                        Accounting.BorrowMethodSaving = dyn.BorrowMethodSaving;
                        Accounting.BorrowMethodBS = dyn.BorrowMethodBS;
                        break;
                    }
                }

            }
            #region ListBudgetType
            ListBudgetType.Add(new SelectListItem
            {
                Text = "---Tất cả---",
                Value = "0"
            });

            foreach (var item in Repository.ListBudgetType)
            {
                ListBudgetType.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString()
                });
            }
            #endregion

            #region ListCashFlow
            foreach (var item in Repository.ListCashFlow)
            {
                ListCashFlow.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString()
                });
            }
            #endregion

            #region ListCurrencyType
            ListCurrencyType.Add(new SelectListItem
            {
                Text = Constant.CurrencyType.VND.ToString(),
                Value = Constant.CurrencyType.VND.ToString()
            });

            ListCurrencyType.Add(new SelectListItem
            {
                Text = Constant.CurrencyType.USD.ToString(),
                Value = Constant.CurrencyType.USD.ToString()
            });
            #endregion

            #region ListBorrowMethod
            ListBorrowMethod.Add(new SelectListItem
            {
                Text = Constant.BorrowMethod.TM.ToString(),
                Value = Constant.BorrowMethod.TM.ToString()
            });

            ListBorrowMethod.Add(new SelectListItem
            {
                Text = Constant.BorrowMethod.CK.ToString(),
                Value = Constant.BorrowMethod.CK.ToString()
            });
            #endregion

            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListCashFlow = ListCashFlow;
            ViewBag.ListCurrencyType = ListCurrencyType;
            ViewBag.ListBorrowMethod = ListBorrowMethod;
            ViewBag.Status = status;
            ViewBag.Id = Id;
            return PartialView(Accounting);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult IsReaded(int Id, string status)
        {
            JsonResult jResult = new JsonResult();
            if (status == "N")
            {
                EmployeeViewModel Empl = new EmployeeViewModel();
                Empl = UserCurrent();
                var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/System/MaskAsReadMessage?id={0}&poId={1}", new object[] { Id, Empl.POID }), null);
                string Code = rs.Code;
                string Mes = rs.Message;
                if (rs.Code == "00")
                {
                    jResult = Json(new { Code = Code, Mes = Mes }, JsonRequestBehavior.AllowGet);
                }
            }
            return jResult;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult FunnotifiGet(FunNotifiViewModels funNoti, int PageIndex = 1, string status = "", string date = "")
        {
            EmployeeViewModel on = new EmployeeViewModel();
            List<FunNotifiViewModels> lstFun = new List<FunNotifiViewModels>();
            string fromDate = "";
            string toDate = "";
            on = UserCurrent();
            if (date == "")
            {
                fromDate = DateTime.Now.ToString("dd/MM/yyyy");
                toDate = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                fromDate = date.Split('-')[0].Trim();
                toDate = date.Split('-')[1].Trim();
            }
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04CDManage?poId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}", new object[] { on.POID, fromDate, toDate, status, PageIndex, Constant.PageSize }), null);
            {
                if (rs != null && rs.ListValue != null)
                {
                    foreach (dynamic Fun in rs.ListValue)
                    {
                        //string Status = "";
                        //if (Fun.ReportStatus == "C")
                        //{
                        //    Status = "Chưa lập báo cáo";
                        //}
                        //else if (Fun.ReportStatus == "L")
                        //{
                        //    Status = "Chưa xác nhận";
                        //}
                        //else if (Fun.ReportStatus == "A")
                        //{
                        //    Status = "Đã xác nhận";
                        //}
                        //else
                        //{
                        //    Status = "Trạng thái không xác định";
                        //}
                        var FunNoti = new FunNotifiViewModels();
                        FunNoti.reportStatus = Fun.ReportStatus;
                        FunNoti.CreateDate = Fun.ReportDate;
                        if (!lstFun.Contains(FunNoti))
                            lstFun.Add(FunNoti);
                    }
                }
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            return PartialView(lstFun);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult InfoNotifiGet()
        {
            List<InfomationNotifiViewModels> lstInfo = new List<InfomationNotifiViewModels>();
            InfomationNotifiViewModels InfoModel = new InfomationNotifiViewModels();
            EmployeeViewModel Empl = new EmployeeViewModel();
            Empl = UserCurrent();
            var rs = Helper.Invoke("GET", string.Format("api/System/GetPOFundInfo?PoId={0}&reportDate={1}", Empl.POID, DateTime.Now.ToString("dd/MM/yyyy")), null);
            if (rs.Value != null)
            {
                dynamic obj = rs.Value;
                var Info = new InfomationNotifiViewModels();
                Info.OpeningAmount = Convert.ToDecimal(obj.OpeningAmount);
                Info.ReceipAmount = Convert.ToDecimal(obj.ReceiptAmount);
                Info.PaymentAmount = Convert.ToDecimal(obj.PaymentAmount);
                Info.ClosingAmount = Convert.ToDecimal(obj.ClosingAmount);
                if (!lstInfo.Contains(Info))
                    lstInfo.Add(Info);

            }
            return PartialView(lstInfo);
        }
        #endregion
    }
}