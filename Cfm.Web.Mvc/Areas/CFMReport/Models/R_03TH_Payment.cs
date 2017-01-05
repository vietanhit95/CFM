using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMReport.Models
{
    public class R_03TH_Payment
    {
        public int Group_type { get; set; }
        public string From_date { get; set; }
        public string To_date { get; set; }
        public string Po_Code { get; set; }
        public string Po_Name { get; set; }
        public string Parent_Code { get; set; }
        public string Parent_Name { get; set; }       
        public long PHAI_TRA_DK { get; set; }
        public long PHAI_THU_DK { get; set; }
        public long SO_THU_HO { get; set; }
        public long SO_CHI_HO { get; set; }
        public long CHENH_LECH { get; set; } 
        public long DA_THANH_TOAN { get; set; }
        public long SO_DA_NHAN { get; set; }
        public long CON_PHAI_TRA { get; set; }
        public long CON_PHAI_THU { get; set; }
        public double Ty_gia { get; set; }
        public string GHI_CHU { get; set; }
    }
}