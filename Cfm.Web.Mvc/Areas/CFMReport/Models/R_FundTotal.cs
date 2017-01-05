using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMReport.Models
{
    public class R_FundTotal
    {
        public string PO_CODE { get; set; }
        public string PO_NAME { get; set; }
        public string PARENT_CODE { get; set; }
        public string PARENT_NAME { get; set; }
        public string REPORT_DATE { get; set; }
        public string TRANS_DATE { get; set; }
        public string REPORT_TYPE { get; set; }        
        public long HAN_MUC { get; set; }
        public long TIEN_LUU_QUY { get; set; }
        public long VUOT_HAN_MUC { get; set; }   
        public long DOANH_THU { get; set; }
        public long SO_DE_XUAT { get; set; }
        public long SO_DUYET { get; set; }
        public long SO_CHUA_DUYET { get; set; }
        public string CONTENT { get; set; }


    }
}