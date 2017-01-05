using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }

        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string Username { get; set; }

        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        public string FullName { get; set; }

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại Người dùng không được để trống.")]
        public string PhoneNumber { get; set; }

        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ Người dùng không được để trống.")]
        public string Address { get; set; }

        [DisplayName("Nhóm người dùng")]
        [Required(ErrorMessage = "Nhóm người dùng không được để trống.")]
        public int EmpGroupID { get; set; }

        [DisplayName("Nhóm người dùng")]
        public string EmpGroupName { get; set; }

        [DisplayName("Đơn vị")]
        [Required(ErrorMessage = "Đơn vị không được để trống.")]
        public int POID { get; set; }

        [DisplayName("Khóa")]
        public bool IsLock { get; set; }

        public string PoName { get; set; }

        public List<Cfm.Web.Mvc.Models.FunctionList> ListRole { get; set; }
    }
}