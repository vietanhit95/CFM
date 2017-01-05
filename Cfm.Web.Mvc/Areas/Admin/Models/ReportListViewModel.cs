using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class ReportListViewModel
    {
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        [DisplayName("ID báo cáo")]
        public int Id { get; set; }
        public int EmpID { get; set; }
        [DisplayName("Mã báo cáo")]
        public string Code { get; set; }
        [DisplayName("Tên báo cáo")]
        public string Name { get; set; }
        [DisplayName("Ghi chú")]
        public string Description { get; set; }
        [DisplayName("Trung tâm")]
        public bool On_Moc { get; set; }
        [DisplayName("Tỉnh")]
        public bool On_Province_PO { get; set; }
        [DisplayName("Huyện")]
        public bool On_District_PO { get; set; }
        [DisplayName("Bưu cục")]
        public bool On_PO { get; set; }
        [DisplayName("Cho phép tạo bút toán")]
        public bool AllowCreateEntry { get; set; }
        public string OfficeManage { get; set; }
        [DisplayName("Loại báo cáo")]
        public string ReportType { get; set; }
    }
}