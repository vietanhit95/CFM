using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMReport.Models
{
    public class R_03TH_KDDV
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
        public string Item_name { get; set; }
        public long GT_VND_DK { get; set; }
        public double GT_USD_DK { get; set; }
        public long GT_VND_TANG { get; set; }
        public double GT_USD_TANG { get; set; }
        public long GT_VND_GIAM { get; set; }
        public double GT_USD_GIAM { get; set; }
        public long GT_VND_CK { get; set; }
        public double GT_USD_CK { get; set; }
        public double Ty_gia { get; set; }
    }
}