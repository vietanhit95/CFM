using Cfm.Web.Common;
using Cfm.Web.Lib;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{    
    public class EmployeeController : Controller
    {
        public ActionResult Index(int poId=0)
        {
          
            return View();
        }
        public ActionResult Create(int poId)
        {
            EmployeeViewModel emp = new EmployeeViewModel();
          
            return View(emp);
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel emp)
        {
        
               
            return View(emp);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel model = new LoginViewModel();
            if (Request.Cookies["Username"] != null && Request.Cookies["Password"] != null)
            {
                model.Username = Request.Cookies["Username"].Value;
                model.Password = Request.Cookies["Password"].Value;
                model.RememberMe = true;
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Helper.APIUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var req = new
                        {
                            Username = model.Username,
                            Password = model.Password,
                            Signature = Security.SHA256Hash(model.Username + model.Password + Common.Helper.APIKey)
                        };
                        var res = client.PostAsJsonAsync("api/Employee/Login", req).Result;
                        if (res.IsSuccessStatusCode)
                        {
                            var rs = res.Content.ReadAsAsync<ResponseObject>().Result;
                            if (rs.Code == "00")
                            {
                                if (rs.Value != null)
                                {
                                    EmployeeViewModel emp = new EmployeeViewModel();
                                    dynamic e = rs.Value;
                                    emp.ID = e.Id;
                                    emp.Username = e.Username;
                                    emp.Password = e.Password;
                                    emp.FullName = e.FullName;
                                    emp.PhoneNumber = e.PhoneNumber;
                                    emp.Address = e.Address;
                                    emp.EmpGroupID = e.EmpGroupId;
                                    emp.EmpGroupName = e.EmpGroupName;
                                    emp.POID = e.POId;
                                    emp.IsLock = e.IsLock == "Y" ? true : false;

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
                                    Response.Cookies["Username"].Value = model.Username;
                                    Response.Cookies["Password"].Value = model.Password;

                                    ModelState.Clear();
                                    Session["Employee"] = emp;
                                    FormsAuthentication.SetAuthCookie(emp.Username, model.RememberMe);

                                    if (Url.IsLocalUrl(returnUrl))
                                    {
                                        return Redirect(returnUrl);
                                    }
                                    return RedirectToAction("Index", "Home",new { area = "CFMCounter" });
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
                        else
                        {
                            ModelState.AddModelError("98", "Lỗi kết nối hệ thống, vui lòng liên hệ quản trị.");
                        }
                    }
                }
            }
            catch (Exception )
            {
               
                ModelState.AddModelError("99", "Lỗi hệ thống, vui lòng liên hệ quản trị.");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Employee"] = null;
            return RedirectToAction("Login", "Employee", new { area = "Admin" });
        }
    }
}