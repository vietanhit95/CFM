using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMReport.Models
{
    public class R_03THTotal
    {
        public int Group_type { get; set; }
        public string From_date { get; set; }
        public string To_date { get; set; }
        public string Po_Code { get; set; }
        public string Po_Name { get; set; }
        public string Parent_Code { get; set; }
        public string Parent_Name { get; set; }
        public int font_bold { get; set; }
        public int level_view { get; set; }
        public string CFM_Code { get; set; }
        public string Item_name { get; set; }
        public long GT_VND { get; set; }
        public double GT_USD { get; set; }
        public long GT_VND_LK { get; set; }
        public double GT_USD_LK { get; set; }
        public double Ty_gia { get; set; }
        public double DANH_GIA_TANG { get; set; }
        public double DANH_GIA_GIAM { get; set; }
        public long PHAI_THU_CK { get; set; }
        public long PHAI_TRA_CK { get; set; }
    }
}