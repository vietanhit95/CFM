
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Models
{
    public class CFMReport
    {
        public int Service_type { get; set; }
        public int Group_type { get; set; }
        public int Trans_type { get; set; }
        public string From_date { get; set; }
        public string To_date { get; set; }
        public string Po_Code { get; set; }
        public string Po_Name { get; set; }
        public int font_bold { get; set; }
        public string Ma_thu { get; set; }
        public decimal thu_vnd { get; set; }
        public double thu_usd { get; set; }
        public decimal thu_qd_vnd { get; set; }
        public string Ma_chi { get; set; }
        public decimal chi_vnd { get; set; }
        public double chi_usd { get; set; }
        public decimal chi_qd_vnd { get; set; }
        public double Ty_gia { get; set; }
    }
}