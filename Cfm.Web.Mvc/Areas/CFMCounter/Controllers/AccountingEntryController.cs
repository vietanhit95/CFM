using Cfm.Web.Mvc.Areas.Admin.Controllers;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cfm.Web.Lib;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter })]
    public class AccountingEntryController : BaseController
    {
        // GET: CFMCounter/Business
        public ActionResult ViewTest()
        {
            return PartialView();
        }
        public ActionResult Index()
        {
            return View();
        }

        #region Tiếp nộp quỹ AllocateFund
        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.FuntionPO_AllocateFundManage })]
        public ActionResult AllocateFundManage()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AllocateFundGet(int id = 0, int poId = 0, string dateRange = "", int budgetTypeId = 0, int cashFllowId = 0, int refType = 0, int PageIndex = 1)
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            if (po.POLevel == (int)Constant.POLevel.District || po.POLevel == (int)Constant.POLevel.Branch)
            {
                return RedirectToAction("AllocateFundGet", "AccountingEntry", new { area = "CFMDistrict", id = id, poId = poId, dateRange = dateRange, budgetTypeId = budgetTypeId, cashFllowId = cashFllowId, refType = refType, PageIndex = PageIndex });
            }
            string fromDate = "";
            string toDate = "";
            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
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
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AllocateFundDetail(int Id, int refType)
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            if (po.POLevel == (int)Constant.POLevel.District || po.POLevel == (int)Constant.POLevel.Branch)
            {
                return RedirectToAction("AllocateFundDetail", "AccountingEntry", new { area = "CFMDistrict", Id = Id, refType = refType });
            }
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
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AllocateFundCreate(int RefType)
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            if (po.POLevel == (int)Constant.POLevel.District || po.POLevel == (int)Constant.POLevel.Branch)
            {
                return RedirectToAction("AllocateFundCreate", "AccountingEntry", new { area = "CFMDistrict", RefType = RefType });
            }
            AccountingEntryViewModel oAccounting = new AccountingEntryViewModel();

            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];

            var rs = Helper.Invoke(Common.Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchOrdinalNumber?PoId={0}&RefType={1}&Month={2}", oEmployee.POID, RefType, DateTime.Now.Month), null);

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
            oAccounting.IsLvp = false;
            List<SelectListItem> ListPo = new List<SelectListItem>();
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();



            //#region ListPo
            //PostOfficeViewModel poFund = Common.Repository.GetPOByID(po.ParentID);
            //if (poFund != null)
            //{
            //    ListPo.Add(new SelectListItem
            //    {
            //        Text = poFund.Code + " - " + poFund.Name,
            //        Value = poFund.ID.ToString()
            //    });
            //}

            //poFund = Common.Repository.GetPOByID(poFund.ParentID);
            //if (poFund != null)
            //{
            //    ListPo.Add(new SelectListItem
            //    {
            //        Text = poFund.Code + " - " + poFund.Name,
            //        Value = poFund.ID.ToString()
            //    });
            //}
            //#endregion

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
        public JsonResult AllocateFundUpdate(AccountingEntryViewModel oAccounting)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                if (oAccounting.PoIdTemp1 > 0 || oAccounting.IsLvp)
                {
                    if (Convert.ToDateTime(oAccounting.TransDate, new System.Globalization.CultureInfo("fr-FR")) <= Convert.ToDateTime(Session[Constant.TIMEWORK_SESSION].ToString(), new System.Globalization.CultureInfo("fr-FR")))
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
                        jResult = Json(new { Code = "-95", Mes = "Ngày giao dịch [" + oAccounting.TransDate + "] không thể lớn hơn ngày Làm việc [" + Session[Constant.TIMEWORK_SESSION].ToString() + "]." }, JsonRequestBehavior.AllowGet);
                    }
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
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH })]
        public ActionResult PayBankManage()
        {
            return View();
        }
        #endregion

        #region Vay trả quỹ khác

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.FuntionPO_BorrowFundManage })]
        public ActionResult BorrowFundManage()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult BorrowFundGet(int id = 0, int poId = 0, string dateRange = "", int budgetTypeId = 0, int cashFllowId = 0, int refType = 0, int PageIndex = 1)
        {

            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            if (po.POLevel == (int)Constant.POLevel.District || po.POLevel == (int)Constant.POLevel.Branch)
            {
                return RedirectToAction("BorrowFundGet", "AccountingEntry", new { area = "CFMDistrict", id = id, poId = oEmployee.POID, dateRange = dateRange, budgetTypeId = budgetTypeId, cashFllowId = cashFllowId, refType = refType, PageIndex = PageIndex });
            }

            int Total = 0;
            string fromDate = string.Empty;
            string toDate = string.Empty;
            
            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
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
                        //BudgetTypeName = Repository.ListBudgetType[Convert.ToInt32(dyn.BudgetTypeId)],
                        AmountUnitVnd = decimal.Parse("0" + dyn.AmountUnitVnd.ToString()),
                        AmountUnitUsd = decimal.Parse("0" + dyn.AmountUnitUsd.ToString()),
                        CurrencyTypeUnit = dyn.CurrencyTypeUnit,
                        AmountSavingVnd = decimal.Parse("0" + dyn.AmountSavingVnd.ToString()),
                        AmountSavingUsd = decimal.Parse("0" + dyn.AmountSavingUsd.ToString()),
                        CurrencyTypeSaving = dyn.CurrencyTypeSaving,
                        AmountBSVnd = decimal.Parse("0" + dyn.AmountBSVnd.ToString()),
                        AmountBSUsd = decimal.Parse("0" + dyn.AmountBSUsd.ToString()),
                        CurrencyTypeBS = dyn.CurrencyTypeBS,
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
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult BorrowFundCreate(int id = 0, int refType = 0)
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            if (po.POLevel == (int)Constant.POLevel.District || po.POLevel == (int)Constant.POLevel.Branch)
            {
                return RedirectToAction("BorrowFundCreate", "AccountingEntry", new { area = "CFMDistrict", id = id, refType = refType });
            }
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
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

            oEntry.Amount = "0";
            oEntry.AmountUnitString = "0";
            oEntry.AmountSavingString = "0";
            oEntry.AmountBSString = "0";
            oEntry.RefType = oEntry.RefType;
            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListCashFlow = ListCashFlow;
            ViewBag.ListCurrencyType = ListCurrencyType;
            ViewBag.refType = refType;
            return PartialView(oEntry);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult BorrowFundUpdate(AccountingEntryViewModel oEntry)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                if (Convert.ToDateTime(oEntry.TransDate, new System.Globalization.CultureInfo("fr-FR")) <= Convert.ToDateTime(Session[Constant.TIMEWORK_SESSION].ToString(), new System.Globalization.CultureInfo("fr-FR")))
                {
                    EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                    decimal dAmountVnd = 0;
                    decimal dAmountUsd = 0;
                    if (oEntry.CurrencyType == Constant.CurrencyType.VND.ToString())
                    {
                        dAmountVnd = decimal.Parse(oEntry.Amount.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
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
                    jResult = Json(new { Code = "-95", Mes = "Ngày giao dịch [" + oEntry.TransDate + "] không thể lớn hơn ngày Làm việc [" + Session[Constant.TIMEWORK_SESSION].ToString() + "]." }, JsonRequestBehavior.AllowGet);
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
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult PaymentBorrowFundGet(int id = 0, int poId = 0, string dateRange = "", int budgetTypeId = 0, int cashFllowId = 0, int refType = 0, int PageIndex = 1)
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            if (po.POLevel == (int)Constant.POLevel.District || po.POLevel == (int)Constant.POLevel.Branch)
            {
                return RedirectToAction("PaymentBorrowFundGet", "AccountingEntry", new { area = "CFMDistrict", id = id, poId = poId, dateRange = dateRange, budgetTypeId = budgetTypeId, cashFllowId = cashFllowId, refType = refType, PageIndex });
            }

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
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult PaymentBorrowFundCreate(int id = 0, int refType = 0)
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            if (po.POLevel == (int)Constant.POLevel.District || po.POLevel == (int)Constant.POLevel.Branch)
            {
                return RedirectToAction("PaymentBorrowFundCreate", "AccountingEntry", new { area = "CFMDistrict", id = id, refType = refType });
            }
            List<SelectListItem> ListBudgetType = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
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
            oEntry.Amount = "0";
            oEntry.AmountUnitString = "0";
            oEntry.AmountSavingString = "0";
            oEntry.AmountBSString = "0";
            ViewBag.ListBudgetType = ListBudgetType;
            ViewBag.ListCashFlow = ListCashFlow;
            ViewBag.ListCurrencyType = ListCurrencyType;
            ViewBag.refType = refType;
            return PartialView(oEntry);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult PaymentBorrowFundUpdate(AccountingEntryViewModel oEntry)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                if (!string.IsNullOrEmpty(oEntry.RefTransNumber))
                {
                    PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
                    if (po.POLevel == (int)Constant.POLevel.District)
                    {
                        using (CFMDistrict.Controllers.AccountingEntryController district = new CFMDistrict.Controllers.AccountingEntryController())
                        {
                            return district.PaymentBorrowFundUpdate(oEntry);
                        }
                    }

                    EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                    decimal dAmountVnd = 0;
                    decimal dAmountUsd = 0;
                    if (oEntry.CurrencyType == Constant.CurrencyType.VND.ToString())
                    {
                        dAmountVnd = decimal.Parse(oEntry.Amount.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
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
                PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
                if (po.POLevel == (int)Constant.POLevel.District)
                {
                    using (CFMDistrict.Controllers.AccountingEntryController district = new CFMDistrict.Controllers.AccountingEntryController())
                    {
                        return district.PaymentBorrowFundDelete(AccountEntryId);
                    }
                }
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
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult BorrowSelect(int refType = 0, int poId = 0, int budgetTypeID = 0, int cashFllowId = 0, string dateRange = "", int PageIndex = 1)
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];

            int _PoID = po.ID;
            if (poId > 0)
            {
                var posChild = Repository.GetPOByID(poId);

                if (posChild != null && posChild.ParentID == po.ID)
                {
                    _PoID = posChild.ID;
                }
                else
                {
                    return null;
                }
            }

            if (po.POLevel == (int)Constant.POLevel.District || po.POLevel == (int)Constant.POLevel.Branch)
            {
                return RedirectToAction("BorrowSelect", "AccountingEntry", new { area = "CFMDistrict", refType = refType, poId = _PoID, budgetTypeID = budgetTypeID, cashFllowId = cashFllowId, dateRange = dateRange, PageIndex = PageIndex });
            }
            int Total = 0;
            string fromDate = string.Empty;
            string toDate = string.Empty;

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
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult BorrowSelectGet(int refType = 0, int poId = 0, int budgetTypeID = 0, int cashFllowId = 0, string dateRange = "", int PageIndex = 1)
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            int _PoID = po.ID;
            if (poId > 0)
            {
                var posChild = Repository.GetPOByID(poId);

                if (posChild != null && posChild.ParentID == po.ID)
                {
                    _PoID = posChild.ID;
                }
                else
                {
                    return null;
                }
            }

            if (po.POLevel == (int)Constant.POLevel.District || po.POLevel == (int)Constant.POLevel.Branch)
            {
                return RedirectToAction("BorrowSelectGet", "AccountingEntry", new { area = "CFMDistrict", refType = refType, poId = _PoID, budgetTypeID = budgetTypeID, cashFllowId = cashFllowId, dateRange = dateRange, PageIndex = PageIndex });
            }
            int Total = 0;
            string fromDate = string.Empty;
            string toDate = string.Empty;

            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
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

        #region Thông số quỹ đầu tháng
        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.System_MonthlyFundInfo, (int)Constant.Function.System_District_MonthlyFundInfo, (int)Constant.Function.System_Branch_MonthlyFundInfo })]
        public ActionResult MonthlyFundInfo(int PoId = 0)
        {
            List<SelectListItem> ListMonth = new List<SelectListItem>();
            List<SelectListItem> ListYear = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();


            MonthlyFundInfo oMonthlyFundInfo = new Models.ViewModel.MonthlyFundInfo();

            PostOfficeViewModel oPo = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            int _POID = oPo.ID;


            if (oPo.POLevel == (int)Constant.POLevel.District)
            {
                List<SelectListItem> ListPO = new List<SelectListItem>();
                ListPO.Add(new SelectListItem
                {
                    Text = oPo.Code + "-" + oPo.Name,
                    Value = oPo.ID.ToString()
                });
                var res = Helper.Invoke("GET", string.Format("api/PostOffice/GetListPOByParentID?parentID={0}", new object[] { oPo.ID }), null);

                if (res != null && res.Code == "00" && res.ListValue != null)
                {
                    foreach (dynamic dyn in res.ListValue)
                    {
                        ListPO.Add(new SelectListItem
                        {
                            Text = dyn.Code.ToString() + "-" + dyn.Name.ToString(),
                            Value = dyn.Id.ToString()
                        });
                    }
                }


                if (PoId == 0)
                {
                    PoId = int.Parse(ListPO[0].Value);
                }

                ViewBag.ListPO = ListPO;
            }
            else if (oPo.POLevel == (int)Constant.POLevel.Counter)
            {
                PoId = _POID;
            }


            if (PoId > 0)
            {
                var Po = Repository.GetPOByID(PoId);

                if (Po != null && (((Po.ParentID == oPo.ID || Po.ID == oPo.ID) && oPo.POLevel == (int)Constant.POLevel.District) || oPo.POLevel == (int)Constant.POLevel.Branch || oPo.POLevel == (int)Constant.POLevel.Counter))
                {
                    _POID = Po.ID;
                    oMonthlyFundInfo.PoId = _POID;
                    oMonthlyFundInfo.PoCode = Po.Code;
                    oMonthlyFundInfo.PoName = Po.Name;
                    oMonthlyFundInfo.PoLevel = Po.POLevel;
                }
                else
                {
                    return null;
                }
            }


            for (int i = 1; i <= 12; i++)
            {
                ListMonth.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            ListYear.Add(new SelectListItem
            {
                Text = DateTime.Now.Year.ToString(),
                Value = DateTime.Now.Year.ToString()
            });



            var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchMonthlyFundInfo?Month={0}&Year={1}&PoId={2}&CashFllowId={3}", new object[] { DateTime.Now.Month, DateTime.Now.Year, _POID, (int)Constant.CashFllowType.Center }), null);

            if (rs.Code == "00")
            {
                foreach (dynamic dyna in rs.ListValue)
                {
                    oMonthlyFundInfo.Id = Convert.ToInt32(dyna.Id);
                    oMonthlyFundInfo.Month = Convert.ToInt32(dyna.Month);
                    oMonthlyFundInfo.Year = Convert.ToInt32(dyna.Year);
                    oMonthlyFundInfo.StringAmountVND = dyna.AmountVND.ToString();
                    oMonthlyFundInfo.StringAmountUSD = dyna.AmountUSD.ToString();
                    oMonthlyFundInfo.StringAmountBankVND = dyna.AmountBankVND.ToString();
                    oMonthlyFundInfo.StringAmountBankUSD = dyna.AmountBankUSD.ToString();
                    oMonthlyFundInfo.StringAmountBorrowFund = dyna.AmountBorrowFund.ToString();
                    oMonthlyFundInfo.StringAmountLoanFund = dyna.AmountLoanFund.ToString();
                    oMonthlyFundInfo.StringAmountTransferVND = dyna.AmountTransferVND.ToString();
                    oMonthlyFundInfo.StringAmountTransferUSD = dyna.AmountTransferUSD.ToString();
                    oMonthlyFundInfo.PoId = Convert.ToInt32(dyna.PoId);
                    break;
                }
            }
            else
            {
                oMonthlyFundInfo.StringAmountVND = "0";
                oMonthlyFundInfo.StringAmountUSD = "0";
                oMonthlyFundInfo.StringAmountBankVND = "0";
                oMonthlyFundInfo.StringAmountBankUSD = "0";
                oMonthlyFundInfo.StringAmountBorrowFund = "0";
                oMonthlyFundInfo.StringAmountLoanFund = "0";
                oMonthlyFundInfo.StringAmountTransferVND = "0";
                oMonthlyFundInfo.StringAmountTransferUSD = "0";
                oMonthlyFundInfo.Month = DateTime.Now.Month;

            }

            #region ListCashFlow
            foreach (var item in Repository.ListCashFlow)
            {
                if (item.Key != (int)Constant.CashFllowType.General)
                {
                    ListCashFlow.Add(new SelectListItem
                    {
                        Text = item.Value,
                        Value = item.Key.ToString()
                    });
                }

            }
            #endregion

            ViewBag.ListMonth = ListMonth;
            ViewBag.ListYear = ListYear;
            ViewBag.ListCashFlow = ListCashFlow;

            return PartialView(oMonthlyFundInfo);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult MonthlyFundUpdate(MonthlyFundInfo oMonthlyFundInfo)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                if (oMonthlyFundInfo.PoId > 0)
                {
                    oMonthlyFundInfo.AmountVND = long.Parse("0" + oMonthlyFundInfo.StringAmountVND.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                    oMonthlyFundInfo.AmountUSD = long.Parse("0" + oMonthlyFundInfo.StringAmountUSD.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                    oMonthlyFundInfo.AmountBankVND = 0;
                    oMonthlyFundInfo.AmountBankUSD = 0;
                    oMonthlyFundInfo.AmountBorrowFund = 0;
                    oMonthlyFundInfo.AmountLoanFund = 0;
                    oMonthlyFundInfo.AmountTransferVND = 0;
                    oMonthlyFundInfo.AmountTransferUSD = 0;

                    EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                    PostOfficeViewModel oPo = (PostOfficeViewModel)Session[Constant.PO_SESSION];
                    int POID = 0;
                    bool bCheckPO = true;

                    #region CheckPO
                    if (oPo.POLevel == (int)Constant.POLevel.Counter)
                    {
                        POID = oEmployee.POID;
                    }
                    else if (oPo.POLevel == (int)Constant.POLevel.District)
                    {
                        if (oMonthlyFundInfo.PoId > 0)
                        {
                            var _Po = Repository.GetPOByID(oMonthlyFundInfo.PoId);

                            if (_Po.ParentID == oPo.ID || _Po.ID == oPo.ID)
                            {
                                POID = _Po.ID;
                            }

                            if (_Po.POLevel == (int)Constant.POLevel.District)
                            {
                                oMonthlyFundInfo.AmountBankVND = long.Parse("0" + oMonthlyFundInfo.StringAmountBankVND.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                                oMonthlyFundInfo.AmountBankUSD = long.Parse("0" + oMonthlyFundInfo.StringAmountBankUSD.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                                oMonthlyFundInfo.AmountBorrowFund = long.Parse("0" + oMonthlyFundInfo.StringAmountBorrowFund.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                                oMonthlyFundInfo.AmountLoanFund = long.Parse("0" + oMonthlyFundInfo.StringAmountLoanFund.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                                oMonthlyFundInfo.AmountTransferVND = long.Parse("0" + oMonthlyFundInfo.StringAmountTransferVND.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                                oMonthlyFundInfo.AmountTransferUSD = long.Parse("0" + oMonthlyFundInfo.StringAmountTransferUSD.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                            }
                        }
                        else
                        {
                            bCheckPO = false;
                        }
                    }
                    else if (oPo.POLevel == (int)Constant.POLevel.Branch)
                    {
                        oMonthlyFundInfo.AmountBankVND = long.Parse("0" + oMonthlyFundInfo.StringAmountBankVND.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                        oMonthlyFundInfo.AmountBankUSD = long.Parse("0" + oMonthlyFundInfo.StringAmountBankUSD.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                        oMonthlyFundInfo.AmountBorrowFund = long.Parse("0" + oMonthlyFundInfo.StringAmountBorrowFund.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                        oMonthlyFundInfo.AmountLoanFund = long.Parse("0" + oMonthlyFundInfo.StringAmountLoanFund.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                        oMonthlyFundInfo.AmountTransferVND = long.Parse("0" + oMonthlyFundInfo.StringAmountTransferVND.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                        oMonthlyFundInfo.AmountTransferUSD = long.Parse("0" + oMonthlyFundInfo.StringAmountTransferUSD.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                        POID = oMonthlyFundInfo.PoId;
                    }
                    else
                    {
                        bCheckPO = false;
                    }
                    #endregion

                    if (bCheckPO == true)
                    {

                        oMonthlyFundInfo.PoId = POID;
                        oMonthlyFundInfo.Year = DateTime.Now.Year;
                        oMonthlyFundInfo.AmndEmp = oEmployee.ID;

                        ResponseObject rs = new ResponseObject();
                        if (oMonthlyFundInfo.Id > 0)
                        {
                            rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/AccountingEntry/UpdateMonthlyFundInfo", oMonthlyFundInfo);
                        }
                        else
                        {
                            rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/AccountingEntry/AddMonthlyFundInfo", oMonthlyFundInfo);
                        }

                        string Code = rs.Code;
                        string Mes = rs.Message;

                        jResult = Json(new { Code = Code, Mes = Mes, Value = rs.Value }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jResult = Json(new { Code = "-98", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    jResult = Json(new { Code = "-101", Mes = "Bạn chưa chọn đơn vị cần thiết lập thông số quỹ!" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult MonthlyFundSearch(int Month, int CashFllowId, int PoId = 0)
        {
            JsonResult jResult = new JsonResult();
            PostOfficeViewModel oPo = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            int _PoId = oPo.ID;

            if (PoId > 0)
            {
                var _Po = Repository.GetPOByID(PoId);
                if (oPo.POLevel == (int)Constant.POLevel.District && _Po.ParentID == oPo.ID)
                {
                    _PoId = _Po.ID;
                }
                else if (oPo.POLevel == (int)Constant.POLevel.Branch)
                {
                    _PoId = PoId;
                }
            }

            MonthlyFundInfo oMonthlyFundInfo = new Models.ViewModel.MonthlyFundInfo();

            var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchMonthlyFundInfo?Month={0}&Year={1}&PoId={2}&CashFllowId={3}", new object[] { Month, DateTime.Now.Year, _PoId, CashFllowId }), null);

            if (rs.Code == "00")
            {
                foreach (dynamic dyna in rs.ListValue)
                {
                    oMonthlyFundInfo.Id = Convert.ToInt32(dyna.Id);
                    oMonthlyFundInfo.Month = Convert.ToInt32(dyna.Month);
                    oMonthlyFundInfo.Year = Convert.ToInt32(dyna.Year);
                    oMonthlyFundInfo.AmountVND = Convert.ToInt64(dyna.AmountVND);
                    oMonthlyFundInfo.AmountUSD = Convert.ToInt64(dyna.AmountUSD);
                    oMonthlyFundInfo.AmountBankVND = Convert.ToInt64(dyna.AmountBankVND);
                    oMonthlyFundInfo.AmountBankUSD = Convert.ToInt64(dyna.AmountBankUSD);
                    oMonthlyFundInfo.AmountBorrowFund = Convert.ToInt64(dyna.AmountBorrowFund);
                    oMonthlyFundInfo.AmountLoanFund = Convert.ToInt64(dyna.AmountLoanFund);
                    oMonthlyFundInfo.AmountTransferVND = Convert.ToInt64(dyna.AmountTransferVND);
                    oMonthlyFundInfo.AmountTransferUSD = Convert.ToInt64(dyna.AmountTransferUSD);
                    oMonthlyFundInfo.PoId = Convert.ToInt32(dyna.PoId);
                    break;
                }
            }
            else
            {
                oMonthlyFundInfo = null;
            }

            jResult = Json(new { data = oMonthlyFundInfo }, JsonRequestBehavior.AllowGet);

            return jResult;
        }
        #endregion

        #region Bút toán điều chỉnh
        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AdjustmentEntryGet(int id = 0, int poId = 0, string reportDate = "", int reportId = 0)
        {
            int Total = 0;

            List<AdjustmentEntryViewModel> ListAdjustmentEntry = new List<AdjustmentEntryViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/LoadAdjustmentEntry?id={0}&poId={1}&reportDate={2}&reportId={3}", new object[] { 0, poId, reportDate, reportId }), null);
            if (rs != null && rs.ListValue != null)
            {
                string sAdjTypeName = "";

                foreach (dynamic dyn in rs.ListValue)
                {

                    sAdjTypeName = Repository.ListAdjType[(int)dyn.AdjType].ToString();

                    var oEntry = new AdjustmentEntryViewModel()
                    {
                        Id = dyn.Id,
                        AmndEmpId = dyn.AmndEmpId,
                        AmndEmpName = dyn.AmndEmpName,
                        PoId = dyn.PoId,
                        PoCode = dyn.PoCode,
                        PoName = dyn.PoName,
                        CreatedDate = dyn.CreatedDate,
                        ReportDate = dyn.ReportDate,
                        ReportId = dyn.ReportId,
                        AdjType = dyn.AdjType,
                        AdjTypeName = sAdjTypeName,
                        AdjAmountVnd = dyn.AdjAmountVnd,
                        AdjAmountUsd = dyn.AdjAmountUsd,
                        ItemId = dyn.ItemId,
                        ItemCode = dyn.ItemCode,
                        ItemName = dyn.ItemName
                    };

                    if (!ListAdjustmentEntry.Contains(oEntry))
                        ListAdjustmentEntry.Add(oEntry);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.poId = poId;
            ViewBag.reportDate = reportDate;
            ViewBag.reportId = reportId;
            return PartialView(ListAdjustmentEntry);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AdjustmentEntryCreate(int reportId = 0, string reportDate = "", int PoId = 0)
        {
            List<SelectListItem> ListAdjType = new List<SelectListItem>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            int Total = 0;
            int poId = oEmployee.POID;
            AdjustmentEntryViewModel oEntry = new AdjustmentEntryViewModel();
            if (PoId > 0 && oEmployee.POID != PoId)
            {
                PostOfficeViewModel oPo = new PostOfficeViewModel();
                oPo = Repository.GetPOByID(PoId);
                if (oPo == null || oPo.ParentID != oEmployee.POID)
                {
                    return null;
                }
                poId = oPo.ID;
            }


            #region ListAdjType
            ListAdjType.Add(new SelectListItem
            {
                Text = "---Chọn loại bút toán---",
                Value = "0"
            });

            foreach (var item in Repository.ListAdjType)
            {
                ListAdjType.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString()
                });
            }
            #endregion

            #region Danh sách chỉ tiêu báo cáo

            List<ItemListViewModel> listItem = new List<ItemListViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchItemForCreateAdjEntry?reportId={0}&searchContent={1}&PageIndex={2}&PageSize={3}", new object[] { reportId, "", 1, Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    var oItem = new ItemListViewModel()
                    {
                        Id = dyn.Id,
                        Code = dyn.Code,
                        Name = dyn.Name
                    };

                    if (!listItem.Contains(oItem))
                        listItem.Add(oItem);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }

            #endregion
            oEntry.StringAmountVnd = "0";
            oEntry.StringAmountUsd = "0";
            oEntry.ReportId = reportId;
            oEntry.ItemId = 0;
            oEntry.ReportDate = reportDate;
            oEntry.ListItem = listItem;
            oEntry.PoId = poId;
            oEntry.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.reportId = reportId;
            ViewBag.reportDate = reportDate;
            ViewBag.ListAdjType = ListAdjType;
            ViewBag.poId = poId;
            ViewBag.Total = Total;
            ViewBag.PageIndex = 1;
            ViewBag.PageSize = Constant.PageSize;
            return PartialView(oEntry);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ItemListGet(int PageIndex = 1, string searchContent = "", int reportId = 0)
        {
            List<ItemListViewModel> ListItem = new List<ItemListViewModel>();
            int Total = 0;

            try
            {

                var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchItemForCreateAdjEntry?reportId={0}&searchContent={1}&PageIndex={2}&PageSize={3}", new object[] { reportId, searchContent, PageIndex, Constant.PageSize }), null);
                if (rs != null && rs.ListValue != null)
                {
                    foreach (dynamic dyn in rs.ListValue)
                    {
                        var oItem = new ItemListViewModel()
                        {
                            Id = dyn.Id,
                            Code = dyn.Code,
                            Name = dyn.Name
                        };

                        if (!ListItem.Contains(oItem))
                            ListItem.Add(oItem);
                    }

                    if (rs.Value != null)
                        int.TryParse(rs.Value.ToString(), out Total);
                }
            }
            catch
            {
                ListItem = null;
            }

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.reportId = reportId;
            return PartialView("ItemListGetForCreateAdjEntry", ListItem);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult AdjustmentEntryUpdate(AdjustmentEntryViewModel oEntry)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                PostOfficeViewModel oPo = (PostOfficeViewModel)Session[Constant.PO_SESSION];
                int PoID = oPo.ID;

                if (oPo.POLevel == (int)Constant.POLevel.District)
                {
                    PoID = oEntry.PoId;
                }

                decimal dAmountVnd = 0;
                if (oEntry.StringAmountVnd != null && oEntry.StringAmountVnd != "")
                    dAmountVnd = decimal.Parse(oEntry.StringAmountVnd.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                decimal dAmountUsd = 0;
                if (oEntry.StringAmountUsd != null && oEntry.StringAmountUsd != "")
                    dAmountUsd = decimal.Parse(oEntry.StringAmountUsd.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));

                var _Accounting = new
                {
                    Id = oEntry.Id,
                    AmndEmpId = oEntry.AmndEmpId,
                    PoId = PoID,
                    CreatedDate = oEntry.CreatedDate,
                    ReportDate = oEntry.ReportDate,
                    ReportId = oEntry.ReportId,
                    AdjType = oEntry.AdjType,
                    AdjAmountVnd = dAmountVnd,
                    AdjAmountUsd = dAmountUsd,
                    ItemId = oEntry.ItemId

                };

                var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/AddAdjustmentEntry", _Accounting);

                string Code = rs.Code.ToString();
                string Mes = rs.Message.ToString();
                string Value = rs.Value.ToString();

                jResult = Json(new { Code = Code, Mes = Mes, Value = Value }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        //[ValidateAntiForgeryToken]
        public JsonResult AdjustmentEntryDelete(int id, string reportDate)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                var oRequest = new
                {
                    Id = id,
                    PoId = oEmployee.POID,
                    ReportDate = reportDate
                };
                var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/DeleteAdjustmentEntry", oRequest);
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

        #endregion

        #region Thông số quỹ theo các dòng tiền
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult FundInfo()
        {
            List<SelectListItem> ListDay = new List<SelectListItem>();
            List<SelectListItem> ListMonth = new List<SelectListItem>();
            List<SelectListItem> ListYear = new List<SelectListItem>();
            List<SelectListItem> ListCashFlow = new List<SelectListItem>();

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

            int iDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            for (int i = 1; i <= iDay; i++)
            {
                ListDay.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            for (int i = 1; i <= 12; i++)
            {
                ListMonth.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            ListYear.Add(new SelectListItem
            {
                Text = DateTime.Now.Year.ToString(),
                Value = DateTime.Now.Year.ToString()
            });

            FundInfo oFundInfo = new Models.ViewModel.FundInfo();


            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchFundInfo?Day={0}&Month={1}&Year={2}&PoId={3}&Fund_Type={4}", new object[] { DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, oEmployee.POID, 1 }), null);

            if (rs.Code == "00")
            {
                foreach (dynamic dyna in rs.ListValue)
                {
                    oFundInfo.Id = Convert.ToInt32(dyna.Id);
                    oFundInfo.Fund_Day = Convert.ToInt32(dyna.Fund_Day);
                    oFundInfo.Fund_Month = Convert.ToInt32(dyna.Fund_Month);
                    oFundInfo.Fund_Year = Convert.ToInt32(dyna.Fund_Year);
                    oFundInfo.StringAmountVND = dyna.AmountVND.ToString();
                    oFundInfo.StringAmountUSD = dyna.AmountUSD.ToString();
                    oFundInfo.StringAmountBankVND = dyna.AmountBankVND.ToString();
                    oFundInfo.StringAmountBankUSD = dyna.AmountBankUSD.ToString();
                    oFundInfo.StringAmountBorrowVND = dyna.AmountBorrowVND.ToString();
                    oFundInfo.StringAmountLoanVND = dyna.AmountLoanVND.ToString();
                    oFundInfo.StringAmountTransferVND = dyna.AmountTransferVND.ToString();
                    oFundInfo.StringAmountTransferUSD = dyna.AmountTransferUSD.ToString();
                    oFundInfo.PoId = Convert.ToInt32(dyna.PoId);
                    break;
                }
            }
            else
            {
                oFundInfo.StringAmountVND = "0";
                oFundInfo.StringAmountUSD = "0";
                oFundInfo.StringAmountBankVND = "0";
                oFundInfo.StringAmountBankUSD = "0";
                oFundInfo.StringAmountBorrowVND = "0";
                oFundInfo.StringAmountLoanVND = "0";
                oFundInfo.StringAmountTransferVND = "0";
                oFundInfo.StringAmountTransferUSD = "0";
                oFundInfo.Fund_Month = DateTime.Now.Month;
                oFundInfo.Fund_Day = DateTime.Now.Day;
                oFundInfo.Fund_Year = DateTime.Now.Year;
            }

            ViewBag.ListDay = ListDay;
            ViewBag.ListMonth = ListMonth;
            ViewBag.ListYear = ListYear;
            ViewBag.ListCashFlow = ListCashFlow;
            return PartialView(oFundInfo);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult FundInfoUpdate(FundInfo oFundInfo)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                oFundInfo.AmountVND = long.Parse("0" + oFundInfo.StringAmountVND.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                oFundInfo.AmountUSD = long.Parse("0" + oFundInfo.StringAmountUSD.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                oFundInfo.AmountBankVND = long.Parse("0" + oFundInfo.StringAmountBankVND.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                oFundInfo.AmountBankUSD = long.Parse("0" + oFundInfo.StringAmountBankUSD.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                oFundInfo.AmountBorrowVND = long.Parse("0" + oFundInfo.StringAmountBorrowVND.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                oFundInfo.AmountLoanVND = long.Parse("0" + oFundInfo.StringAmountLoanVND.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                oFundInfo.AmountTransferVND = long.Parse("0" + oFundInfo.StringAmountTransferVND.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                oFundInfo.AmountTransferUSD = long.Parse("0" + oFundInfo.StringAmountTransferUSD.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));

                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                oFundInfo.PoId = oEmployee.POID;
                oFundInfo.Fund_Month = DateTime.Now.Month;
                oFundInfo.Fund_Day = DateTime.Now.Day;
                oFundInfo.Fund_Year = DateTime.Now.Year;
                oFundInfo.AmndEmp = oEmployee.ID;

                ResponseObject rs = new ResponseObject();
                if (oFundInfo.Id > 0)
                {
                    rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/AccountingEntry/UpdateFundInfo", oFundInfo);
                }
                else
                {
                    rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/AccountingEntry/AddFundInfo", oFundInfo);
                }

                string Code = rs.Code;
                string Mes = rs.Message;

                jResult = Json(new { Code = Code, Mes = Mes, Value = rs.Value }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult FundInfoSearch(int Day, int Month, int Fund_Type)
        {
            JsonResult jResult = new JsonResult();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            FundInfo oFundInfo = new Models.ViewModel.FundInfo();

            var rs = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/AccountingEntry/SearchFundInfo?Day={0}&Month={1}&Year={2}&PoId={3}&Fund_Type={4}", new object[] { Day, Month, DateTime.Now.Year, oEmployee.POID, Fund_Type }), null);

            if (rs.Code == "00")
            {
                foreach (dynamic dyna in rs.ListValue)
                {
                    oFundInfo.Id = Convert.ToInt32(dyna.Id);
                    oFundInfo.Fund_Day = Convert.ToInt32(dyna.Fund_Day);
                    oFundInfo.Fund_Month = Convert.ToInt32(dyna.Month);
                    oFundInfo.Fund_Year = Convert.ToInt32(dyna.Fund_Year);
                    oFundInfo.AmountVND = Convert.ToInt64(dyna.AmountVND);
                    oFundInfo.AmountUSD = Convert.ToInt64(dyna.AmountUSD);
                    oFundInfo.AmountBankVND = Convert.ToInt64(dyna.AmountBankVND);
                    oFundInfo.AmountBankUSD = Convert.ToInt64(dyna.AmountBankUSD);
                    oFundInfo.AmountBorrowVND = Convert.ToInt64(dyna.AmountBorrowVND);
                    oFundInfo.AmountLoanVND = Convert.ToInt64(dyna.AmountLoanVND);
                    oFundInfo.AmountTransferVND = Convert.ToInt64(dyna.AmountTransferVND);
                    oFundInfo.AmountTransferUSD = Convert.ToInt64(dyna.AmountTransferUSD);
                    oFundInfo.PoId = Convert.ToInt32(dyna.PoId);
                    break;
                }
            }
            else
            {
                oFundInfo = null;
            }

            jResult = Json(new { data = oFundInfo }, JsonRequestBehavior.AllowGet);

            return jResult;
        }
        #endregion

        #region Nhu cầu tiếp quỹ

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.FuntionPO_FundRequiredManage, (int)Constant.Function.BusinessSupport_District_FundRequired })]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult FundRequiredManage()
        {

            return View();
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult FundRequiredGet(int id = 0, int createdPoid = 0, int approvedPoId = 0, string dateRange = "", int PageIndex = 1)
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];

            int Total = 0;
            string fromDate = string.Empty;
            string toDate = string.Empty;

            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            List<FundRequiredViewmodel> ListFundRequired = new List<FundRequiredViewmodel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchFundRequired?id={0}&createdPoid={1}&approvedPoId={2}&fromDate={3}&toDate={4}&PageIndex={5}&PageSize={6}", new object[] { 0, oEmployee.POID, 0, fromDate, toDate, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    var oFund = new FundRequiredViewmodel()
                    {
                        Id = dyn.Id,
                        CreatedPoId = dyn.CreatedPoId,
                        CreatedDate = dyn.CreatedDate,
                        CreatedPoName = dyn.CreatedPoName,
                        StatusName = (dyn.Status == "L") ? "Chờ phê duyệt" : "Đã phê duyệt",
                        Status = dyn.Status,
                        CurrencyType = dyn.CurrencyType,
                        RecommentAmount = dyn.RecommentAmount,
                        RecommentAmountString = dyn.RecommentAmount.ToString(),
                        ApprovedAmount = dyn.ApprovedAmount,
                        ApprovedAmountString = dyn.ApprovedAmount.ToString(),
                        Description = dyn.Description
                    };

                    if (!ListFundRequired.Contains(oFund))
                        ListFundRequired.Add(oFund);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.refType = 1;

            return PartialView(ListFundRequired);

        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult FundRequiredApprovedGet(int id = 0, int createdPoid = 0, int approvedPoId = 0, string dateRange = "", int PageIndex = 1)
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];

            int Total = 0;
            string fromDate = string.Empty;
            string toDate = string.Empty;

            if (dateRange != string.Empty)
            {
                fromDate = dateRange.Split('-')[0].Trim();
                toDate = dateRange.Split('-')[1].Trim();
            }
            List<FundRequiredViewmodel> ListFundRequired = new List<FundRequiredViewmodel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/SearchFundRequired?id={0}&createdPoid={1}&approvedPoId={2}&fromDate={3}&toDate={4}&PageIndex={5}&PageSize={6}", new object[] { 0, 0, oEmployee.POID, fromDate, toDate, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    var oFund = new FundRequiredViewmodel()
                    {
                        Id = dyn.Id,
                        CreatedPoId = dyn.CreatedPoId,
                        CreatedDate = dyn.CreatedDate,
                        CreatedPoName = dyn.CreatedPoName,
                        StatusName = (dyn.Status == "L") ? "Chờ phê duyệt" : "Đã phê duyệt",
                        Status = dyn.Status,
                        CurrencyType = dyn.CurrencyType,
                        RecommentAmount = dyn.RecommentAmount,
                        RecommentAmountString = dyn.RecommentAmount.ToString(),
                        ApprovedAmount = dyn.ApprovedAmount,
                        ApprovedAmountString = dyn.ApprovedAmount.ToString(),
                        Description = dyn.Description
                    };

                    if (!ListFundRequired.Contains(oFund))
                        ListFundRequired.Add(oFund);
                }

                if (rs.Value != null)
                    int.TryParse(rs.Value.ToString(), out Total);
            }
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.ToTal = Total;
            ViewBag.refType = 1;

            return PartialView(ListFundRequired);

        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult FundRequiredCreate(int id = 0)
        {
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
            FundRequiredViewmodel oFund = null;
            if (id == 0)
            {
                decimal normAmount = 0;
                var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/GetNormAmountByPoId?createdPoid={0}", new object[] { oEmployee.POID }), null);
                if (rs != null && rs.Code.ToString() == "00" && rs.Value != null)
                {
                    normAmount = decimal.Parse(rs.Value.ToString());
                }
                oFund = new Models.ViewModel.FundRequiredViewmodel();
                oFund.OpeningAmountString = "0";
                oFund.ReceiveAmountString = "0";
                oFund.PaymentAmountString = "0";
                oFund.ClosingAmountString = "0";
                oFund.ExpectedAmountString = "0";
                oFund.NormAmountString = normAmount.ToString();
                oFund.ApprovedAmountString = "0";
                oFund.RecommentAmountString = "0";
                oFund.CreatedEmpId = oEmployee.ID;
                oFund.CreatedPoId = oEmployee.POID;
                oFund.Status = "L";
                oFund.StatusName = "Chờ phê duyệt";
                oFund.CreatedDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            else
            {
                var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/GetFundRequiredById?id={0}&createdPoid={1}", new object[] { id, oEmployee.POID }), null);
                if (rs != null && rs.Value != null)
                {
                    dynamic dyn = rs.Value;
                    var value = new FundRequiredViewmodel()
                    {
                        Id = dyn.Id,
                        CreatedPoId = dyn.CreatedPoId,
                        CreatedDate = dyn.CreatedDate,
                        CreatedPoName = dyn.CreatedPoName,
                        StatusName = (dyn.Status == "L") ? "Chờ phê duyệt" : "Đã phê duyệt",
                        CurrencyType = dyn.CurrencyType,
                        RecommentAmount = dyn.RecommentAmount,
                        RecommentAmountString = dyn.RecommentAmount.ToString(),
                        ApprovedAmount = dyn.ApprovedAmount,
                        ApprovedAmountString = dyn.ApprovedAmount.ToString(),
                        Description = dyn.Description,
                        OpeningAmountString = dyn.OpeningAmount.ToString(),
                        ReceiveAmountString = dyn.ReceiveAmount.ToString(),
                        PaymentAmountString = dyn.PaymentAmount.ToString(),
                        ClosingAmountString = dyn.ClosingAmount.ToString(),
                        ExpectedAmountString = dyn.ExpectedAmount.ToString(),
                        NormAmountString = dyn.NormAmount.ToString()

                    };

                    oFund = value;
                }
            }

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
            ViewBag.ListCurrencyType = ListCurrencyType;
            return PartialView(oFund);
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult FundRequiredApprovedCreate(int id = 0, int poId = 0)
        {
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<SelectListItem> ListCurrencyType = new List<SelectListItem>();
            FundRequiredViewmodel oFund = null;
            if (id == 0)
            {
                oFund = new Models.ViewModel.FundRequiredViewmodel();
                oFund.OpeningAmountString = "0";
                oFund.ReceiveAmountString = "0";
                oFund.PaymentAmountString = "0";
                oFund.ClosingAmountString = "0";
                oFund.ExpectedAmountString = "0";
                oFund.NormAmountString = "0";
                oFund.ApprovedAmountString = "0";
                oFund.RecommentAmountString = "0";
                oFund.CreatedEmpId = oEmployee.ID;
                oFund.CreatedPoId = oEmployee.POID;
                oFund.Status = "L";
                oFund.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                var rs = Helper.Invoke("GET", string.Format("api/AccountingEntry/GetFundRequiredById?id={0}&createdPoid={1}", new object[] { id, poId }), null);
                if (rs != null && rs.Value != null)
                {
                    dynamic dyn = rs.Value;
                    var value = new FundRequiredViewmodel()
                    {
                        Id = dyn.Id,
                        CreatedPoId = dyn.CreatedPoId,
                        CreatedDate = dyn.CreatedDate,
                        CreatedPoName = dyn.CreatedPoName,
                        StatusName = (dyn.Status == "L") ? "Chờ phê duyệt" : "Đã phê duyệt",
                        Status = dyn.Status,
                        CurrencyType = dyn.CurrencyType,
                        RecommentAmount = dyn.RecommentAmount,
                        RecommentAmountString = dyn.RecommentAmount.ToString(),
                        ApprovedAmount = dyn.ApprovedAmount,
                        ApprovedAmountString = dyn.ApprovedAmount.ToString(),
                        Description = dyn.Description,
                        OpeningAmountString = dyn.OpeningAmount.ToString(),
                        ReceiveAmountString = dyn.ReceiveAmount.ToString(),
                        PaymentAmountString = dyn.PaymentAmount.ToString(),
                        ClosingAmountString = dyn.ClosingAmount.ToString(),
                        ExpectedAmountString = dyn.ExpectedAmount.ToString(),
                        NormAmountString = dyn.NormAmount.ToString()

                    };

                    oFund = value;
                }
            }

            #region ListCurrencyType
            ListCurrencyType.Add(new SelectListItem
            {
                Text = Constant.CurrencyType.VND.ToString(),
                Value = ((int)Constant.CurrencyType.VND).ToString()
            });

            ListCurrencyType.Add(new SelectListItem
            {
                Text = Constant.CurrencyType.USD.ToString(),
                Value = ((int)Constant.CurrencyType.USD).ToString()
            });
            #endregion
            ViewBag.ListCurrencyType = ListCurrencyType;
            return PartialView(oFund);
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult FundRequiredUpdate(FundRequiredViewmodel oFund)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                decimal dOpeningAmount = 0;
                decimal dReceiveAmount = 0;
                decimal dPaymentAmount = 0;
                decimal dClosingAmount = 0;
                decimal dExpectedAmount = 0;
                decimal dNormAmount = 0;
                decimal dRecommentAmount = 0;
                decimal dApprovedAmount = 0;

                dOpeningAmount = decimal.Parse(oFund.OpeningAmountString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                dReceiveAmount = decimal.Parse(oFund.ReceiveAmountString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                dPaymentAmount = decimal.Parse(oFund.PaymentAmountString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                dClosingAmount = decimal.Parse(oFund.ClosingAmountString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                dExpectedAmount = decimal.Parse(oFund.ExpectedAmountString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                dNormAmount = decimal.Parse(oFund.NormAmountString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                dRecommentAmount = decimal.Parse(oFund.RecommentAmountString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));
                dApprovedAmount = decimal.Parse(oFund.ApprovedAmountString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));

                var _Accounting = new
                {
                    Id = oFund.Id,
                    CreatedEmpId = oEmployee.ID,
                    CreatedPoId = oEmployee.POID,
                    CreatedDate = oFund.CreatedDate,
                    ApprovedEmpId = oFund.ApprovedEmpId,
                    ApprovedPoId = oFund.ApprovedPoId,
                    ApprovedDate = oFund.ApprovedDate,
                    Status = oFund.Status,
                    OpeningAmount = dOpeningAmount,
                    ReceiveAmount = dReceiveAmount,
                    PaymentAmount = dPaymentAmount,
                    ClosingAmount = dClosingAmount,
                    ExpectedAmount = dExpectedAmount,
                    NormAmount = dNormAmount,
                    RecommentAmount = dRecommentAmount,
                    ApprovedAmount = 0,
                    CurrencyType = oFund.CurrencyType,
                    Description = oFund.Description

                };
                dynamic rs;
                if (oFund.Id == 0)
                    rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/AddFundRequired", _Accounting);
                else
                    rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/UpdateFundRequired", _Accounting);
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

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult FundRequiredApprove(FundRequiredViewmodel oFund)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];

                decimal dApprovedAmount = 0;


                dApprovedAmount = decimal.Parse(oFund.ApprovedAmountString.Replace(Constant.GroupSeparator, "").Replace(Constant.DecimalSeparator, Constant.StandardDecimalSeparator));

                var _Accounting = new
                {
                    Id = oFund.Id,
                    CreatedPoId = oFund.CreatedPoId,
                    ApprovedEmpId = oEmployee.ID,
                    ApprovedPoId = oEmployee.POID,
                    ApprovedDate = oFund.ApprovedDate,
                    Status = "A",
                    ApprovedAmount = dApprovedAmount

                };
                dynamic rs;
                rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/ApproveFundRequired", _Accounting);
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
        //[ValidateAntiForgeryToken]
        public JsonResult FundRequiredDelete(int id)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                var oRequest = new
                {
                    Id = id,
                    CreatedPoId = oEmployee.POID
                };
                var rs = Helper.Invoke(Common.Constant.Method.POST.ToString(), "api/AccountingEntry/DeleteFundRequired", oRequest);
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
        #endregion
    }
}