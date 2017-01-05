using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class UserViewModel
    {
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class ChangePassViewModel
    {
        [Required(ErrorMessage = "Tài khoản hoặc Mật khẩu không chính xác!")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Tài khoản hoặc Mật khẩu không chính xác!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Tài khoản hoặc Mật khẩu không chính xác!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu cũ không được để trống!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới không được để trống!")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có độ dài lớn hơn 6 ký tự!")]
        public string PasswordNew { get; set; }

        [Compare("PasswordNew", ErrorMessage = "Mật khẩu xác nhận không chính xác!")]
        public string ConfirmPassword { get; set; }
    }
}