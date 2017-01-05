using Cfm.Web.Common;
using Cfm.Web.Log;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Common;
using Cfm.Web.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center, (int)Constant.POLevel.Branch, (int)Constant.POLevel.District, (int)Constant.POLevel.Counter })]
    public class UserController : Controller
    {
        // GET: Admin/User
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;

            Session[Constant.EMP_SESSION] = null;
            Session[Constant.PO_SESSION] = null;
            Session[Constant.TIMEWORK_SESSION] = null;

            LoginViewModel model = new LoginViewModel();
            model.ReturnUrl = returnUrl;
            if (Request.Cookies["Username"] != null && Request.Cookies["Password"] != null)
            {
                model.Username = Security.MD5Decrypt(Request.Cookies["Username"].Value);
                model.Password = Security.MD5Decrypt(Request.Cookies["Password"].Value);
                model.RememberMe = true;
            }

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rs = Helper.Invoke("GET", string.Format("api/Employee/Login?userName={0}&password={1}", new object[]
                    {
                        model.Username,
                        Security.SHA256Hash(model.Password + Security.PASSWORD_KEY)
                    }), null);

                    if (rs.Code == "00")
                    {
                        if (rs.Value != null)
                        {
                            EmployeeViewModel emp = new EmployeeViewModel();
                            dynamic e = rs.Value;
                            emp.ID = e.ID;
                            emp.Username = e.Username;
                            //emp.Password = e.Password;
                            emp.FullName = e.FullName;
                            emp.PhoneNumber = e.PhoneNumber;
                            emp.Address = e.Address;
                            emp.EmpGroupID = e.EmpGroupID;
                            emp.EmpGroupName = e.EmpGroupName;
                            emp.POID = e.POID;
                            emp.PoName = e.PoName;
                            emp.IsLock = e.IsLock == "Y" ? true : false;
                            if (e.ListRole != null)
                            {
                                emp.ListRole = new List<FunctionList>();
                                foreach(dynamic dyna in e.ListRole)
                                {
                                    FunctionList oRole = new FunctionList();
                                    oRole.Id = int.Parse("0" + dyna.Id.ToString());
                                    oRole.Code = (dyna.Code.ToString());
                                    oRole.Name = (dyna.Name.ToString());
                                    oRole.ParentId = int.Parse("0" + dyna.ParentId.ToString());
                                    oRole.EmpGroupId = int.Parse("0" + dyna.EmpGroupId.ToString());
                                    oRole.PoId = int.Parse("0" + dyna.PoId.ToString());
                                    oRole.MenuId = int.Parse("0" + dyna.MenuId.ToString());
                                    oRole.Checked = dyna.Checked;
                                    emp.ListRole.Add(oRole);
                                }
                            }

                            var po = Repository.GetPOByID(emp.POID);
                            if (po != null)
                            {
                                if (model.RememberMe)
                                {
                                    Response.Cookies["Username"].Expires = DateTime.Now.AddDays(30);
                                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                                }
                                else
                                {
                                    Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                                }
                                Response.Cookies["Username"].Value = Security.MD5Encrypt(model.Username);
                                Response.Cookies["Password"].Value = Security.MD5Encrypt(model.Password);

                                Session[Constant.EMP_SESSION] = emp;
                                Session[Constant.PO_SESSION] = po;
                                Session[Constant.TIMEWORK_SESSION] = DateTime.Now.ToString("dd/MM/yyyy");

                                FormsAuthentication.SetAuthCookie(emp.Username, model.RememberMe);

                                if (Url.IsLocalUrl(model.ReturnUrl))
                                {
                                    if(model.ReturnUrl == "/")
                                    {
                                        if (po.POLevel == (int)Constant.POLevel.Center)
                                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                                        else if (po.POLevel == (int)Constant.POLevel.Branch)
                                            return RedirectToAction("Index", "Home", new { area = "CFMBranch" });
                                        else if (po.POLevel == (int)Constant.POLevel.District)
                                            return RedirectToAction("Index", "Home", new { area = "CFMDistrict" });
                                        else if (po.POLevel == (int)Constant.POLevel.Counter)
                                            return RedirectToAction("Index", "Home", new { area = "CFMCounter" });
                                        else
                                            ModelState.AddModelError("", "Không lấy được thông tin đơn vị.");
                                    }
                                    else 
                                    {
                                        return Redirect(model.ReturnUrl);
                                    }
                                }


                                if (po.POLevel == 1)
                                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                                else if (po.POLevel == 2)
                                    return RedirectToAction("Index", "Home", new { area = "CFMBranch" });
                                else if (po.POLevel == 3)
                                    return RedirectToAction("Index", "Home", new { area = "CFMDistrict" });
                                else if (po.POLevel == 4)
                                    return RedirectToAction("Index", "Home", new { area = "CFMCounter" });
                                else
                                    ModelState.AddModelError("", "Không lấy được thông tin đơn vị.");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Không lấy được thông tin đơn vị.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Không lấy được thông tin tài khoản đăng nhập.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(rs.Code, rs.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogToFile(LogFileType.EXCEPTION, "LoginController.Login: " + ex.Message);
                ModelState.AddModelError("99", "Lỗi hệ thống, vui lòng liên hệ quản trị.");
            }
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ChangePassword()
        {
            if (Session[Constant.EMP_SESSION] == null || Session[Constant.PO_SESSION] == null)
            {
                return null;
            }
            else 
            {
                ChangePassViewModel oItem = new ChangePassViewModel();
                EmployeeViewModel oEmp = (EmployeeViewModel)Session[Constant.EMP_SESSION];
                oItem.Username = oEmp.Username;
                oItem.FullName = oEmp.FullName;
                oItem.ID = oEmp.ID;
                return PartialView(oItem);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(ChangePassViewModel oItem)
        {
            JsonResult jResult = new JsonResult();
            
            try
            {
                if (Session[Constant.EMP_SESSION] == null || Session[Constant.PO_SESSION] == null)
                {
                    jResult = Json(new { Code = "-101", Mes = "Bạn không có quyền sử dụng chức năng này!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if(oItem.PasswordNew != oItem.ConfirmPassword)
                    {
                        jResult = Json(new { Code = "-102", Mes = "Mật khẩu xác nhận không chính xác!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if(ModelState.IsValid)
                        {
                            oItem.Password = Security.SHA256Hash(oItem.Password + Security.PASSWORD_KEY);
                            oItem.PasswordNew = Security.SHA256Hash(oItem.PasswordNew + Security.PASSWORD_KEY);
                            var rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/Employee/ChangePasswordEmployee", oItem);
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

                }
            }
            catch(Exception )
            {
                jResult = Json(new { Code = "-99", Mes = "Có lỗi trong quá trình xử lý dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }
          

            return jResult;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User", new { area = "Admin" });
        }
    }
}