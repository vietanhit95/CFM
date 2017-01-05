using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class ItemGroupViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Nhập vào Mã Dịch vụ!")]
        [StringLength(32,ErrorMessage = "Độ dài Mã Dịch vụ nằm trong khoảng [6,32] ký tự",MinimumLength = 6)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Nhập vào Tên Dịch vụ!")]
        [StringLength(150, ErrorMessage = "Độ dài Tên Dịch vụ nằm trong khoảng [6,150] ký tự", MinimumLength = 6)]
        public string Name { get; set; }
        public int VisibleIndex { get; set; }

        [Required(ErrorMessage = "Chọn Báo cáo!")]
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Chọn Dòng tiền!")]
        public int CashFllowId { get; set; }
    }
}