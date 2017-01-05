using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMReport.Models
{
    public class R_FundInfo
    {
        public int ID { get; set; }
        public int PO_TYPE { get; set; }
        public string PO_CODE { get; set; }
        public string PO_NAME { get; set; }
        public string PARENT_CODE { get; set; }
        public string PARENT_NAME { get; set; } 
        public long FUND_TT { get; set; }
        public long FUND_TDV { get; set; }
        public long FUND_KD { get; set; }
        public long FUND_TKBD { get; set; }
        public long FUND_TOTAL { get; set; }
        public long FUND_NEED { get; set; }
        public long FUND_NEED_TKBD { get; set; }
    }
}