using Cfm.Web.Mvc.Areas.Admin.Controllers;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Areas.CFMBranch.Models;
using Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel;
using Cfm.Web.Mvc.Areas.CFMDistrict.Models.ViewModels;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.CFMBranch.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Branch })]
    public class AccountingController : BaseController
    {
        // GET: CFMBranch/Accounting
        public ActionResult Index()
        {
            return View();
        }

        #region Report 02CD
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Report02CD(int reportID = 0, string reportDate = "")
        {
            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            List<int> ListItemGroupID_TDV = new List<int>();
            List<int> ListItemGroupID_TKBD = new List<int>();
            List<int> ListItemGroupID_KD = new List<int>();

            CD02HeaderViewModel CD02Header = new CD02HeaderViewModel();
            CD02Header.ListDetail = new List<CD02DetailViewModel>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            CD02Header.PoId = oEmployee.POID;
            CD02Header.ApprovedEmpId = oEmployee.ID;
            int Total = 0;
            CD02Header.ReportDate = DateTime.Now.ToString("dd/MM/yyyy");
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
            return View(CD02Header);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Report02CDGetData(int reportID = 0, string reportDate = "", string isAgain = "N")
        {
            List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
            List<int> ListItemGroupID = new List<int>();
            List<int> ListItemGroupID_TDV = new List<int>();
            List<int> ListItemGroupID_TKBD = new List<int>();
            List<int> ListItemGroupID_KD = new List<int>();
            CD02HeaderViewModel CD02Header = new CD02HeaderViewModel();
            CD02Header.ListDetail = new List<CD02DetailViewModel>();
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            CD02Header.PoId = oEmployee.POID;
            CD02Header.ApprovedEmpId = oEmployee.ID;
            if (reportDate != "")
                CD02Header.ReportDate = reportDate;
            else
                CD02Header.ReportDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
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
            return PartialView(CD02Header);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Report02CDGet(CD02HeaderViewModel CD02Header = null, int reportID = 0, int itemGroupId = 0, int cashFllowID = 0, string isAgain = "N")
        {
            var rs = Helper.Invoke("GET", string.Format("api/AccountingProvince/Report02CDSummary?id={0}&reportID={1}&itemGroupID={2}&reportDate={3}&cashFllowId={4}&poId={5}&isAgain={6}", new object[] { 0, reportID, itemGroupId, CD02Header.ReportDate, cashFllowID, CD02Header.PoId, isAgain }), null);
            if (rs != null && rs.Value != null)
            {
                dynamic oValue = rs.Value;
                if (oValue.ListDetail != null)
                {
                    CD02Header = CD02Header ?? new CD02HeaderViewModel();

                    foreach (dynamic dyn in oValue.ListDetail)
                    {
                        var oDetail = new CD02DetailViewModel()
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

                        if (!CD02Header.ListDetail.Contains(oDetail))
                            CD02Header.ListDetail.Add(oDetail);
                    }
                }

            }

            ViewBag.reportID = reportID;
            ViewBag.itemGroupID = itemGroupId;
            ViewBag.cashFllowID = cashFllowID;
            return PartialView(CD02Header);
        }
        #endregion

        #region Report 03CD
        public ActionResult Report03CDReplace(int reportID = 0, int poId = 0, string reportDate = "")
        {
            try
            {
                EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                var po = Repository.GetPOByID(poId);
               
                if (po != null && po.ParentID == oEmployee.POID)
                {
                    List<ItemGroupViewModel> listItemGroup = new List<ItemGroupViewModel>();
                    List<int> ListItemGroupID = new List<int>();
                    List<int> ListItemGroupID_TDV = new List<int>();
                    List<int> ListItemGroupID_TKBD = new List<int>();
                    List<int> ListItemGroupID_KD = new List<int>();
                   
                    CD03HeaderViewModel CD03Header = new CD03HeaderViewModel();
                    CD03Header.PoName = po.Name;
                    CD03Header.PoCode = po.Code;
                    CD03Header.PoId = po.ID;
                    CD03Header.IsApproved = true;

                    CD03Header.ListDetail = new List<CD03DetailViewModel>();

                    CD03Header.PoId = poId;
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
                    ViewBag.PoID = CD03Header.PoId;
                    ViewBag.ListItemGroupID = ListItemGroupID;
                    ViewBag.ListItemGroupID_TDV = ListItemGroupID_TDV;
                    ViewBag.ListItemGroupID_TKBD = ListItemGroupID_TKBD;
                    ViewBag.ListItemGroupID_KD = ListItemGroupID_KD;
                   
                    ViewBag.reportDate = CD03Header.ReportDate;
                    return View(CD03Header);
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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Report03CDReplaceManage()
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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Report03CDReplaceManageGet(int PoId = 0, string dateRange = "", string reportStatus = "", int pageIndex = 1)
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
            List<CD03HeaderViewModel> listReportHeader = new List<CD03HeaderViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report03CDManage?poId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}&PoDistrictId={6}", new object[] { PoId, fromDate, toDate, reportStatus, pageIndex, Common.Constant.PageSize, oEmployee.POID }), null);
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
        #endregion
        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.BusinessSupport_Branch_ReportPOManage })]
        public ActionResult ReportPOManage()
        {
            List<SelectListItem> ListReportType = new List<SelectListItem>();
            List<SelectListItem> ListStatus = new List<SelectListItem>();

            #region ListReportType
            ListReportType.Add(new SelectListItem
            {
                Text = "Báo cáo 02CĐ của BĐ Tỉnh",
                Value = "02CD"
            });

            ListReportType.Add(new SelectListItem
            {
                Text = "Báo cáo 03CĐ của BĐ Quận, Huyện",
                Value = "03CD",
                Selected = true
            });

            ListReportType.Add(new SelectListItem
            {
                Text = "Báo cáo 04CĐ trong BĐ Quận, Huyện",
                Value = "04CD"
            });
            #endregion

            #region ListStatus
            ListStatus.Add(new SelectListItem
            {
                Text = "Tất cả",
                Value =""
            });

            ListStatus.Add(new SelectListItem
            {
                Text = "Chưa lập báo cáo",
                Value = "C"
            });

            ListStatus.Add(new SelectListItem
            {
                Text = "Chưa xác nhận",
                Value = "L"
            });

            ListStatus.Add(new SelectListItem
            {
                Text = "Đã xác nhận",
                Value = "A"
            });

            ListStatus.Add(new SelectListItem
            {
                Text = "Báo cáo chậm tiến độ",
                Value = "L1"
            });

            ListStatus.Add(new SelectListItem
            {
                Text = "Báo cáo đúng hạn",
                Value = "T"
            });

            ListStatus.Add(new SelectListItem
            {
                Text = "Trạng thái không xác định",
                Value = "N"
            });
            #endregion

            ViewBag.ListStatus = ListStatus;
            ViewBag.ListReportType = ListReportType;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.BusinessSupport_Branch_ReportPOManage })]
        public ActionResult ReportPOManage04CDGet(int poDistrictId, string dateRange = "", string reportStatus = "", int PageIndex = 1)
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
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04CDManage?PoDistrictId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}", new object[] { poDistrictId, fromDate, toDate, reportStatus, PageIndex, Common.Constant.PageSize }), null);
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
                        ApprovedEmpName = dyn.ApprovedEmpName,
                        PoCode = dyn.PoCode,
                        PoName = dyn.PoName

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

            return PartialView(listReportHeader);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.BusinessSupport_Branch_ReportPOManage })]
        public ActionResult ReportPOManage03CDGet(string dateRange = "", string Status = "", int PageIndex = 1)
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

            if (Status == "L")
            {
                Status = "";
            }
            else if(Status == "L1")
            {
                Status = "L";
            }

            int Total = 0;
            EmployeeViewModel oEmployee = (EmployeeViewModel)Session[Constant.EMP_SESSION];
            List<CD03HeaderViewModel> listReportHeader = new List<CD03HeaderViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingDistrict/Report03CDManage?provincePoId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}", new object[] { oEmployee.POID, fromDate, toDate, Status, PageIndex, Common.Constant.PageSize }), null);
            if (rs != null && rs.ListValue != null)
            {
                string statusName = "";
                string status = "";
                foreach (dynamic dyn in rs.ListValue)
                {
                    try
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
                    }
                    catch
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
                        ReportStatusName =statusName

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
    }
}