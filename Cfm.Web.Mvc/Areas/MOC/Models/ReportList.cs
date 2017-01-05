using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.MOC.Models
{
    public class ReportList
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        [DisplayName("ID báo cáo")]
        public int ID { get; set; }
        [DisplayName("Mã báo cáo")]
        public string Code { get; set; }
        [DisplayName("Tên báo cáo")]
        public string Name { get; set; }
        [DisplayName("Ghi chú")]
        public string Description { get; set; }
    }
}