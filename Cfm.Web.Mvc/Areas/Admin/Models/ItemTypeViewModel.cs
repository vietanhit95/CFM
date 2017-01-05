using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class ItemTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nhập vào Mã loại chỉ tiêu!")]
        [StringLength(32, ErrorMessage = "Độ dài tối đa là [32] ký tự", MinimumLength = 1)]
        [DisplayName("Mã Loại chỉ tiêu")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Nhập vào Tên loại chỉ tiêu!")]
        [StringLength(150, ErrorMessage = "Độ dài tối đa là [100] ký tự", MinimumLength = 1)]
        [DisplayName("Tên Loại chỉ tiêu")]
        public string Name { get; set; }       
        [DisplayName("Số thứ tự hiển thị")]
        public int VisibleIndex { get; set; }
        public int EmpId { get; set; }
    }
}