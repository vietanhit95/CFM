using Cfm.Web.Mvc.Areas.Admin.Controllers;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.CFMDistrict.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.District })]
    public class HomeController : BaseController
    {
        // GET: CFMDistrict/Home
        public ActionResult Index()
        {
            return View();
        }
        #region DashBoard_Notifi


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DashBoardNotifiGet(NotifiCationViewModels notifi, int id = 0, int PageIndex = 1, string status = "", int refType = 0, string type = "", string oderby = "", string date = "")
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
                    if (NotifiCation.Status == "Y")
                        sTatus = "Đã xử lý";
                    else if (NotifiCation.Status == "N")
                        sTatus = "Chưa xử lý";
                    var Notifi = new NotifiCationViewModels();
                    Notifi.Id = NotifiCation.Id;
                    Notifi.CreateDate = NotifiCation.CreateDate;
                    Notifi.Description = NotifiCation.Description;
                    Notifi.SendPoId = NotifiCation.SendPoId;
                    Notifi.SendPoName = NotifiCation.SendPoName;
                    Notifi.ReceivePoId = NotifiCation.ReceivePoId;
                    Notifi.SendPoCode = NotifiCation.SendPoCode;
                    Notifi.ReceivePoName = NotifiCation.ReceivePoName;
                    Notifi.ReceivePoCode = NotifiCation.ReceivePoCode;
                    Notifi.PassLimits = NotifiCation.PassLimits;
                    Notifi.Status = sTatus;
                    NotifiCation.Type = Notifi.Type;
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
        public ActionResult DashboardNotifiDetails(int Id)
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
        public ActionResult FunnotifiGet(int Id = 0, int PageIndex = 1, string status = "", string date = "")
        {
            EmployeeViewModel on = new EmployeeViewModel();
            List<FunNotifiViewModels> lstFun = new List<FunNotifiViewModels>();
            string fromDate = "";
            string toDate = "";
            if (date == "")
            {
                fromDate = Session[Constant.TIMEWORK_SESSION].ToString();
                toDate = Session[Constant.TIMEWORK_SESSION].ToString();
            }
            else
            {
                fromDate = date.Split('-')[0].Trim();
                toDate = date.Split('-')[1].Trim();
            }
            on = UserCurrent();
            var rs = Helper.Invoke("GET", string.Format("api/AccountingCounter/Report04CDManage?poId={0}&fromDate={1}&toDate={2}&reportStatus={3}&PageIndex={4}&PageSize={5}", new object[] { on.POID, fromDate, toDate, status, PageIndex, Constant.PageSize }), null);
            {
                if (rs != null && rs.ListValue != null)
                {
                    foreach (dynamic Fun in rs.ListValue)
                    {
                        string Status = "";
                        if (Fun.ReportStatus == "C")
                        {
                            Status = "Chưa lập báo cáo";
                        }
                        else if (Fun.ReportStatus == "L")
                        {
                            Status = "Chưa xác nhận";
                        }
                        else if (Fun.ReportStatus == "A")
                        {
                            Status = "Đã xác nhận";
                        }
                        else
                        {
                            Status = "Trạng thái không xác định";
                        }
                        var FunNoti = new FunNotifiViewModels();
                        FunNoti.Id = Fun.Id;
                        FunNoti.poName = Fun.PoName;
                        FunNoti.reportStatus = Status;
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