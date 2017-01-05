using Cfm.Web.Lib;
using Cfm.Web.Mvc.Areas.Admin.Controllers;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel;
using Cfm.Web.Mvc.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Counter, (int)Constant.POLevel.Center, (int)Constant.POLevel.Branch, (int)Constant.POLevel.District })]
    public class SystemController : BaseController
    {
        // GET: CFMCounter/System
        public ActionResult Index()
        {
            return View();
        }

        #region Tham số Dịch vụ Bưu cục
        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.System_ReportConfigItemPO })]
        public ActionResult ReportConfigItemPO()
        {
            ReportPoSubmit oReportPoSubmit = new ReportPoSubmit();
            return PartialView(oReportPoSubmit);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ListReportConfig(int Id = 0, int ReportId = 0, int ItemGroupID = 0, int PageIndex = 1)
        {
            List<ReportConfigViewModel> ListReportConfig = new List<ReportConfigViewModel>();
            int Total = 0;
            try
            {
                var Po = PoCurrent();
                var rs = Helper.Invoke("GET", string.Format("api/System/GetReportConfig?id={0}&reportID={1}&itemGroupID={2}&PoId={3}&PageIndex={4}&PageSize={5}", new object[] { Id, ReportId, ItemGroupID, Po.ID, PageIndex, Constant.PageSize }), null);
                if (rs != null && rs.ListValue != null)
                {
                    foreach (dynamic dyn in rs.ListValue)
                    {
                        var oReport = new ReportConfigViewModel()
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
                            ReportName = dyn.ReportName
                        };

                        ListReportConfig.Add(oReport);
                    }

                    Total = int.Parse(rs.Value.ToString());
                }
            }
            catch
            {
                ListReportConfig = null;
            }


            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.Total = Total;
            return PartialView(ListReportConfig);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ListReportConfigPo(int Id = 0, int ReportId = 0, int ItemId = 0, int ConfigId = 0, int PageIndex = 1)
        {
            List<ReportConfigPo> ListReportConfigPo = new List<ReportConfigPo>();
            int Total = 0;
            try
            {
                var Po = PoCurrent();
                var rs = Helper.Invoke("GET", string.Format("api/System/ReportConfigPoSearch?Id={0}&ReportId={1}&ItemId={2}&PoId={3}&ConfigId={4}&PageIndex={5}&PageSize={6}", new object[] { Id, ReportId, ItemId, Po.ID, ConfigId, PageIndex, Constant.PageSize }), null);
                if (rs != null && rs.ListValue != null)
                {
                    foreach (dynamic dyn in rs.ListValue)
                    {
                        var oReport = new ReportConfigPo()
                        {
                            Id = dyn.Id,
                            ReportItemId = dyn.ReportItemId,
                            PoId = dyn.PoId,
                            ReportId = dyn.ReportId,
                            ItemId = dyn.ItemId,
                            ItemCode = dyn.ItemCode,
                            ItemName = dyn.ItemName,
                            DisplayName = dyn.DisplayName,
                            ReportName = dyn.ReportName,
                            ReportCode = dyn.ReportCode
                        };

                        ListReportConfigPo.Add(oReport);
                    }

                    Total = int.Parse(rs.Value.ToString());
                }
            }
            catch
            {
                ListReportConfigPo = null;
            }


            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize = Constant.PageSize;
            ViewBag.Total = Total;
            return PartialView(ListReportConfigPo);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult ReportConfigPoUpdate(ReportPoSubmit oReportPoSubmit)
        {
            JsonResult jResult = new JsonResult();
            dynamic arrDel = JObject.Parse(oReportPoSubmit.DelString);
            dynamic arrAdd = JObject.Parse(oReportPoSubmit.AddString);
            try
            {
                var Po = PoCurrent();
                List<ReportConfigPo> ListReportConfigDelete = new List<ReportConfigPo>();
                List<ReportConfigPo> ListReportConfigAdd = new List<ReportConfigPo>();

                foreach (var item in arrDel.Report)
                {
                    if (int.Parse((item.ReportPoId ?? "0").ToString()) > 0)
                    {
                        ReportConfigPo oReport = new ReportConfigPo();
                        oReport.Id = int.Parse((item.Id ?? "0").ToString());
                        oReport.PoId = Po.ID;
                        ListReportConfigDelete.Add(oReport);
                    }
                }

                foreach (var item in arrAdd.Report)
                {
                    if (int.Parse((item.ReportPoId ?? "0").ToString()) == 0)
                    {
                        ReportConfigPo oReport = new ReportConfigPo();
                        oReport.ReportItemId = int.Parse((item.Id ?? "0").ToString());
                        oReport.PoId = Po.ID;
                        ListReportConfigAdd.Add(oReport);
                    }
                }

                ResponseObject rsAdd = new ResponseObject();
                ResponseObject rsDel = new ResponseObject();
                string Code = string.Empty;
                string Mes = string.Empty;

                if (ListReportConfigAdd != null && ListReportConfigAdd.Count > 0)
                {
                    rsAdd = Helper.Invoke(Constant.Method.POST.ToString(), "api/System/System", ListReportConfigAdd);
                    Code = rsAdd.Code;
                    Mes = rsAdd.Message;
                }

                if (ListReportConfigDelete != null && ListReportConfigDelete.Count > 0)
                {
                    rsDel = Helper.Invoke(Constant.Method.POST.ToString(), "api/System/DeleteReportConfigPo", ListReportConfigDelete);
                    Code = rsDel.Code;
                    Mes = rsDel.Message;
                }

                if (Code == string.Empty)
                {
                    Code = "01";
                    Mes = "Không có chỉ tiêu nào được chọn.";
                }                    

                jResult = Json(new { Code = Code, Mes = Mes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }
        #endregion

        #region Cấu hình đơn vị
        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] 
        { 
            (int)Constant.Function.System_PoManage, 
            (int)Constant.Function.System_Branch_PoManage, 
            (int)Constant.Function.System_District_PoManage,
            (int)Constant.Function.System_Center_PoManage
        })]
        public ActionResult PoManage()
        {
            PostOfficeViewModel po = PoCurrent();
            return View(po);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult PoTree()
        {
            PostOfficeViewModel po = PoCurrent();

            return PartialView(po);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult PoChild(int ParentId)
        {
            var rs = Helper.Invoke("GET", string.Format("api/PostOffice/GetListPOByParentID?parentID={0}", new object[] { ParentId }), null);

            List<PostOfficeViewModel> ListPo = new List<PostOfficeViewModel>();
            if (rs != null && rs.Code == "00" && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    PostOfficeViewModel oItem = new PostOfficeViewModel();

                    oItem.ID = int.Parse((dyn.Id ?? "0").ToString());
                    oItem.ParentID = int.Parse((dyn.ParentId ?? "0").ToString());
                    oItem.Code = (dyn.Code ?? "").ToString();
                    oItem.Name = (dyn.Name ?? "").ToString();
                    oItem.POLevel = int.Parse((dyn.POLevel ?? "0").ToString());
                    oItem.IsCenter = (dyn.IsCenter ?? "N").ToString() == "Y" ? true : false;
                    oItem.Address = (dyn.Address ?? "").ToString();
                    oItem.PhoneNumber = (dyn.PhoneNumber ?? "").ToString();
                    oItem.FaxNumber = (dyn.FaxNumber ?? "").ToString();
                    oItem.IsOffline = (dyn.IsOffline ?? "N").ToString() == "Y" ? true : false;
                    oItem.CycleDate = int.Parse((dyn.CycleDate ?? "0").ToString());
                    oItem.IsLock = (dyn.IsLock ?? "Y").ToString() == "Y" ? true : false;
                    ListPo.Add(oItem);
                }
            }

            return PartialView(ListPo);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult PoDetail(int PoId = 0)
        {
            PostOfficeViewModel po = new PostOfficeViewModel();
            if (PoId == 0)
            {
                po = PoCurrent();
            }
            else
            {
                var rs = Helper.Invoke("GET", string.Format("api/PostOffice/GetPOByID?id={0}", new object[] { PoId }), null);

                if (rs != null && rs.Code == "00" && rs.Value != null)
                {
                    dynamic dyn = rs.Value;

                    po.ID = int.Parse((dyn.Id ?? "0").ToString());
                    po.ParentID = int.Parse((dyn.ParentId ?? "0").ToString());
                    po.Code = (dyn.Code ?? "").ToString();
                    po.Name = (dyn.Name ?? "").ToString();
                    po.POLevel = int.Parse((dyn.POLevel ?? "0").ToString());
                    po.IsCenter = (dyn.IsCenter ?? "N").ToString() == "Y" ? true : false;
                    po.Address = (dyn.Address ?? "").ToString();
                    po.PhoneNumber = (dyn.PhoneNumber ?? "").ToString();
                    po.FaxNumber = (dyn.FaxNumber ?? "").ToString();
                    po.IsOffline = (dyn.IsOffline ?? "N").ToString() == "Y" ? true : false;
                    po.CycleDate = int.Parse((dyn.CycleDate ?? "0").ToString());
                    po.IsLock = (dyn.IsLock ?? "Y").ToString() == "Y" ? true : false;
                }
            }

            return PartialView(po);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult PoAdd(int Id = 0, int ParentId = 0)
        {
            PostOfficeViewModel po = new PostOfficeViewModel();
            try
            {
                if (Id > 0)
                {
                    var rs = Helper.Invoke("GET", string.Format("api/PostOffice/GetPOByID?id={0}", new object[] { Id }), null);

                    if (rs != null && rs.Code == "00" && rs.Value != null)
                    {
                        dynamic dyn = rs.Value;

                        po.ID = int.Parse((dyn.Id ?? "0").ToString());
                        po.ParentID = int.Parse((dyn.ParentId ?? "0").ToString());
                        po.Code = (dyn.Code ?? "").ToString();
                        po.Name = (dyn.Name ?? "").ToString();
                        po.POLevel = int.Parse((dyn.POLevel ?? "0").ToString());
                        po.IsCenter = (dyn.IsCenter ?? "N").ToString() == "Y" ? true : false;
                        po.Address = (dyn.Address ?? "").ToString();
                        po.PhoneNumber = (dyn.PhoneNumber ?? "").ToString();
                        po.FaxNumber = (dyn.FaxNumber ?? "").ToString();
                        po.IsOffline = (dyn.IsOffline ?? "N").ToString() == "Y" ? true : false;
                        po.CycleDate = int.Parse((dyn.CycleDate ?? "0").ToString());
                        po.IsLock = (dyn.IsLock ?? "Y").ToString() == "Y" ? true : false;
                    }
                }
                else
                {
                    po.ParentID = ParentId;
                }
                return PartialView(po);
            }
            catch
            {
                return null;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ListPoChild(int ParentId = 0)
        {
            if (ParentId == 0)
            {
                PostOfficeViewModel po = PoCurrent();
                if (po != null)
                {
                    ParentId = po.ID;
                }
            }

            if (ParentId != 0)
            {
                var rs = Helper.Invoke("GET", string.Format("api/PostOffice/GetListPOByParentID?parentID={0}", new object[] { ParentId }), null);

                List<PostOfficeViewModel> ListPo = new List<PostOfficeViewModel>();
                if (rs != null && rs.Code == "00" && rs.ListValue != null)
                {
                    foreach (dynamic dyn in rs.ListValue)
                    {
                        PostOfficeViewModel oItem = new PostOfficeViewModel();

                        oItem.ID = int.Parse((dyn.Id ?? "0").ToString());
                        oItem.ParentID = int.Parse((dyn.ParentId ?? "0").ToString());
                        oItem.Code = (dyn.Code ?? "").ToString();
                        oItem.Name = (dyn.Name ?? "").ToString();
                        oItem.POLevel = int.Parse((dyn.POLevel ?? "0").ToString());
                        oItem.IsCenter = (dyn.IsCenter ?? "N").ToString() == "Y" ? true : false;
                        oItem.Address = (dyn.Address ?? "").ToString();
                        oItem.PhoneNumber = (dyn.PhoneNumber ?? "").ToString();
                        oItem.FaxNumber = (dyn.FaxNumber ?? "").ToString();
                        oItem.IsOffline = (dyn.IsOffline ?? "N").ToString() == "Y" ? true : false;
                        oItem.CycleDate = int.Parse((dyn.CycleDate ?? "0").ToString());
                        oItem.IsLock = (dyn.IsLock ?? "Y").ToString() == "Y" ? true : false;
                        ListPo.Add(oItem);
                    }
                }
                ViewBag.ParentId = ParentId;
                return PartialView(ListPo);
            }
            else
            {
                return null;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult UpdatePo(PostOfficeViewModel oPo)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                if (ModelState.IsValid)
                {
                    ResponseObject rs = new ResponseObject();
                    if (oPo.ID > 0)
                    {
                        rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/PostOffice/UpdatePo", oPo);

                        if (rs.Code == "00")
                        {
                            PostOfficeViewModel po = PoCurrent();
                            if (oPo.ID == po.ID)
                            {
                                po.Name = oPo.Name;
                                po.PhoneNumber = oPo.PhoneNumber;
                                po.Address = oPo.Address;
                                po.Code = oPo.Code;
                                po.FaxNumber = oPo.FaxNumber;
                                Session[Constant.PO_SESSION] = po;
                            }
                        }
                    }
                    else
                    {
                        rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/PostOffice/AddPo", oPo);
                    }

                    jResult = Json(new { Code = rs.Code, Mes = rs.Message, Value = rs.Value }, JsonRequestBehavior.AllowGet);
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

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePo(int Id)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                PostOfficeViewModel po = new PostOfficeViewModel();
                po.ID = Id;

                var rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/PostOffice/PoDelete", po);
                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult PoChangeStatus(int Id)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                PostOfficeViewModel po = new PostOfficeViewModel();
                po.ID = Id;

                var rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/PostOffice/PoChangeStatus", po);
                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }
        #endregion

        #region Quản lý người dùng
        [AcceptVerbs(HttpVerbs.Get)]
        [AuthoritiesFunc(FunctionRole = new int[] 
        { 
            (int)Constant.Function.System_UserManage, 
            (int)Constant.Function.System_Branch_UserManage, 
            (int)Constant.Function.System_District_UserManage,
            (int)Constant.Function.System_Center_UserManage
        })]
        public ActionResult UserManage()
        {
            PostOfficeViewModel po = PoCurrent();
            return View(po);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ListUser(int PoId)
        {
            List<EmployeeViewModel> ListUser = new List<EmployeeViewModel>();
            var UserCurrentId = UserCurrent().ID;
            try
            {
                var rs = Helper.Invoke("GET", string.Format("api/Employee/GetUser?PoId={0}", new object[] { PoId }), null);
                if (rs != null && rs.Code == "00" && rs.ListValue != null)
                {
                    foreach (dynamic dyn in rs.ListValue)
                    {
                        EmployeeViewModel oItem = new EmployeeViewModel();

                        oItem.ID = Convert.ToInt32("0" + dyn.ID);
                        oItem.Username = dyn.Username.ToString();
                        oItem.FullName = dyn.FullName.ToString();
                        oItem.Address = dyn.Address.ToString();
                        oItem.PhoneNumber = dyn.PhoneNumber.ToString();
                        oItem.EmpGroupID = Convert.ToInt32("0" + dyn.EmpGroupID.ToString());
                        oItem.EmpGroupName = dyn.EmpGroupName.ToString();
                        oItem.POID = Convert.ToInt32("0" + dyn.POID.ToString());
                        oItem.IsLock = (dyn.IsLock ?? "Y").ToString() == "Y" ? true : false;

                        ListUser.Add(oItem);
                    }
                }
                else
                {
                    ListUser = null;
                }
            }
            catch
            {
                ListUser = null;
            }

            ViewBag.PoId = PoId;
            ViewBag.UserCurrentId = UserCurrentId;
            return PartialView(ListUser);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CreateUser(int UserId = 0, string UserName = "", int PoId = 0)
        {
            EmployeeViewModel oEmp = new EmployeeViewModel();
            EmployeeViewModel oEmpCurrent = UserCurrent();
            List<SelectListItem> ListEmployeeGroup = new List<SelectListItem>();

            #region List Employee Group
            ListEmployeeGroup.Add(new SelectListItem
            {
                Text = "---Nhóm Người dùng---",
                Value = ""
            });

            var rs = Helper.Invoke("GET", string.Format("api/Employee/GetEmployeeGroup?PoId={0}", new object[] { PoId }), null);
            if (rs != null && rs.Code == "00" && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    ListEmployeeGroup.Add(new SelectListItem
                    {
                        Text = dyn.Name.ToString(),
                        Value = dyn.Id.ToString()
                    });
                }
            }
            #endregion

            if (UserId > 0 && !string.IsNullOrEmpty(UserName))
            {
                var res = Helper.Invoke("GET", string.Format("api/Employee/GetUserV2?PoId={0}&Id={1}&UserName={2}&GroupId={3}", new object[] { 0, UserId, UserName, 0 }), null);
                if (res != null && res.Code == "00" && res.ListValue != null)
                {
                    foreach (dynamic dyn in res.ListValue)
                    {
                        oEmp.ID = Convert.ToInt32("0" + dyn.ID.ToString());
                        oEmp.Username = dyn.Username.ToString();
                        oEmp.FullName = dyn.FullName.ToString();
                        oEmp.Address = dyn.Address.ToString();
                        oEmp.PhoneNumber = dyn.PhoneNumber.ToString();
                        oEmp.EmpGroupID = Convert.ToInt32("0" + dyn.EmpGroupID.ToString());
                        oEmp.EmpGroupName = dyn.EmpGroupName.ToString();
                        oEmp.POID = Convert.ToInt32("0" + dyn.POID);
                        oEmp.IsLock = (dyn.IsLock ?? "Y").ToString() == "Y" ? true : false;
                    }
                }
            }

            ViewBag.ListEmployeeGroup = ListEmployeeGroup;
            return PartialView(oEmp);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateUser(EmployeeViewModel oUser)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                ResponseObject rs = new ResponseObject();
                if(ModelState.IsValid)
                {
                    string Code = string.Empty;
                    string Mes = string.Empty;

                    if (oUser.ID > 0)
                    {
                        rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/Employee/UpdateEmployee", oUser);

                        Code = rs.Code;
                        Mes = rs.Message;

                        if (rs.Code == "00")
                        {
                            EmployeeViewModel oEmp = UserCurrent();

                            if (oUser.ID == oEmp.ID && oUser.Username == oEmp.Username)
                            {
                                oEmp.FullName = oUser.FullName;
                                oEmp.Address = oUser.Address;
                                oEmp.PhoneNumber = oUser.PhoneNumber;
                                oEmp.EmpGroupID = oUser.EmpGroupID;

                                Session[Constant.EMP_SESSION] = oEmp;
                            }
                        }
                    }
                    else
                    {
                        rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/Employee/AddEmployee", oUser);
                        Code = rs.Code;
                        Mes = rs.Message;
                    }

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
            catch (Exception )
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteUser(int Id, string UserName)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.ID = Id;
                emp.Username = UserName;

                var rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/Employee/DeleteEmployee", emp);
                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult UserChangeStatus(int Id, string UserName)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.ID = Id;
                emp.Username = UserName;

                var rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/Employee/ChangeStatusEmployee", emp);
                jResult = Json(new { Code = rs.Code, Mes = rs.Message }, JsonRequestBehavior.AllowGet);
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