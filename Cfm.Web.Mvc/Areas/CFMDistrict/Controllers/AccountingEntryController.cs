using Cfm.Web.Mvc.Areas.Admin.Controllers;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel;
using Cfm.Web.Mvc.Areas.CFMDistrict.Models.ViewModels;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.CFMDistrict.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District })]
    public class AccountingEntryController : BaseController
    {
        // GET: CFMDistrict/AccountingEntry
        public ActionResult Index()
        {
            return View();
        }

        #region Tiếp nộp quỹ AllocateFund
        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.AccountingEntry_District_AllocateFundManage, (int)Constant.Function.FuntionBranch_AllocateFundManage })]
        public ActionResult AllocateFundManage()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AllocateFundGet(int id = 0, int poId = 0, string dateRange = "", int budgetTypeId = 0, int cashFllowId = 0, int refType = 0, int PageIndex = 1)
        {
            string fromDate = "";
            string toDate = "";
            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            else
            {
                fromDate = Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            List<AccountingEntryViewModel> ListAccountingEntry = new List<AccountingEntryViewModel>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchAccountingEntry?id={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&refTypeId={6}&PageIndex={7}&PageSize={8}", new object[] { 0, oEmployee.POID, fromDate, toDate, budgetTypeId, cashFllowId, refType, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    var oEntry = new AccountingEntryViewModel();

                    oEntry.Id = int.Parse("0" + dyn.Id.ToString());
                    oEntry.TransNumber = dyn.TransNumber;
                    oEntry.AmndEmpId = int.Parse("0" + dyn.AmndEmpId.ToString());
                    oEntry.PoId = int.Parse("0" + dyn.PoId.ToString());
                    oEntry.PoCode = dyn.PoCode;

                    oEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                    oEntry.SendPoCode = dyn.SendPoCode;
                    oEntry.SendPoName = dyn.SendPoCode + " - " + dyn.SendPoName;

                    oEntry.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                    oEntry.ReceivePoCode = dyn.ReceivePoCode;
                    oEntry.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;

                    oEntry.RefType = int.Parse("0" + dyn.RefType.ToString());
                    oEntry.TransDate = dyn.TransDate;
                    oEntry.RefTransNumber = dyn.RefTransNumber;
                    oEntry.AmountVnd = Convert.ToDecimal(dyn.AmountVnd);
                    oEntry.AmountUsd = Convert.ToDecimal(dyn.AmountUsd);
                    oEntry.BudgetTypeId = int.Parse("0" + dyn.BudgetTypeId.ToString());
                    oEntry.CashFllowId = int.Parse("0" + dyn.CashFllowId.ToString());
                    oEntry.Description = dyn.Description;
                    oEntry.CurrencyType = dyn.CurrencyType;
                    oEntry.AmountUnitVnd = Convert.ToDecimal(dyn.AmountUnitVnd);
                    oEntry.AmountUnitUsd = Convert.ToDecimal(dyn.AmountUnitUsd);
                    oEntry.CurrencyTypeUnit = dyn.CurrencyTypeUnit;
                    oEntry.AmountSavingVnd = Convert.ToDecimal(dyn.AmountSavingVnd);
                    oEntry.AmountSavingUsd = Convert.ToDecimal(dyn.AmountSavingUsd);
                    oEntry.CurrencyTypeSaving = dyn.CurrencyTypeSaving;
                    oEntry.AmountBSVnd = Convert.ToDecimal(dyn.AmountBSVnd);
                    oEntry.AmountBSUsd = Convert.ToDecimal(dyn.AmountBSUsd);
                    oEntry.CurrencyTypeBS = dyn.CurrencyTypeBS;
                    oEntry.CreatedPoCode = dyn.CreatedPoCode;
                    if (!ListAccountingEntry.Contains(oEntry))
                        ListAccountingEntry.Add(oEntry);
                }

            }

            ViewBag.RefType = refType;

            return PartialView(ListAccountingEntry);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AllocateFundDetail(int Id, int refType)
        {
            AccountingEntryViewModel oAccountingEntry = new AccountingEntryViewModel();
            List<SelectListItem> ListPo = new List<SelectListItem>();
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
            List<SelectListItem> ListBorrowMethod = new List<SelectListItem>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];

            var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchAccountingEntry?id={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&refTypeId={6}&PageIndex={7}&PageSize={8}", new object[] { Id, oEmployee.POID, "", "", 0, 0, refType, 1, 1 }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    oAccountingEntry.Id = int.Parse("0" + dyn.Id.ToString());
                    oAccountingEntry.TransNumber = dyn.TransNumber;
                    oAccountingEntry.AmndEmpId = int.Parse("0" + dyn.AmndEmpId.ToString());
                    oAccountingEntry.PoId = int.Parse("0" + dyn.PoId.ToString());
                    oAccountingEntry.PoCode = dyn.PoCode;

                    if (refType == (int)Constant.AccountingRefType.FundsReceive)
                    {
                        if (dyn.IsLvp != null && Convert.ToBoolean(dyn.IsLvp.ToString()))
                        {
                            oAccountingEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                            oAccountingEntry.SendPoCode = dyn.SendPoCode;
                            oAccountingEntry.SendPoName = "Ngân hàng Liên Việt";
                            oAccountingEntry.PoIdTemp1 = int.Parse("0" + dyn.SendPoId.ToString());
                        }
                        else
                        {
                            oAccountingEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                            oAccountingEntry.SendPoCode = dyn.SendPoCode;
                            oAccountingEntry.SendPoName = dyn.SendPoCode + " - " + dyn.SendPoName;
                            oAccountingEntry.PoIdTemp1 = int.Parse("0" + dyn.SendPoId.ToString());
                        }
                    }
                    else
                    {
                        oAccountingEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                        oAccountingEntry.SendPoCode = dyn.SendPoCode;
                        oAccountingEntry.SendPoName = dyn.SendPoCode + " - " + dyn.SendPoName;
                        oAccountingEntry.PoIdTemp1 = int.Parse("0" + dyn.SendPoId.ToString());
                    }

                    if (refType == (int)Constant.AccountingRefType.FundsSend)
                    {
                        if (dyn.IsLvp != null && Convert.ToBoolean(dyn.IsLvp.ToString()))
                        {
                            oAccountingEntry.SendPoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                            oAccountingEntry.SendPoCode = dyn.ReceivePoCode;
                            oAccountingEntry.SendPoName = "Ngân hàng Liên Việt";
                            oAccountingEntry.PoIdTemp1 = int.Parse("0" + dyn.ReceivePoId.ToString());
                        }
                        else
                        {
                            oAccountingEntry.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                            oAccountingEntry.ReceivePoCode = dyn.ReceivePoCode;
                            oAccountingEntry.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;
                            oAccountingEntry.PoIdTemp1 = int.Parse("0" + dyn.ReceivePoId.ToString());
                        }
                    }
                    else
                    {
                        oAccountingEntry.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                        oAccountingEntry.ReceivePoCode = dyn.ReceivePoCode;
                        oAccountingEntry.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;
                        oAccountingEntry.PoIdTemp1 = int.Parse("0" + dyn.ReceivePoId.ToString());
                    }

                    oAccountingEntry.RefType = int.Parse("0" + dyn.RefType.ToString());
                    oAccountingEntry.TransDate = dyn.TransDate;
                    oAccountingEntry.RefTransNumber = dyn.RefTransNumber;
                    oAccountingEntry.AmountVnd = decimal.Parse(dyn.AmountVnd.ToString());
                    oAccountingEntry.AmountUsd = decimal.Parse(dyn.AmountUsd.ToString());
                    oAccountingEntry.BudgetTypeId = int.Parse(dyn.BudgetTypeId.ToString());
                    oAccountingEntry.CashFllowId = int.Parse(dyn.CashFllowId.ToString());
                    oAccountingEntry.Description = dyn.Description;
                    oAccountingEntry.CurrencyType = dyn.CurrencyType;
                    if (dyn.IsLvp != null && Convert.ToBoolean(dyn.IsLvp.ToString()))
                        oAccountingEntry.IsLvp = true;
                    else
                        oAccountingEntry.IsLvp = false;

                    oAccountingEntry.OrdinalNumberString = dyn.OrdinalNumber.ToString();
                    if (dyn.CurrencyType.ToString() == "VND")
                        oAccountingEntry.Amount = dyn.AmountVnd.ToString();
                    else
                        oAccountingEntry.Amount = dyn.AmountUsd.ToString();
                    oAccountingEntry.Description = dyn.Description;
                    oAccountingEntry.CurrencyType = dyn.CurrencyType;
                    oAccountingEntry.CurrencyTypeUnit = dyn.CurrencyTypeUnit;
                    if (dyn.CurrencyTypeUnit.ToString() == "VND")
                        oAccountingEntry.AmountUnitString = dyn.AmountUnitVnd.ToString();
                    else
                        oAccountingEntry.AmountUnitString = dyn.AmountUnitUsd.ToString();
                    oAccountingEntry.CurrencyTypeSaving = dyn.CurrencyTypeSaving;
                    if (dyn.CurrencyTypeSaving.ToString() == "VND")
                        oAccountingEntry.AmountSavingString = dyn.AmountSavingVnd.ToString();
                    else
                        oAccountingEntry.AmountSavingString = dyn.AmountSavingUsd.ToString();
                    oAccountingEntry.CurrencyTypeBS = dyn.CurrencyTypeBS;
                    if (dyn.CurrencyTypeBS.ToString() == "VND")
                        oAccountingEntry.AmountBSString = dyn.AmountBSVnd.ToString();
                    else
                        oAccountingEntry.AmountBSString = dyn.AmountBSUsd.ToString();
                    oAccountingEntry.BorrowMethod = dyn.BorrowMethod;
                    oAccountingEntry.BorrowMethodUnit = dyn.BorrowMethodUnit;
                    oAccountingEntry.BorrowMethodSaving = dyn.BorrowMethodSaving;
                    oAccountingEntry.BorrowMethodBS = dyn.BorrowMethodBS;
                    break;
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
            return PartialView(oAccountingEntry);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AllocateFundCreate(int RefType)
        {
            AccountingEntryViewModel oAccounting = new AccountingEntryViewModel();

            PostOfficeViewModel oPo = (PostOfficeViewModel)Session[Constant.PO_SESSION];

            var rs = Helper.Invoke(Common.Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchOrdinalNumber?PoId={0}&RefType={1}&Month={2}", oPo.ID, RefType, DateTime.Now.Month), null);

            if (rs.Code == "00")
            {
                oAccounting.OrdinalNumberString = rs.Value.ToString();
            }
            else
            {
                oAccounting.OrdinalNumberString = "1";
            }

            oAccounting.TransDate = Session[Constant.TIMEWORK_SESSION].ToString();
            oAccounting.Amount = "0";
            oAccounting.AmountUnitString = "0";
            oAccounting.AmountSavingString = "0";
            oAccounting.AmountBSString = "0";
            oAccounting.RefType = RefType;

            List<SelectListItem> ListPo = new List<SelectListItem>();
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();

            #region ListPo
            if (oPo.POLevel != (int)Constant.POLevel.Center)
            {
                var rsPo = Helper.Invoke("GET", string.Format("api/PostOffice/GetListPOByParentID?parentID={0}", new object[] { oPo.ID }), null);

                //List<PostOfficeViewModel> ListPo = new List<PostOfficeViewModel>();
                if (rsPo != null && rsPo.Code == "00" && rsPo.ListValue != null)
                {
                    foreach (dynamic dyn in rsPo.ListValue)
                    {
                        ListPo.Add(new SelectListItem
                        {
                            Text = dyn.Code + " - " + dyn.Name,
                            Value = dyn.Id
                        });
                    }
                }
                ViewBag.ListPo = ListPo;
            }
            #endregion

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


            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListCashFlow = ListCashFlow;
            ViewBag.ListCurrencyType = ListCurrencyType;
            return PartialView(oAccounting);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult GetOrdinalNumber(int poId = 0, int refType = 0)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                var rs = Helper.Invoke(Common.Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchOrdinalNumber?PoId={0}&RefType={1}&Month={2}", poId, refType, DateTime.Now.Month), null);

                int Number = 1;

                if (rs.Code == "00")
                {
                    Number = int.Parse("0" + rs.Value.ToString());
                }
                else
                {
                    Number = 1;
                }

                jResult = Json(new { Value = Number }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Value = 1 }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult AllocateFundUpdate(AccountingEntryViewModel oAccounting)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                if (oAccounting.PoIdTemp1 > 0)
                {
                    EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                    int sendPoId = 0;
                    int receivePoId = 0;
                    if (oAccounting.RefType == (int)Constant.AccountingRefType.FundsSend || oAccounting.RefType == (int)Constant.AccountingRefType.SendTGNH)
                    {
                        sendPoId = oEmployee.POID;
                        receivePoId = oAccounting.PoIdTemp1;
                    }
                    else
                    {
                        sendPoId = oAccounting.PoIdTemp1;
                        receivePoId = oEmployee.POID;
                    }
                    //sendPoId = oEmployee.POID;
                    //receivePoId = oAccounting.PoIdTemp1;
                    var _Accounting = new
                    {
                        Id = oAccounting.Id,
                        TransNumber = oAccounting.TransNumber,
                        AmndEmpId = oEmployee.ID,
                        PoId = oEmployee.POID,
                        CreatedPoId = oEmployee.POID,
                        SendPoId = sendPoId,
                        ReceivePoId = receivePoId,
                        RefType = oAccounting.RefType,
                        TransDate = new DateTime(int.Parse(oAccounting.TransDate.Split('/')[2]), int.Parse(oAccounting.TransDate.Split('/')[1]), int.Parse(oAccounting.TransDate.Split('/')[0])).ToString("dd-MM-yyyy"),
                        RefTransNumber = oAccounting.RefTransNumber,
                        AmountVnd = oAccounting.CurrencyType == Constant.CurrencyType.VND.ToString() ? oAccounting.Amount.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        AmountUsd = oAccounting.CurrencyType == Constant.CurrencyType.USD.ToString() ? oAccounting.Amount.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        BudgetTypeId = oAccounting.BudgetTypeId,
                        CashFllowId = oAccounting.CashFllowId,
                        Description = oAccounting.Description,
                        CurrencyType = oAccounting.CurrencyType,
                        OrdinalNumber = int.Parse(oAccounting.OrdinalNumberString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator)),
                        AmountUnitVnd = oAccounting.CurrencyTypeUnit == Constant.CurrencyType.VND.ToString() ? oAccounting.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        AmountUnitUsd = oAccounting.CurrencyTypeUnit == Constant.CurrencyType.USD.ToString() ? oAccounting.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        CurrencyTypeUnit = oAccounting.CurrencyTypeUnit,
                        AmountSavingVnd = oAccounting.CurrencyTypeSaving == Constant.CurrencyType.VND.ToString() ? oAccounting.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        AmountSavingUsd = oAccounting.CurrencyTypeSaving == Constant.CurrencyType.USD.ToString() ? oAccounting.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        CurrencyTypeSaving = oAccounting.CurrencyTypeSaving,
                        AmountBSVnd = oAccounting.CurrencyTypeBS == Constant.CurrencyType.VND.ToString() ? oAccounting.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        AmountBSUsd = oAccounting.CurrencyTypeBS == Constant.CurrencyType.USD.ToString() ? oAccounting.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        CurrencyTypeBS = oAccounting.CurrencyTypeBS
                    };

                    Lib.ResponseObject rs = null;
                    if(oAccounting.Id >0)
                        rs= Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/UpdateAccountingEntry", _Accounting);
                    else
                        rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/AddAccountingEntry", _Accounting);

                    string Code = rs.Code.ToString();
                    string Mes = rs.Message.ToString();
                    string Value = "";
                    if (rs.Value != null)
                        Value = rs.Value.ToString();

                    jResult = Json(new { Code = Code, Mes = Mes, Value = Value }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jResult = Json(new { Code = "-97", Mes = "Bạn chưa chọn Đơn vị." }, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }


            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult AllocateFundDelete()
        {
            JsonResult jResult = new JsonResult();

            return jResult;
        }
        #endregion

        #region Nộp Ngân Hàng
        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.AccountingEntry_District_PayBankManage, (int)Constant.Function.FuntionBranch_BorrowFundManage_TGNH })]
        public ActionResult PayBankManage()
        {
            return View();
        }
        #endregion

        #region Vay trả quỹ khác
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.AccountingEntry_District_BorrowFundManage, (int)Constant.Function.FuntionBranch_BorrowFundManage })]
        public ActionResult BorrowFundManage()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult BorrowFundGet(int id = 0, int poId = 0, string dateRange = "", int budgetTypeId = 0, int cashFllowId = 0, int refType = 0, int PageIndex = 1)
        {
            int Total = 0;
            string fromDate = string.Empty;
            string toDate = string.Empty;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            else
            {
                fromDate = Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            List<AccountingEntryViewModel> ListAccountingEntry = new List<AccountingEntryViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchAccountingEntry?id={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&refTypeId={6}&PageIndex={7}&PageSize={8}", new object[] { 0, oEmployee.POID, fromDate, toDate, budgetTypeId, cashFllowId, refType, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {

                    var oEntry = new AccountingEntryViewModel()
                    {
                        Id = dyn.Id,
                        TransNumber = dyn.TransNumber,
                        AmndEmpId = dyn.AmndEmpId,
                        PoId = dyn.PoId,
                        PoCode = dyn.PoCode,
                        SendPoId = dyn.SendPoId,
                        SendPoCode = dyn.SendPoCode,
                        ReceivePoId = dyn.ReceivePoId,
                        ReceivePoCode = dyn.ReceivePoCode,
                        RefType = dyn.RefType,
                        TransDate = dyn.TransDate,
                        RefTransNumber = dyn.RefTransNumber,
                        AmountVnd = dyn.AmountVnd,
                        AmountUsd = dyn.AmountUsd,
                        BudgetTypeId = dyn.BudgetTypeId,
                        CashFllowId = dyn.CashFllowId,
                        Description = dyn.Description,
                        CurrencyType = dyn.CurrencyType,
                        //BudgetTypeName = Repository.ListBudgetType[Convert.ToInt32(dyn.BudgetTypeId)]
                        AmountUnitVnd = decimal.Parse("0" + dyn.AmountUnitVnd.ToString()),
                        AmountUnitUsd = decimal.Parse("0" + dyn.AmountUnitUsd.ToString()),
                        CurrencyTypeUnit = dyn.CurrencyTypeUnit,
                        AmountSavingVnd = decimal.Parse("0" + dyn.AmountSavingVnd.ToString()),
                        AmountSavingUsd = decimal.Parse("0" + dyn.AmountSavingUsd.ToString()),
                        CurrencyTypeSaving = dyn.CurrencyTypeSaving,
                        AmountBSVnd = decimal.Parse("0" + dyn.AmountBSVnd.ToString()),
                        AmountBSUsd = decimal.Parse("0" + dyn.AmountBSUsd.ToString()),
                        CurrencyTypeBS = dyn.CurrencyTypeBS,
                        BorrowMethod = dyn.BorrowMethod,
                        BorrowMethodUnit = dyn.BorrowMethodUnit,
                        BorrowMethodSaving = dyn.BorrowMethodSaving,
                        BorrowMethodBS = dyn.BorrowMethodBS,
                        CreatedPoCode = dyn.CreatedPoCode
                    };

                    if (!ListAccountingEntry.Contains(oEntry))
                        ListAccountingEntry.Add(oEntry);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.refType = refType;
            return PartialView(ListAccountingEntry);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult BorrowFundCreate(int id = 0, int refType = 0)
        {
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
            List<SelectListItem> ListBorrowMethod = new List<SelectListItem>();

            AccountingEntryViewModel oEntry = new AccountingEntryViewModel();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            var rs = Helper.Invoke(Common.Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchOrdinalNumber?PoId={0}&RefType={1}&Month={2}", oEmployee.POID, refType, DateTime.Now.Month), null);

            if (rs.Code == "00")
            {
                oEntry.OrdinalNumberString = rs.Value.ToString();
            }
            else
            {
                oEntry.OrdinalNumberString = "1";
            }
            oEntry.TransDate = Session[Constant.TIMEWORK_SESSION].ToString();

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

            oEntry.RefType = oEntry.RefType;
            oEntry.Amount = "0";
            oEntry.AmountUnitString = "0";
            oEntry.AmountSavingString = "0";
            oEntry.AmountBSString = "0";
            oEntry.AmountVnd = 0;
            oEntry.AmountUsd = 0;
            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListCashFlow = ListCashFlow;
            ViewBag.ListCurrencyType = ListCurrencyType;
            ViewBag.ListBorrowMethod = ListBorrowMethod;
            ViewBag.refType = refType;
            return PartialView(oEntry);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult BorrowFundUpdate(AccountingEntryViewModel oEntry)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                decimal dAmountVnd = 0;
                decimal dAmountUsd = 0;
                if (oEntry.CurrencyType == Constant.CurrencyType.VND.ToString())
                {
                    dAmountVnd = decimal.Parse(oEntry.Amount.Replace(Constant.GroupSeparator, ""));
                    dAmountUsd = 0;
                }
                else
                {
                    dAmountUsd = decimal.Parse(oEntry.Amount.Replace(Constant.GroupSeparator, ""));
                    dAmountVnd = 0;
                }
                var _Accounting = new
                {
                    Id = oEntry.Id,
                    TransNumber = oEntry.TransNumber,
                    AmndEmpId = oEmployee.ID,
                    PoId = oEmployee.POID,
                    CreatedPoId = oEmployee.POID,
                    SendPoId = 0,
                    ReceivePoId = 0,
                    RefType = oEntry.RefType,
                    TransDate = new DateTime(int.Parse(oEntry.TransDate.Split('/')[2]), int.Parse(oEntry.TransDate.Split('/')[1]), int.Parse(oEntry.TransDate.Split('/')[0])).ToString("dd-MM-yyyy"),
                    RefTransNumber = oEntry.RefTransNumber,
                    AmountVnd = dAmountVnd,
                    AmountUsd = dAmountUsd,
                    BudgetTypeId = oEntry.BudgetTypeId,
                    CashFllowId = oEntry.CashFllowId,
                    Description = oEntry.Description,
                    CurrencyType = oEntry.CurrencyType,
                    OrdinalNumber = int.Parse(oEntry.OrdinalNumberString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator)),
                    AmountUnitVnd = oEntry.CurrencyTypeUnit == Constant.CurrencyType.VND.ToString() ? oEntry.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    AmountUnitUsd = oEntry.CurrencyTypeUnit == Constant.CurrencyType.USD.ToString() ? oEntry.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    CurrencyTypeUnit = oEntry.CurrencyTypeUnit,
                    AmountSavingVnd = oEntry.CurrencyTypeSaving == Constant.CurrencyType.VND.ToString() ? oEntry.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    AmountSavingUsd = oEntry.CurrencyTypeSaving == Constant.CurrencyType.USD.ToString() ? oEntry.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    CurrencyTypeSaving = oEntry.CurrencyTypeSaving,
                    AmountBSVnd = oEntry.CurrencyTypeBS == Constant.CurrencyType.VND.ToString() ? oEntry.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    AmountBSUsd = oEntry.CurrencyTypeBS == Constant.CurrencyType.USD.ToString() ? oEntry.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    CurrencyTypeBS = oEntry.CurrencyTypeBS,
                    BorrowMethod = oEntry.BorrowMethod,
                    BorrowMethodUnit = oEntry.BorrowMethodUnit,
                    BorrowMethodSaving = oEntry.BorrowMethodSaving,
                    BorrowMethodBS = oEntry.BorrowMethodBS
                };

                var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/AddAccountingEntry", _Accounting);

                string Code = rs.Code.ToString();
                string Mes = rs.Message.ToString();
                string Value = "";
                if (rs.Value != null)
                    Value = rs.Value.ToString();
                jResult = Json(new { Code = Code, Mes = Mes, Value = Value }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult BorrowFundDelete(int id)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                var oRequest = new
                {
                    Id = id,
                    AmndEmpId = oEmployee.ID,
                    PoId = oEmployee.POID
                };
                var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/DeleteAccountingEntry", oRequest);
                string Code = rs.Code.ToString();
                string Mes = rs.Message.ToString();

                jResult = Json(new { Code = Code, Mes = Mes }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult PaymentBorrowFundGet(int id = 0, int poId = 0, string dateRange = "", int budgetTypeId = 0, int cashFllowId = 0, int refType = 0, int PageIndex = 1)
        {
            int Total = 0;
            List<AccountingEntryViewModel> ListAccountingEntry = new List<AccountingEntryViewModel>();
            string fromDate = "";
            string toDate = "";
            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            else
            {
                fromDate= Session[Constant.TIMEWORK_SESSION].ToString();
                toDate= Session[Constant.TIMEWORK_SESSION].ToString();
            }
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchAccountingEntry?id={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&refTypeId={6}&PageIndex={7}&PageSize={8}", new object[] { 0, oEmployee.POID, fromDate, toDate, budgetTypeId, cashFllowId, refType, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    var oEntry = new AccountingEntryViewModel()
                    {
                        Id = dyn.Id,
                        TransNumber = dyn.TransNumber,
                        AmndEmpId = dyn.AmndEmpId,
                        PoId = dyn.PoId,
                        PoCode= dyn.PoCode,
                        SendPoId = dyn.SendPoId,
                        ReceivePoId = dyn.ReceivePoId,
                        RefType = dyn.RefType,
                        TransDate = dyn.TransDate,
                        RefTransNumber = dyn.RefTransNumber,
                        AmountVnd = dyn.AmountVnd,
                        AmountUsd = dyn.AmountUsd,
                        BudgetTypeId = dyn.BudgetTypeId,
                        CashFllowId = dyn.CashFllowId,
                        Description = dyn.Description,
                        CurrencyType = dyn.CurrencyType,
                        AmountUnitVnd = decimal.Parse("0" + dyn.AmountUnitVnd.ToString()),
                        AmountUnitUsd = decimal.Parse("0" + dyn.AmountUnitUsd.ToString()),
                        CurrencyTypeUnit = dyn.CurrencyTypeUnit,
                        AmountSavingVnd = decimal.Parse("0" + dyn.AmountSavingVnd.ToString()),
                        AmountSavingUsd = decimal.Parse("0" + dyn.AmountSavingUsd.ToString()),
                        CurrencyTypeSaving = dyn.CurrencyTypeSaving,
                        AmountBSVnd = decimal.Parse("0" + dyn.AmountBSVnd.ToString()),
                        AmountBSUsd = decimal.Parse("0" + dyn.AmountBSUsd.ToString()),
                        CurrencyTypeBS = dyn.CurrencyTypeBS,
                        BorrowMethod = dyn.BorrowMethod,
                        BorrowMethodUnit = dyn.BorrowMethodUnit,
                        BorrowMethodSaving = dyn.BorrowMethodSaving,
                        BorrowMethodBS = dyn.BorrowMethodBS,
                        CreatedPoCode = dyn.CreatedPoCode
                    };

                    if (!ListAccountingEntry.Contains(oEntry))
                        ListAccountingEntry.Add(oEntry);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.refType = refType;
            return PartialView(ListAccountingEntry);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult PaymentBorrowFundCreate(int id = 0, int refType = 0)
        {
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
            List<SelectListItem> ListBorrowMethod = new List<SelectListItem>();
            AccountingEntryViewModel oEntry = new AccountingEntryViewModel();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            var rs = Helper.Invoke(Common.Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchOrdinalNumber?PoId={0}&RefType={1}&Month={2}", oEmployee.POID, refType, DateTime.Now.Month), null);

            if (rs.Code == "00")
            {
                oEntry.OrdinalNumberString = rs.Value.ToString();
            }
            else
            {
                oEntry.OrdinalNumberString = "1";
            }
            oEntry.TransDate = Session[Constant.TIMEWORK_SESSION].ToString();
            #region ListBudgetType

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

            oEntry.Amount = "0";
            oEntry.AmountUnitString = "0";
            oEntry.AmountSavingString = "0";
            oEntry.AmountBSString = "0";
            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListCashFlow = ListCashFlow;
            ViewBag.ListCurrencyType = ListCurrencyType;
            ViewBag.ListBorrowMethod = ListBorrowMethod;
            ViewBag.refType = refType;
            return PartialView(oEntry);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult PaymentBorrowFundUpdate(AccountingEntryViewModel oEntry)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                if (!string.IsNullOrEmpty(oEntry.RefTransNumber))
                {
                    EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                    decimal dAmountVnd = 0;
                    decimal dAmountUsd = 0;
                    if (oEntry.CurrencyType == Constant.CurrencyType.VND.ToString())
                    {
                        dAmountVnd = decimal.Parse(oEntry.Amount.Replace(Constant.GroupSeparator, ""));
                        dAmountUsd = 0;
                    }
                    else
                    {
                        dAmountUsd = decimal.Parse(oEntry.Amount.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                        dAmountVnd = 0;
                    }
                    var _Accounting = new
                    {
                        Id = oEntry.Id,
                        TransNumber = oEntry.TransNumber,
                        AmndEmpId = oEmployee.ID,
                        PoId = oEmployee.POID,
                        CreatedPoId = oEmployee.POID,
                        SendPoId = 0,
                        ReceivePoId = 0,
                        RefType = oEntry.RefType,
                        TransDate = new DateTime(int.Parse(oEntry.TransDate.Split('/')[2]), int.Parse(oEntry.TransDate.Split('/')[1]), int.Parse(oEntry.TransDate.Split('/')[0])).ToString("dd-MM-yyyy"),
                        RefTransNumber = oEntry.RefTransNumber,
                        AmountVnd = dAmountVnd,
                        AmountUsd = dAmountUsd,
                        BudgetTypeId = oEntry.BudgetTypeId,
                        CashFllowId = oEntry.CashFllowId,
                        Description = oEntry.Description,
                        CurrencyType = oEntry.CurrencyType,
                        OrdinalNumber = int.Parse(oEntry.OrdinalNumberString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator)),
                        AmountUnitVnd = oEntry.CurrencyTypeUnit == Constant.CurrencyType.VND.ToString() ? oEntry.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        AmountUnitUsd = oEntry.CurrencyTypeUnit == Constant.CurrencyType.USD.ToString() ? oEntry.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        CurrencyTypeUnit = oEntry.CurrencyTypeUnit,
                        AmountSavingVnd = oEntry.CurrencyTypeSaving == Constant.CurrencyType.VND.ToString() ? oEntry.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        AmountSavingUsd = oEntry.CurrencyTypeSaving == Constant.CurrencyType.USD.ToString() ? oEntry.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        CurrencyTypeSaving = oEntry.CurrencyTypeSaving,
                        AmountBSVnd = oEntry.CurrencyTypeBS == Constant.CurrencyType.VND.ToString() ? oEntry.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        AmountBSUsd = oEntry.CurrencyTypeBS == Constant.CurrencyType.USD.ToString() ? oEntry.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        CurrencyTypeBS = oEntry.CurrencyTypeBS,
                        BorrowMethod = oEntry.BorrowMethod,
                        BorrowMethodUnit = oEntry.BorrowMethodUnit,
                        BorrowMethodSaving = oEntry.BorrowMethodSaving,
                        BorrowMethodBS = oEntry.BorrowMethodBS
                    };

                    var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/AddAccountingEntry", _Accounting);

                    string Code = rs.Code.ToString();
                    string Mes = rs.Message.ToString();
                    string Value = "";
                    if (rs.Value != null)
                        Value = rs.Value.ToString();

                    jResult = Json(new { Code = Code, Mes = Mes, Value = Value }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jResult = Json(new { Code = "-97", Mes = "Bạn chưa chọn khoản vay!" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult PaymentBorrowFundDelete(int AccountEntryId)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                var oRequest = new
                {
                    Id = AccountEntryId,
                    AmndEmpId = oEmployee.ID,
                    PoId = oEmployee.POID
                };
                var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/DeleteAccountingEntry", oRequest);
                string Code = rs.Code.ToString();
                string Mes = rs.Message.ToString();

                jResult = Json(new { Code = Code, Mes = Mes }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult BorrowSelect(int refType = 0, int poId = 0, int budgetTypeID = 0, int cashFllowId = 0, string dateRange = "", int PageIndex = 1)
        {
            int Total = 0;
            string fromDate = Session[Constant.TIMEWORK_SESSION].ToString(); ;
            string toDate = Session[Constant.TIMEWORK_SESSION].ToString(); ;

            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];

            int _PoID = po.ID;
            if (poId > 0)
            {
                _PoID = poId;
            }

            List<AccountingEntryViewModel> ListAccountingEntry = new List<AccountingEntryViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchPaymentAccountingEntry?refTypeId={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&PageIndex={6}&PageSize={7}", new object[] { refType, _PoID, fromDate, toDate, budgetTypeID, cashFllowId, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {

                    var oEntry = new AccountingEntryViewModel()
                    {
                        Id = dyn.Id,
                        TransNumber = dyn.TransNumber,
                        RefType = dyn.RefType,
                        TransDate = dyn.TransDate,
                        BudgetTypeId = dyn.BudgetTypeId,
                        CashFllowId = dyn.CashFllowId,
                        CurrencyType = dyn.CurrencyType,
                        AmountVnd = dyn.AmountVnd,
                        AmountUsd = dyn.AmountUsd,
                        AmountUnitVnd = dyn.AmountUnitVnd,
                        AmountUnitUsd = dyn.AmountUnitUsd,
                        AmountSavingVnd = dyn.AmountSavingVnd,
                        AmountSavingUsd = dyn.AmountSavingUsd,
                        AmountBSVnd = dyn.AmountBSVnd,
                        AmountBSUsd = dyn.AmountBSUsd,
                        CurrencyTypeUnit = dyn.CurrencyTypeUnit,
                        CurrencyTypeSaving = dyn.CurrencyTypeSaving,
                        CurrencyTypeBS = dyn.CurrencyTypeBS,
                        BorrowMethod = dyn.BorrowMethod,
                        BorrowMethodUnit = dyn.BorrowMethodUnit,
                        BorrowMethodSaving = dyn.BorrowMethodSaving,
                        BorrowMethodBS = dyn.BorrowMethodBS

                    };

                    if (!ListAccountingEntry.Contains(oEntry))
                        ListAccountingEntry.Add(oEntry);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.refType = refType;
            return PartialView(ListAccountingEntry);


        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult BorrowSelectGet(int refType = 0, int poId = 0, int budgetTypeID = 0, int cashFllowId = 0, string dateRange = "", int PageIndex = 1)
        {
            int Total = 0;
            string fromDate = string.Empty;
            string toDate = string.Empty;

            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            else
            {
                fromDate= Session[Constant.TIMEWORK_SESSION].ToString();
                toDate= Session[Constant.TIMEWORK_SESSION].ToString();
            }

            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];

            int _PoID = po.ID;
            if (poId > 0)
            {
                _PoID = poId;
            }
            List<AccountingEntryViewModel> ListAccountingEntry = new List<AccountingEntryViewModel>();

            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchPaymentAccountingEntry?refTypeId={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&PageIndex={6}&PageSize={7}", new object[] { refType, _PoID, fromDate, toDate, budgetTypeID, cashFllowId, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {

                    var oEntry = new AccountingEntryViewModel()
                    {
                        Id = dyn.Id,
                        TransNumber = dyn.TransNumber,
                        RefType = dyn.RefType,
                        TransDate = dyn.TransDate,
                        BudgetTypeId = dyn.BudgetTypeId,
                        CashFllowId = dyn.CashFllowId,
                        CurrencyType = dyn.CurrencyType,
                        AmountVnd = dyn.AmountVnd,
                        AmountUsd = dyn.AmountUsd,
                        AmountUnitVnd = dyn.AmountUnitVnd,
                        AmountUnitUsd = dyn.AmountUnitUsd,
                        AmountSavingVnd = dyn.AmountSavingVnd,
                        AmountSavingUsd = dyn.AmountSavingUsd,
                        AmountBSVnd = dyn.AmountBSVnd,
                        AmountBSUsd = dyn.AmountBSUsd,
                        CurrencyTypeUnit = dyn.CurrencyTypeUnit,
                        CurrencyTypeSaving = dyn.CurrencyTypeSaving,
                        CurrencyTypeBS = dyn.CurrencyTypeBS,
                        BorrowMethod = dyn.BorrowMethod,
                        BorrowMethodUnit = dyn.BorrowMethodUnit,
                        BorrowMethodSaving = dyn.BorrowMethodSaving,
                        BorrowMethodBS = dyn.BorrowMethodBS

                    };

                    if (!ListAccountingEntry.Contains(oEntry))
                        ListAccountingEntry.Add(oEntry);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.refType = refType;
            return PartialView(ListAccountingEntry);

        }

        #endregion

        #region Nhập thay thế Vay trả quỹ khác
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage })]
        public ActionResult BorrowFundReplaceManage()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult BorrowFundReplaceGet(int id = 0, int poId = 0, string dateRange = "", int budgetTypeId = 0, int cashFllowId = 0, int refType = 0, int PageIndex = 1)
        {
            int Total = 0;
            string fromDate = string.Empty;
            string toDate = string.Empty;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            else
            {
                fromDate= Session[Constant.TIMEWORK_SESSION].ToString();
                toDate= Session[Constant.TIMEWORK_SESSION].ToString();
            }
            List<AccountingEntryViewModel> ListAccountingEntry = new List<AccountingEntryViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchAccountingEntry?id={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&refTypeId={6}&PageIndex={7}&PageSize={8}&isReplace={9}", new object[] { 0, oEmployee.POID, fromDate, toDate, budgetTypeId, cashFllowId, refType, PageIndex, Common.Constant.PageSize, 1 }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {

                    var oEntry = new AccountingEntryViewModel()
                    {
                        Id = dyn.Id,
                        TransNumber = dyn.TransNumber,
                        AmndEmpId = dyn.AmndEmpId,
                        PoId = dyn.PoId,
                        PoCode = dyn.PoCode,
                        SendPoId = dyn.SendPoId,
                        SendPoCode = dyn.SendPoCode,
                        ReceivePoId = dyn.ReceivePoId,
                        ReceivePoCode = dyn.ReceivePoCode,
                        RefType = dyn.RefType,
                        TransDate = dyn.TransDate,
                        RefTransNumber = dyn.RefTransNumber,
                        AmountVnd = dyn.AmountVnd,
                        AmountUsd = dyn.AmountUsd,
                        AmountSavingVnd = dyn.AmountSavingVnd,
                        AmountSavingUsd = dyn.AmountSavingUsd,
                        AmountBSVnd = dyn.AmountBSVnd,
                        AmountBSUsd = dyn.AmountBSUsd,
                        AmountUnitVnd = dyn.AmountUnitVnd,
                        AmountUnitUsd = dyn.AmountUnitUsd,
                        BudgetTypeId = dyn.BudgetTypeId,
                        CashFllowId = dyn.CashFllowId,
                        Description = dyn.Description,
                        CurrencyType = dyn.CurrencyType,
                        BorrowMethod = dyn.BorrowMethod,
                        BorrowMethodUnit = dyn.BorrowMethodUnit,
                        BorrowMethodSaving = dyn.BorrowMethodSaving,
                        BorrowMethodBS = dyn.BorrowMethodBS,
                        CreatedPoCode = dyn.CreatedPoCode

                    };

                    if (!ListAccountingEntry.Contains(oEntry))
                        ListAccountingEntry.Add(oEntry);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.refType = refType;
            return PartialView(ListAccountingEntry);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult BorrowFundReplaceCreate(int id = 0, int refType = 0)
        {
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
            List<SelectListItem> ListBorrowMethod = new List<SelectListItem>();
            AccountingEntryViewModel oEntry = new AccountingEntryViewModel();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            //var rs = Helper.Invoke(Common.Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchOrdinalNumber?PoId={0}&RefType={1}&Month={2}", oEmployee.POID, refType, DateTime.Now.Month), null);

            //if (rs.Code == "00")
            //{
            //    oEntry.OrdinalNumberString = rs.Value.ToString();
            //}
            //else
            //{
            //    oEntry.OrdinalNumberString = "1";
            //}
            oEntry.OrdinalNumberString = "1";
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

            oEntry.RefType = oEntry.RefType;
            oEntry.TransDate = Session[Constant.TIMEWORK_SESSION].ToString();
            oEntry.Amount = "0";
            oEntry.AmountUnitString = "0";
            oEntry.AmountSavingString = "0";
            oEntry.AmountBSString = "0";
            oEntry.AmountVnd = 0;
            oEntry.AmountUsd = 0;
            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListCashFlow = ListCashFlow;
            ViewBag.ListCurrencyType = ListCurrencyType;
            ViewBag.ListBorrowMethod = ListBorrowMethod;
            ViewBag.refType = refType;
            return PartialView(oEntry);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult BorrowFundReplaceUpdate(AccountingEntryViewModel oEntry)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                if (oEntry.PoId > 0)
                {
                    EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                    decimal dAmountVnd = 0;
                    decimal dAmountUsd = 0;
                    if (oEntry.CurrencyType == Constant.CurrencyType.VND.ToString())
                    {
                        dAmountVnd = decimal.Parse(oEntry.Amount.Replace(Constant.GroupSeparator, ""));
                        dAmountUsd = 0;
                    }
                    else
                    {
                        dAmountUsd = decimal.Parse(oEntry.Amount.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                        dAmountVnd = 0;
                    }
                    var _Accounting = new
                    {
                        Id = oEntry.Id,
                        TransNumber = oEntry.TransNumber,
                        AmndEmpId = oEmployee.ID,
                        Poid = oEntry.PoId,
                        CreatedPoId = oEmployee.POID,
                        SendPoId = 0,
                        ReceivePoId = 0,
                        RefType = oEntry.RefType,
                        TransDate = new DateTime(int.Parse(oEntry.TransDate.Split('/')[2]), int.Parse(oEntry.TransDate.Split('/')[1]), int.Parse(oEntry.TransDate.Split('/')[0])).ToString("dd-MM-yyyy"),
                        RefTransNumber = oEntry.RefTransNumber,
                        AmountVnd = dAmountVnd,
                        AmountUsd = dAmountUsd,
                        BudgetTypeId = oEntry.BudgetTypeId,
                        CashFllowId = oEntry.CashFllowId,
                        Description = oEntry.Description,
                        CurrencyType = oEntry.CurrencyType,
                        OrdinalNumber = int.Parse(oEntry.OrdinalNumberString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator)),
                        AmountUnitVnd = oEntry.CurrencyTypeUnit == Constant.CurrencyType.VND.ToString() ? oEntry.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        AmountUnitUsd = oEntry.CurrencyTypeUnit == Constant.CurrencyType.USD.ToString() ? oEntry.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        CurrencyTypeUnit = oEntry.CurrencyTypeUnit,
                        AmountSavingVnd = oEntry.CurrencyTypeSaving == Constant.CurrencyType.VND.ToString() ? oEntry.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        AmountSavingUsd = oEntry.CurrencyTypeSaving == Constant.CurrencyType.USD.ToString() ? oEntry.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        CurrencyTypeSaving = oEntry.CurrencyTypeSaving,
                        AmountBSVnd = oEntry.CurrencyTypeBS == Constant.CurrencyType.VND.ToString() ? oEntry.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        AmountBSUsd = oEntry.CurrencyTypeBS == Constant.CurrencyType.USD.ToString() ? oEntry.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                        CurrencyTypeBS = oEntry.CurrencyTypeBS,
                        BorrowMethod = oEntry.BorrowMethod,
                        BorrowMethodUnit = oEntry.BorrowMethodUnit,
                        BorrowMethodSaving = oEntry.BorrowMethodSaving,
                        BorrowMethodBS = oEntry.BorrowMethodBS
                    };

                    var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/AddAccountingEntry", _Accounting);

                    string Code = rs.Code.ToString();
                    string Mes = rs.Message.ToString();
                    string Value = "";
                    if (rs.Value != null)
                        Value = rs.Value.ToString();
                    jResult = Json(new { Code = Code, Mes = Mes, Value = Value }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jResult = Json(new { Code = "-98", Mes = "Bạn chưa chọn Đơn vị cần nhập thay thế!" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult BorrowFundReplaceDelete(int id)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                var oRequest = new
                {
                    Id = id,
                    AmndEmpId = oEmployee.ID,
                    PoId = oEmployee.POID
                };
                var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/DeleteAccountingEntry", oRequest);
                string Code = rs.Code.ToString();
                string Mes = rs.Message.ToString();

                jResult = Json(new { Code = Code, Mes = Mes }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult PaymentBorrowFundReplaceGet(int id = 0, int poId = 0, string dateRange = "", int budgetTypeId = 0, int cashFllowId = 0, int refType = 0, int PageIndex = 1)
        {
            int Total = 0;
            List<AccountingEntryViewModel> ListAccountingEntry = new List<AccountingEntryViewModel>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            string fromDate = "";
            string toDate = "";
            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            else
            {
                fromDate= Session[Constant.TIMEWORK_SESSION].ToString();
                toDate= Session[Constant.TIMEWORK_SESSION].ToString();
            }

            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchAccountingEntry?id={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&refTypeId={6}&PageIndex={7}&PageSize={8}&isReplace={9}", new object[] { 0, oEmployee.POID, fromDate, toDate, budgetTypeId, cashFllowId, refType, PageIndex, Common.Constant.PageSize, 1 }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    var oEntry = new AccountingEntryViewModel()
                    {
                        Id = dyn.Id,
                        TransNumber = dyn.TransNumber,
                        AmndEmpId = dyn.AmndEmpId,
                        PoId = dyn.PoId,
                        PoCode = dyn.PoCode,
                        SendPoId = dyn.SendPoId,
                        SendPoCode = dyn.SendPoCode,
                        ReceivePoId = dyn.ReceivePoId,
                        ReceivePoCode = dyn.ReceivePoCode,
                        RefType = dyn.RefType,
                        TransDate = dyn.TransDate,
                        RefTransNumber = dyn.RefTransNumber,
                        AmountVnd = dyn.AmountVnd,
                        AmountUsd = dyn.AmountUsd,
                        BudgetTypeId = dyn.BudgetTypeId,
                        CashFllowId = dyn.CashFllowId,
                        Description = dyn.Description,
                        CurrencyType = dyn.CurrencyType,
                        //BudgetTypeName = Repository.ListBudgetType[Convert.ToInt32(dyn.BudgetTypeId)],
                        AmountUnitVnd = decimal.Parse(dyn.AmountUnitVnd.ToString()),
                        AmountUnitUsd = decimal.Parse(dyn.AmountUnitUsd.ToString()),
                        CurrencyTypeUnit = dyn.CurrencyTypeUnit,
                        AmountSavingVnd = decimal.Parse(dyn.AmountSavingVnd.ToString()),
                        AmountSavingUsd = decimal.Parse(dyn.AmountSavingUsd.ToString()),
                        CurrencyTypeSaving = dyn.CurrencyTypeSaving,
                        AmountBSVnd = decimal.Parse(dyn.AmountBSVnd.ToString()),
                        AmountBSUsd = decimal.Parse(dyn.AmountBSUsd.ToString()),
                        CurrencyTypeBS = dyn.CurrencyTypeBS,
                        BorrowMethod = dyn.BorrowMethod,
                        BorrowMethodUnit = dyn.BorrowMethodUnit,
                        BorrowMethodSaving = dyn.BorrowMethodSaving,
                        BorrowMethodBS = dyn.BorrowMethodBS,
                        CreatedPoCode = dyn.CreatedPoCode
                    };

                    if (!ListAccountingEntry.Contains(oEntry))
                        ListAccountingEntry.Add(oEntry);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.refType = refType;
            return PartialView(ListAccountingEntry);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult PaymentBorrowFundReplaceCreate(int id = 0, int refType = 0)
        {
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
            List<SelectListItem> ListBorrowMethod = new List<SelectListItem>();
            AccountingEntryViewModel oEntry = new AccountingEntryViewModel();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            //var rs = Helper.Invoke(Common.Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchOrdinalNumber?PoId={0}&RefType={1}&Month={2}", oEmployee.POID, refType, DateTime.Now.Month), null);

            //if (rs.Code == "00")
            //{
            //    oEntry.OrdinalNumberString = rs.Value.ToString();
            //}
            //else
            //{
            //    oEntry.OrdinalNumberString = "1";
            //}
            oEntry.OrdinalNumberString = "1";
            oEntry.TransDate = Session[Constant.TIMEWORK_SESSION].ToString();

            #region ListBudgetType

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

            oEntry.Amount = "0";
            oEntry.AmountUnitString = "0";
            oEntry.AmountSavingString = "0";
            oEntry.AmountBSString = "0";
            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListCashFlow = ListCashFlow;
            ViewBag.ListCurrencyType = ListCurrencyType;
            ViewBag.ListBorrowMethod = ListBorrowMethod;
            ViewBag.refType = refType;
            return PartialView(oEntry);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult PaymentBorrowFundReplaceUpdate(AccountingEntryViewModel oEntry)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                decimal dAmountVnd = 0;
                decimal dAmountUsd = 0;
                if (oEntry.CurrencyType == Constant.CurrencyType.VND.ToString())
                {
                    dAmountVnd = decimal.Parse(oEntry.Amount.Replace(Constant.GroupSeparator, ""));
                    dAmountUsd = 0;
                }
                else
                {
                    dAmountUsd = decimal.Parse(oEntry.Amount.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                    dAmountVnd = 0;
                }
                var _Accounting = new
                {
                    Id = oEntry.Id,
                    TransNumber = oEntry.TransNumber,
                    AmndEmpId = oEmployee.ID,
                    Poid = oEntry.PoId,
                    CreatedPoId = oEmployee.POID,
                    SendPoId = 0,
                    ReceivePoId = 0,
                    RefType = oEntry.RefType,
                    TransDate = new DateTime(int.Parse(oEntry.TransDate.Split('/')[2]), int.Parse(oEntry.TransDate.Split('/')[1]), int.Parse(oEntry.TransDate.Split('/')[0])).ToString("dd-MM-yyyy"),
                    RefTransNumber = oEntry.RefTransNumber,
                    AmountVnd = dAmountVnd,
                    AmountUsd = dAmountUsd,
                    BudgetTypeId = oEntry.BudgetTypeId,
                    CashFllowId = oEntry.CashFllowId,
                    Description = oEntry.Description,
                    CurrencyType = oEntry.CurrencyType,
                    OrdinalNumber = int.Parse(oEntry.OrdinalNumberString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator)),
                    AmountUnitVnd = oEntry.CurrencyType == Constant.CurrencyType.VND.ToString() ? oEntry.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    AmountUnitUsd = oEntry.CurrencyType == Constant.CurrencyType.USD.ToString() ? oEntry.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    CurrencyTypeUnit = oEntry.CurrencyTypeUnit,
                    AmountSavingVnd = oEntry.CurrencyTypeSaving == Constant.CurrencyType.VND.ToString() ? oEntry.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    AmountSavingUsd = oEntry.CurrencyTypeSaving == Constant.CurrencyType.USD.ToString() ? oEntry.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    CurrencyTypeSaving = oEntry.CurrencyTypeSaving,
                    AmountBSVnd = oEntry.CurrencyTypeBS == Constant.CurrencyType.VND.ToString() ? oEntry.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    AmountBSUsd = oEntry.CurrencyTypeBS == Constant.CurrencyType.USD.ToString() ? oEntry.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                    CurrencyTypeBS = oEntry.CurrencyTypeBS,
                    BorrowMethod = oEntry.BorrowMethod,
                    BorrowMethodUnit = oEntry.BorrowMethodUnit,
                    BorrowMethodSaving = oEntry.BorrowMethodSaving,
                    BorrowMethodBS = oEntry.BorrowMethodBS
                };

                var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/AddAccountingEntry", _Accounting);

                string Code = rs.Code.ToString();
                string Mes = rs.Message.ToString();
                string Value = "";
                if (rs.Value != null)
                    Value = rs.Value.ToString();

                jResult = Json(new { Code = Code, Mes = Mes, Value = Value }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult PaymentBorrowFundReplaceDelete(int AccountEntryId)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                var oRequest = new
                {
                    Id = AccountEntryId,
                    AmndEmpId = oEmployee.ID,
                    PoId = oEmployee.POID
                };
                var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/DeleteAccountingEntry", oRequest);
                string Code = rs.Code.ToString();
                string Mes = rs.Message.ToString();

                jResult = Json(new { Code = Code, Mes = Mes }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult BorrowReplaceSelect(int refType = 0, int poId = 0, int budgetTypeID = 0, int cashFllowId = 0, string dateRange = "", int PageIndex = 1)
        {
            int Total = 0;
            string fromDate = Session[Constant.TIMEWORK_SESSION].ToString();
            string toDate = Session[Constant.TIMEWORK_SESSION].ToString();

            List<AccountingEntryViewModel> ListAccountingEntry = new List<AccountingEntryViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchPaymentAccountingEntry?refTypeId={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&PageIndex={6}&PageSize={7}", new object[] { refType, poId, fromDate, toDate, budgetTypeID, cashFllowId, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {

                    var oEntry = new AccountingEntryViewModel()
                    {
                        Id = dyn.Id,
                        TransNumber = dyn.TransNumber,
                        RefType = dyn.RefType,
                        TransDate = dyn.TransDate,
                        BudgetTypeId = dyn.BudgetTypeId,
                        CashFllowId = dyn.CashFllowId,
                        CurrencyType = dyn.CurrencyType,
                        AmountVnd = dyn.AmountVnd,
                        AmountUsd = dyn.AmountUsd,
                        AmountUnitVnd = dyn.AmountUnitVnd,
                        AmountUnitUsd = dyn.AmountUnitUsd,
                        AmountSavingVnd = dyn.AmountSavingVnd,
                        AmountSavingUsd = dyn.AmountSavingUsd,
                        AmountBSVnd = dyn.AmountBSVnd,
                        AmountBSUsd = dyn.AmountBSUsd,
                        CurrencyTypeUnit = dyn.CurrencyTypeUnit,
                        CurrencyTypeSaving = dyn.CurrencyTypeSaving,
                        CurrencyTypeBS = dyn.CurrencyTypeBS,
                        BorrowMethod = dyn.BorrowMethod,
                        BorrowMethodUnit = dyn.BorrowMethodUnit,
                        BorrowMethodSaving = dyn.BorrowMethodSaving,
                        BorrowMethodBS = dyn.BorrowMethodBS
                    };

                    if (!ListAccountingEntry.Contains(oEntry))
                        ListAccountingEntry.Add(oEntry);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.refType = refType;
            return PartialView(ListAccountingEntry);


        }

        public ActionResult BorrowReplaceSelectGet(int refType = 0, int poId = 0, int budgetTypeID = 0, int cashFllowId = 0, string dateRange = "", int PageIndex = 1)
        {
            int Total = 0;
            string fromDate = string.Empty;
            string toDate = string.Empty;

            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            else
            {
                fromDate= Session[Constant.TIMEWORK_SESSION].ToString();
                toDate= Session[Constant.TIMEWORK_SESSION].ToString();
            }
            List<AccountingEntryViewModel> ListAccountingEntry = new List<AccountingEntryViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchPaymentAccountingEntry?refTypeId={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&PageIndex={6}&PageSize={7}", new object[] { refType, poId, fromDate, toDate, budgetTypeID, cashFllowId, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {

                    var oEntry = new AccountingEntryViewModel()
                    {
                        Id = dyn.Id,
                        TransNumber = dyn.TransNumber,
                        RefType = dyn.RefType,
                        TransDate = dyn.TransDate,
                        BudgetTypeId = dyn.BudgetTypeId,
                        CashFllowId = dyn.CashFllowId,
                        CurrencyType = dyn.CurrencyType,
                        AmountVnd = dyn.AmountVnd,
                        AmountUsd = dyn.AmountUsd,
                        AmountUnitVnd = dyn.AmountUnitVnd,
                        AmountUnitUsd = dyn.AmountUnitUsd,
                        AmountSavingVnd = dyn.AmountSavingVnd,
                        AmountSavingUsd = dyn.AmountSavingUsd,
                        AmountBSVnd = dyn.AmountBSVnd,
                        AmountBSUsd = dyn.AmountBSUsd,
                        CurrencyTypeUnit = dyn.CurrencyTypeUnit,
                        CurrencyTypeSaving = dyn.CurrencyTypeSaving,
                        CurrencyTypeBS = dyn.CurrencyTypeBS,
                        BorrowMethod = dyn.BorrowMethod,
                        BorrowMethodUnit = dyn.BorrowMethodUnit,
                        BorrowMethodSaving = dyn.BorrowMethodSaving,
                        BorrowMethodBS = dyn.BorrowMethodBS

                    };

                    if (!ListAccountingEntry.Contains(oEntry))
                        ListAccountingEntry.Add(oEntry);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.refType = refType;
            return PartialView(ListAccountingEntry);

        }

        #endregion

        #region Nhập thay thế Tiếp nộp quỹ
        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.AccountingEntry_District_AllocateFundReplaceManage, (int)Constant.Function.AccountingEntry_Branch_AllocateFundReplaceManage })]
        public ActionResult AllocateFundReplaceManage()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AllocateFundReplaceGet(int id = 0, int poId = 0, string dateRange = "", int budgetTypeId = 0, int cashFllowId = 0, int refType = 0, int PageIndex = 1)
        {
            string fromDate = "";
            string toDate = "";

            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            else
            {
                fromDate= Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }

            List<AccountingEntryViewModel> ListAccountingEntry = new List<AccountingEntryViewModel>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchAccountingEntry?id={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&refTypeId={6}&PageIndex={7}&PageSize={8}&isReplace={9}", new object[] { 0, oEmployee.POID, fromDate, toDate, budgetTypeId, cashFllowId, refType, PageIndex, Common.Constant.PageSize, 1 }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    var oEntry = new AccountingEntryViewModel();

                    oEntry.Id = int.Parse("0" + dyn.Id.ToString());
                    oEntry.TransNumber = dyn.TransNumber;
                    oEntry.AmndEmpId = int.Parse("0" + dyn.AmndEmpId.ToString());
                    oEntry.PoId = int.Parse("0" + dyn.PoId.ToString());
                    oEntry.PoCode = dyn.PoCode;

                    //oEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                    //oEntry.SendPoCode = dyn.SendPoCode;
                    //oEntry.SendPoName = dyn.SendPoCode + " - " + dyn.SendPoName;
                    if (refType == (int)Constant.AccountingRefType.FundsReceive)
                    {
                        if (dyn.IsLvp != null && Convert.ToBoolean(dyn.IsLvp.ToString()))
                        {
                            oEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                            oEntry.SendPoCode = dyn.SendPoCode;
                            oEntry.SendPoName = "Ngân hàng Liên Việt";
                        }
                        else
                        {
                            oEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                            oEntry.SendPoCode = dyn.SendPoCode;
                            oEntry.SendPoName = dyn.SendPoCode + " - " + dyn.SendPoName;
                        }
                    }
                    else
                    {
                        oEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                        oEntry.SendPoCode = dyn.SendPoCode;
                        oEntry.SendPoName = dyn.SendPoCode + " - " + dyn.SendPoName;
                    }

                    //oEntry.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                    //oEntry.ReceivePoCode = dyn.ReceivePoCode;
                    //oEntry.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;
                    if (refType == (int)Constant.AccountingRefType.FundsSend)
                    {
                        if (dyn.IsLvp != null && Convert.ToBoolean(dyn.IsLvp.ToString()))
                        {
                            oEntry.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                            oEntry.ReceivePoCode = dyn.ReceivePoCode;
                            oEntry.ReceivePoName = "Ngân hàng Liên Việt";
                        }
                        else
                        {
                            oEntry.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                            oEntry.ReceivePoCode = dyn.ReceivePoCode;
                            oEntry.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;
                        }
                    }
                    else
                    {
                        oEntry.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                        oEntry.ReceivePoCode = dyn.ReceivePoCode;
                        oEntry.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;
                    }

                    oEntry.RefType = int.Parse("0" + dyn.RefType.ToString());
                    oEntry.TransDate = dyn.TransDate;
                    oEntry.RefTransNumber = dyn.RefTransNumber;
                    oEntry.AmountVnd = Convert.ToDecimal(dyn.AmountVnd);
                    oEntry.AmountUsd = Convert.ToDecimal(dyn.AmountUsd);
                    oEntry.BudgetTypeId = int.Parse("0" + dyn.BudgetTypeId.ToString());
                    oEntry.CashFllowId = int.Parse("0" + dyn.CashFllowId.ToString());
                    oEntry.Description = dyn.Description;
                    oEntry.CurrencyType = dyn.CurrencyType;
                    oEntry.AmountUnitVnd = Convert.ToDecimal(dyn.AmountUnitVnd);
                    oEntry.AmountUnitUsd = Convert.ToDecimal(dyn.AmountUnitUsd);
                    oEntry.CurrencyTypeUnit = dyn.CurrencyTypeUnit;
                    oEntry.AmountSavingVnd = Convert.ToDecimal(dyn.AmountSavingVnd);
                    oEntry.AmountSavingUsd = Convert.ToDecimal(dyn.AmountSavingUsd);
                    oEntry.CurrencyTypeSaving = dyn.CurrencyTypeSaving;
                    oEntry.AmountBSVnd = Convert.ToDecimal(dyn.AmountBSVnd);
                    oEntry.AmountBSUsd = Convert.ToDecimal(dyn.AmountBSUsd);
                    oEntry.CurrencyTypeBS = dyn.CurrencyTypeBS;
                    oEntry.CreatedPoCode = dyn.CreatedPoCode;

                    if (!ListAccountingEntry.Contains(oEntry))
                        ListAccountingEntry.Add(oEntry);
                }

            }

            ViewBag.RefType = refType;

            return PartialView(ListAccountingEntry);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AllocateFundReplaceDetail(int Id, int refType)
        {
            AccountingEntryViewModel oAccountingEntry = new AccountingEntryViewModel();
            List<SelectListItem> ListPo = new List<SelectListItem>();
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];

            var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchAccountingEntry?id={0}&poId={1}&fromDate={2}&toDate={3}&budgetTypeId={4}&cashFllowId={5}&refTypeId={6}&PageIndex={7}&PageSize={8}&isReplace={9}", new object[] { Id, oEmployee.POID, "", "", 0, 0, refType, 1, 1, 1 }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    oAccountingEntry.Id = int.Parse("0" + dyn.Id.ToString());
                    oAccountingEntry.TransNumber = dyn.TransNumber;
                    oAccountingEntry.AmndEmpId = int.Parse("0" + dyn.AmndEmpId.ToString());
                    oAccountingEntry.PoId = int.Parse("0" + dyn.PoId.ToString());
                    oAccountingEntry.PoCode = dyn.PoCode;

                    //oAccountingEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                    //oAccountingEntry.SendPoCode = dyn.SendPoCode;
                    //oAccountingEntry.SendPoName = dyn.SendPoCode + " - " + dyn.SendPoName;
                    //oAccountingEntry.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                    //oAccountingEntry.ReceivePoCode = dyn.ReceivePoCode;
                    //oAccountingEntry.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;
                    if (refType == (int)Constant.AccountingRefType.FundsReceive)
                    {
                        if (dyn.IsLvp != null && Convert.ToBoolean(dyn.IsLvp.ToString()))
                        {
                            oAccountingEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                            oAccountingEntry.SendPoCode = dyn.SendPoCode;
                            oAccountingEntry.SendPoName = "Ngân hàng Liên Việt";
                        }
                        else
                        {
                            oAccountingEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                            oAccountingEntry.SendPoCode = dyn.SendPoCode;
                            oAccountingEntry.SendPoName = dyn.SendPoCode + " - " + dyn.SendPoName;
                        }
                    }
                    else
                    {
                        oAccountingEntry.SendPoId = int.Parse("0" + dyn.SendPoId.ToString());
                        oAccountingEntry.SendPoCode = dyn.SendPoCode;
                        oAccountingEntry.SendPoName = dyn.SendPoCode + " - " + dyn.SendPoName;
                    }

                    if (refType == (int)Constant.AccountingRefType.FundsSend)
                    {
                        if (dyn.IsLvp != null && Convert.ToBoolean(dyn.IsLvp.ToString()))
                        {
                            oAccountingEntry.SendPoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                            oAccountingEntry.SendPoCode = dyn.ReceivePoCode;
                            oAccountingEntry.SendPoName = "Ngân hàng Liên Việt";
                        }
                        else
                        {
                            oAccountingEntry.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                            oAccountingEntry.ReceivePoCode = dyn.ReceivePoCode;
                            oAccountingEntry.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;
                        }
                    }
                    else
                    {
                        oAccountingEntry.ReceivePoId = int.Parse("0" + dyn.ReceivePoId.ToString());
                        oAccountingEntry.ReceivePoCode = dyn.ReceivePoCode;
                        oAccountingEntry.ReceivePoName = dyn.ReceivePoCode + " - " + dyn.ReceivePoName;
                    }
                    oAccountingEntry.RefType = int.Parse("0" + dyn.RefType.ToString());
                    oAccountingEntry.TransDate = dyn.TransDate;
                    oAccountingEntry.RefTransNumber = dyn.RefTransNumber;
                    oAccountingEntry.OrdinalNumberString = dyn.OrdinalNumber.ToString();
                    if (dyn.CurrencyType == "VND")
                        oAccountingEntry.Amount = dyn.AmountVnd.ToString();
                    else
                        oAccountingEntry.Amount = dyn.AmountUsd.ToString();
                    oAccountingEntry.Description = dyn.Description;
                    oAccountingEntry.CurrencyType = dyn.CurrencyType;
                    oAccountingEntry.CurrencyTypeUnit = dyn.CurrencyTypeUnit;
                    if (dyn.CurrencyTypeUnit == "VND")
                        oAccountingEntry.AmountUnitString = dyn.AmountUnitVnd.ToString();
                    else
                        oAccountingEntry.AmountUnitString = dyn.AmountUnitUsd.ToString();
                    oAccountingEntry.CurrencyTypeSaving = dyn.CurrencyTypeSaving;
                    if (dyn.CurrencyTypeSaving == "VND")
                        oAccountingEntry.AmountSavingString = dyn.AmountSavingVnd.ToString();
                    else
                        oAccountingEntry.AmountSavingString = dyn.AmountSavingUsd.ToString();
                    oAccountingEntry.CurrencyTypeBS = dyn.CurrencyTypeBS;
                    if (dyn.CurrencyTypeBS == "VND")
                        oAccountingEntry.AmountBSString = dyn.AmountBSVnd.ToString();
                    else
                        oAccountingEntry.AmountBSString = dyn.AmountBSUsd.ToString();
                    break;
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

            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListCashFlow = ListCashFlow;
            ViewBag.ListCurrencyType = ListCurrencyType;

            return PartialView(oAccountingEntry);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AllocateFundReplaceCreate(int RefType)
        {
            AccountingEntryViewModel oAccounting = new AccountingEntryViewModel();

            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];

            //var rs = Helper.Invoke(Common.Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchOrdinalNumber?PoId={0}&RefType={1}&Month={2}", oEmployee.POID, RefType, DateTime.Now.Month), null);

            //if (rs.Code == "00")
            //{
            //    oAccounting.OrdinalNumberString = rs.Value.ToString();
            //}
            //else
            //{
            //    oAccounting.OrdinalNumberString = "1";
            //}
            oAccounting.OrdinalNumberString = "1";
            oAccounting.TransDate = Session[Constant.TIMEWORK_SESSION].ToString();

            oAccounting.Amount = "0";
            oAccounting.AmountUnitString = "0";
            oAccounting.AmountSavingString = "0";
            oAccounting.AmountBSString = "0";
            oAccounting.RefType = RefType;

            List<SelectListItem> ListPo = new List<SelectListItem>();
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
            PostOfficeViewModel po = Common.Repository.GetPOByID(oEmployee.POID);

            #region ListPo
            if (po != null)
            {
                ListPo.Add(new SelectListItem
                {
                    Text = po.Code + " - " + po.Name,
                    Value = po.ID.ToString()
                });
            }
            if (RefType == (int)Constant.AccountingRefType.FundsSend || RefType == (int)Constant.AccountingRefType.FundsReceive)
            {
                po = Common.Repository.GetPOByID(po.ParentID);
                if (po != null)
                {
                    ListPo.Add(new SelectListItem
                    {
                        Text = po.Code + " - " + po.Name,
                        Value = po.ID.ToString()
                    });
                }
            }

            #endregion

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

            ViewBag.ListPo = ListPo;
            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListCashFlow = ListCashFlow;
            ViewBag.ListCurrencyType = ListCurrencyType;
            return PartialView(oAccounting);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult AllocateFundReplaceUpdate(AccountingEntryViewModel oAccounting)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                if (oAccounting.PoIdTemp1 > 0)
                {
                    if (oAccounting.PoIdTemp2 > 0 || oAccounting.IsLvp)
                    {
                        EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                        int sendPoId = 0;
                        int receivePoId = 0;
                        if (oAccounting.RefType == (int)Constant.AccountingRefType.FundsSend || oAccounting.RefType == (int)Constant.AccountingRefType.SendTGNH)
                        {
                            sendPoId = oAccounting.PoIdTemp1;
                            receivePoId = oAccounting.PoIdTemp2;
                        }
                        else
                        {
                            sendPoId = oAccounting.PoIdTemp2;
                            receivePoId = oAccounting.PoIdTemp1;
                        }

                        //sendPoId = oAccounting.PoIdTemp1;
                        //receivePoId = oAccounting.PoIdTemp2;
                        var _Accounting = new
                        {
                            Id = oAccounting.Id,
                            TransNumber = oAccounting.TransNumber,
                            AmndEmpId = oEmployee.ID,
                            CreatedPoId = oEmployee.POID,
                            PoId = oAccounting.PoIdTemp1,
                            SendPoId = sendPoId,
                            ReceivePoId = receivePoId,
                            RefType = oAccounting.RefType,
                            TransDate = new DateTime(int.Parse(oAccounting.TransDate.Split('/')[2]), int.Parse(oAccounting.TransDate.Split('/')[1]), int.Parse(oAccounting.TransDate.Split('/')[0])).ToString("dd-MM-yyyy"),
                            RefTransNumber = oAccounting.RefTransNumber,
                            AmountVnd = oAccounting.CurrencyType == Constant.CurrencyType.VND.ToString() ? oAccounting.Amount.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                            AmountUsd = oAccounting.CurrencyType == Constant.CurrencyType.USD.ToString() ? oAccounting.Amount.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                            BudgetTypeId = oAccounting.BudgetTypeId,
                            CashFllowId = oAccounting.CashFllowId,
                            Description = oAccounting.Description,
                            CurrencyType = oAccounting.CurrencyType,
                            OrdinalNumber = int.Parse(oAccounting.OrdinalNumberString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator)),
                            AmountUnitVnd = oAccounting.CurrencyType == Constant.CurrencyType.VND.ToString() ? oAccounting.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                            AmountUnitUsd = oAccounting.CurrencyType == Constant.CurrencyType.USD.ToString() ? oAccounting.AmountUnitString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                            CurrencyTypeUnit = oAccounting.CurrencyTypeUnit,
                            AmountSavingVnd = oAccounting.CurrencyTypeSaving == Constant.CurrencyType.VND.ToString() ? oAccounting.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                            AmountSavingUsd = oAccounting.CurrencyTypeSaving == Constant.CurrencyType.USD.ToString() ? oAccounting.AmountSavingString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                            CurrencyTypeSaving = oAccounting.CurrencyTypeSaving,
                            AmountBSVnd = oAccounting.CurrencyTypeBS == Constant.CurrencyType.VND.ToString() ? oAccounting.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                            AmountBSUsd = oAccounting.CurrencyTypeBS == Constant.CurrencyType.USD.ToString() ? oAccounting.AmountBSString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator) : "0",
                            CurrencyTypeBS = oAccounting.CurrencyTypeBS,
                            IsLvp = oAccounting.IsLvp
                        };

                        var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/AddAccountingEntry", _Accounting);

                        string Code = rs.Code.ToString();
                        string Mes = rs.Message.ToString();
                        string Value = "";
                        if (rs.Value != null)
                            Value = rs.Value.ToString();

                        jResult = Json(new { Code = Code, Mes = Mes, Value = Value }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jResult = Json(new { Code = "-96", Mes = "Bạn chưa chọn đơn vị cần thực hiện bút toán!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    jResult = Json(new { Code = "-97", Mes = "Bạn chưa chọn đơn vị cần nhập thay thế!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }


            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult AllocateFundReplaceDelete()
        {
            JsonResult jResult = new JsonResult();

            return jResult;
        }
        #endregion

        #region Nộp Ngân Hàng
        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.AccountingEntry_District_PayBankReplaceManage, (int)Constant.Function.AccountingEntry_Branch_PayBankReplaceManage })]
        public ActionResult PayBankReplaceManage()
        {
            return View();
        }
        #endregion

        #region Hạn mức lưu quỹ tiền mặt
        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.BusinessSupport_District_FundLimitsManage, (int)Constant.Function.BusinessSupport_Branch_FundLimitsManage })]
        public ActionResult FundLimitsManage()
        {
            List<SelectListItem> ListNam = new List<SelectListItem>();

            for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 1; i++)
            {
                if (DateTime.Now.Year == i)
                {
                    ListNam.Add(new SelectListItem
                    {
                        Text = i.ToString(),
                        Value = i.ToString(),
                        Selected = true
                    });
                }
                else
                {
                    ListNam.Add(new SelectListItem
                    {
                        Text = i.ToString(),
                        Value = i.ToString()
                    });
                }

            }

            ViewBag.ListNam = ListNam;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult FundLimitsGet(int Nam = 0)
        {
            List<FundLimitsViewModel> ListFundLimits = new List<FundLimitsViewModel>();
            EmployeeViewModel oEmployee = UserCurrent();

            var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchFundLimits?PoId={0}&Thang={1}&Nam={2}&IsChild={3}", new object[] { oEmployee.POID, 0, Nam <= 0 ? DateTime.Now.Year : Nam, 1 }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    FundLimitsViewModel oFundLimits = new FundLimitsViewModel();
                    oFundLimits.Id = int.Parse("0" + dyn.Id);
                    oFundLimits.PoId = int.Parse("0" + dyn.PoId);
                    oFundLimits.DinhMucDuocGiao = long.Parse("0" + dyn.DinhMucDuocGiao);
                    oFundLimits.K1 = long.Parse("0" + dyn.K1);
                    oFundLimits.K2 = long.Parse("0" + dyn.K2);
                    oFundLimits.K3 = long.Parse("0" + dyn.K3);
                    oFundLimits.ThuTB = long.Parse("0" + dyn.ThuTB);
                    oFundLimits.ChiTB = long.Parse("0" + dyn.ChiTB);
                    oFundLimits.Thang = int.Parse("0" + dyn.Thang);
                    oFundLimits.Nam = int.Parse("0" + dyn.Nam);
                    oFundLimits.DinhMucDeXuat = long.Parse("0" + dyn.DinhMucDeXuat);
                    oFundLimits.PoCode = dyn.PoCode.ToString();
                    oFundLimits.PoName = dyn.PoName.ToString();

                    ListFundLimits.Add(oFundLimits);
                }
            }
            else
            {
                ListFundLimits = null;
            }


            var res = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchFundLimits?PoId={0}&Thang={1}&Nam={2}&IsChild={3}", new object[] { oEmployee.POID, 0, Nam <= 0 ? DateTime.Now.Year : Nam, 0 }), null);
            if (res != null && res.ListValue != null)
            {
                foreach (dynamic dyn in res.ListValue)
                {
                    FundLimitsViewModel oFundLimits = new FundLimitsViewModel();
                    oFundLimits.Id = int.Parse("0" + dyn.Id);
                    oFundLimits.PoId = int.Parse("0" + dyn.PoId);
                    oFundLimits.DinhMucDuocGiao = long.Parse("0" + dyn.DinhMucDuocGiao ?? "0");
                    oFundLimits.DinhMucDuocGiaoString = (dyn.DinhMucDuocGiao ?? "0");
                    oFundLimits.K1String = (dyn.K1 ?? "0");
                    oFundLimits.K2String = (dyn.K2 ?? "0");
                    oFundLimits.K3String = (dyn.K3 ?? "0");
                    oFundLimits.ThuTBString = (dyn.ThuTB ?? "0");
                    oFundLimits.ChiTBString = (dyn.ChiTB ?? "0");
                    oFundLimits.Thang = int.Parse("0" + dyn.Thang);
                    oFundLimits.Nam = int.Parse("0" + dyn.Nam);
                    oFundLimits.DinhMucDeXuatString = (dyn.DinhMucDeXuat ?? "0");
                    oFundLimits.PoCode = dyn.PoCode.ToString();
                    oFundLimits.PoName = dyn.PoName.ToString();

                    ListFundLimits.Add(oFundLimits);
                    break;
                }
            }




            return PartialView(ListFundLimits);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult FundLimitsDetail(int PoId, int Nam, int Thang = 0)
        {
            FundLimitsViewModel oFundLimits = new FundLimitsViewModel();
            var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchFundLimits?PoId={0}&Thang={1}&Nam={2}&IsChild={3}", new object[] { PoId, Thang, Nam <= 0 ? DateTime.Now.Year : Nam, 0 }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    oFundLimits.Id = int.Parse("0" + dyn.Id);
                    oFundLimits.PoId = int.Parse("0" + dyn.PoId);
                    oFundLimits.DinhMucDuocGiaoString = (dyn.DinhMucDuocGiao ?? "0");
                    oFundLimits.K1String = (dyn.K1 ?? "0");
                    oFundLimits.K2String = (dyn.K2 ?? "0");
                    oFundLimits.K3String = (dyn.K3 ?? "0");
                    oFundLimits.ThuTBString = (dyn.ThuTB ?? "0");
                    oFundLimits.ChiTBString = (dyn.ChiTB ?? "0");
                    oFundLimits.Thang = int.Parse("0" + dyn.Thang);
                    oFundLimits.Nam = int.Parse("0" + dyn.Nam);
                    oFundLimits.DinhMucDeXuatString = (dyn.DinhMucDeXuat ?? "0");
                    oFundLimits.PoCode = dyn.PoCode.ToString();
                    oFundLimits.PoName = dyn.PoName.ToString();
                    break;
                }
            }
            else
            {
                oFundLimits = new FundLimitsViewModel();
            }
            return PartialView(oFundLimits);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult FundLimitsUpdate(FundLimitsViewModel oItem)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                long DinhMucDuocGiao = 0;
                long K1 = 0;
                long K2 = 0;
                long K3 = 0;
                long ThuTB = 0;
                long ChiTB = 0;
                long DinhMucDeXuat = 0;

                long.TryParse("0" + oItem.DinhMucDuocGiaoString.Replace(",", "").Replace(".", ""), out DinhMucDuocGiao);
                long.TryParse("0" + oItem.K1String.Replace(",", "").Replace(".", ""), out K1);
                long.TryParse("0" + oItem.K2String.Replace(",", "").Replace(".", ""), out K2);
                long.TryParse("0" + oItem.K3String.Replace(",", "").Replace(".", ""), out K3);
                long.TryParse("0" + oItem.ThuTBString.Replace(",", "").Replace(".", ""), out ThuTB);
                long.TryParse("0" + oItem.ChiTBString.Replace(",", "").Replace(".", ""), out ChiTB);
                long.TryParse("0" + oItem.DinhMucDeXuatString.Replace(",", "").Replace(".", ""), out DinhMucDeXuat);

                oItem.DinhMucDuocGiao = DinhMucDuocGiao;
                oItem.K1 = K1;
                oItem.K2 = K2;
                oItem.K3 = K3;
                oItem.ThuTB = ThuTB;
                oItem.ChiTB = ChiTB;
                oItem.DinhMucDeXuat = DinhMucDeXuat;

                if (ModelState.IsValid)
                {
                    var rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/AccountingEntry/AddFundLimits", oItem);
                    string Code = rs.Code;
                    string Mes = rs.Message;

                    jResult = Json(new { Code = Code, Mes = Mes }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var message = (from state in ModelState.Values
                                   from error in state.Errors
                                   select error.ErrorMessage).Take(1);

                    jResult = Json(new { Code = "-99", Mes = message }, JsonRequestBehavior.AllowGet);
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