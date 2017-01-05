using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class ListEmployeePartial
    {
        public int Id { get; set; }

        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; }

        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [DisplayName("Họ tên")]
        public string FullName { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }        

        [DisplayName("Nhóm người dùng")]
        public int EmpGroupId { get; set; }

        [DisplayName("Nhóm người dùng")]
        public string EmpGroupName { get; set; }

        [DisplayName("Đơn vị")]
        public int POId { get; set; }

        [DisplayName("Khóa")]
        public bool IsLock { get; set; }
    }

    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string Username { get; set; }

        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        public string FullName { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Nhóm người dùng")]
        [Required(ErrorMessage = "Nhóm người dùng không được để trống.")]
        public int EmpGroupId { get; set; }

        [DisplayName("Nhóm người dùng")]
        public string EmpGroupName { get; set; }

        [DisplayName("Đơn vị")]
        [Required(ErrorMessage = "Đơn vị không được để trống.")]
        public int POId { get; set; }

        [DisplayName("Khóa")]
        public bool IsLock { get; set; }
    }

    public class EmpGroupViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public int POLevel { get; set; }
    }

    public class POEmpViewModel
    {
        public POEmpViewModel()
        {
            this.PO = new PostOfficeViewModel();
            this.Employees = new List<EmployeeViewModel>();
        }
        [DisplayName("Thông tin đơn vị")]
        public PostOfficeViewModel PO { get; set; }

        [DisplayName("Danh sách người dùng")]
        public List<EmployeeViewModel> Employees { get; set; }
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
    }
}