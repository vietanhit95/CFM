using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Common;
using Cfm.Web.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center })]
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center, (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult Index()
        {
            PostOfficeViewModel po = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            if (po.POLevel == (int)Constant.POLevel.Branch)
                return RedirectToAction("Index", "Home", new { area = "CFMBranch" });
            else if (po.POLevel == (int)Constant.POLevel.District)
                return RedirectToAction("Index", "Home", new { area = "CFMDistrict" });
            else if (po.POLevel == (int)Constant.POLevel.Counter)
                return RedirectToAction("Index", "Home", new { area = "CFMCounter" });
            else
            return View();
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center, (int)Constant.POLevel.Counter, (int)Constant.POLevel.District })]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TimeWork()
        {

            return PartialView();
        }

        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center, (int)Constant.POLevel.Counter, (int)Constant.POLevel.District })]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SetTimeWork()
        {
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center, (int)Constant.POLevel.Counter, (int)Constant.POLevel.District })]
        [ValidateAntiForgeryToken]
        public ActionResult SetTimeWork(FormCollection form)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                string[] arrTimeWork = form["TimeWork"].ToString().Split('/');
                DateTime date = new DateTime(int.Parse(arrTimeWork[2]), int.Parse(arrTimeWork[1]), int.Parse(arrTimeWork[0]));
                Session[Constant.TIMEWORK_SESSION] = date.ToString("dd/MM/yyyy");

                jResult = Json(new { Code = "00", Mes = "Thay đổi giờ làm việc thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center, (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        //[AuthoritiesFunc(FunctionRole = new int[] { (int)Constant.Function.System_RoleManage, (int)Constant.Function.System_District_RoleManage, (int)Constant.Function.System_Branch_RoleManage })]
        public ActionResult RoleManage()
        {
            EmployeeViewModel oEmp = UserCurrent();
            if(oEmp.EmpGroupID == (int)Constant.Role.PTCenter 
                || oEmp.EmpGroupID == (int)Constant.Role.PTProvince
                || oEmp.EmpGroupID == (int)Constant.Role.PTDistrict
                || oEmp.EmpGroupID == (int)Constant.Role.PTPO)
            {
                List<SelectListItem> ListEmployeeGroup = new List<SelectListItem>();
                EmployeeViewModel oEmpCurrent = UserCurrent();
                var rs = Helper.Invoke("GET", string.Format("api/Employee/GetEmployeeGroup?PoId={0}", new object[] { oEmpCurrent.POID }), null);
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

                ViewBag.ListEmployeeGroup = ListEmployeeGroup;
                ViewBag.GroupID = ListEmployeeGroup[0].Value;
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { Area = "Admin" });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center, (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult TreeFunction(int EmpGroupId)
        {
            PostOfficeViewModel oPo = PoCurrent();
            List<FunctionList> ListFunctionList = new List<FunctionList>();
            var rs = Helper.Invoke("GET", string.Format("api/Employee/SearchFunctionMenu?EmpGroupId={0}&PoId={1}&AreaId={2}", new object[] { EmpGroupId, oPo.ID, oPo.POLevel }), null);

            if(rs.Code == "00")
            {
                dynamic dyna = rs.ListValue;

                foreach(var item in dyna)
                {
                    FunctionList oItem = new FunctionList();

                    oItem.Id = int.Parse(item.Id.ToString());
                    oItem.MenuId = int.Parse(item.MenuId.ToString());
                    oItem.Name = item.Name.ToString();
                    oItem.ParentId = int.Parse(item.ParentId.ToString());
                    oItem.PoId = int.Parse(item.PoId.ToString());
                    oItem.Checked = item.Checked;
                    oItem.EmpGroupId = EmpGroupId;

                    ListFunctionList.Add(oItem);
                }
            }

            return PartialView(ListFunctionList);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center, (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult TreeFunctionChild(List<FunctionList> ListFunctionList = null, int ParentId = 0)
        {
            ViewBag.ParentId = ParentId;
            return PartialView(ListFunctionList);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center, (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public JsonResult RoleSetup(List<FunctionList> ListRole)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                EmployeeViewModel oUser = UserCurrent();
                for (int i = 0; i < ListRole.Count; i++)
                {
                    ListRole[i].PoId = oUser.POID;
                }
                var rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/Employee/UpdateRole", ListRole);

                string Code = rs.Code;
                string Mes = rs.Message;

                if(rs.Code == "00")
                {
                    var res = Helper.Invoke(Constant.Method.GET.ToString(), string.Format("api/Employee/SearchRole?EmpGroupId={0}&PoId={1}", oUser.EmpGroupID, oUser.POID), null);

                    if(res.Code == "00")
                    {
                        if(res.ListValue != null)
                        {
                            oUser.ListRole = new List<FunctionList>();
                            dynamic dyna = res.ListValue;
                            foreach(var item in dyna)
                            {
                                FunctionList oRole = new FunctionList();
                                oRole.Id = int.Parse("0" + item.Id.ToString());
                                oRole.Code = (item.Code.ToString());
                                oRole.Name = (item.Name.ToString());
                                oRole.ParentId = int.Parse("0" + item.ParentId.ToString());
                                oRole.EmpGroupId = int.Parse("0" + item.EmpGroupId.ToString());
                                oRole.PoId = int.Parse("0" + item.PoId.ToString());
                                oRole.MenuId = int.Parse("0" + item.MenuId.ToString());
                                oRole.Checked = item.Checked;
                                oUser.ListRole.Add(oRole);
                            }

                            Session[Constant.EMP_SESSION] = oUser;
                        }
                    }
                }

                jResult = Json(new { Code = Code, Mes = Mes }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }
           

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center, (int)Constant.POLevel.Counter, (int)Constant.POLevel.District, (int)Constant.POLevel.Branch })]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}