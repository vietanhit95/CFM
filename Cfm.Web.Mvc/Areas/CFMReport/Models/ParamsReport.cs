using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMReport.Models
{
    public class ParamsReport
    {
        public string Report_code { get; set; }
        public string File_name { get; set; }
        public int ViewType { get; set; } // Kiểu xem: 0: TH, 1: CT
        public int Term_id { get; set; } //0:Ngày; 1:tháng; 2:Quý; 3: từ ngày đến ngày
        public int Month_id { get; set; }
        public int Year_id { get; set; }
        public string From_date { get; set; }
        public string To_date { get; set; }
        public int Po_ID { get; set; }
        public int Tab_Indx { get; set; }
    }
}