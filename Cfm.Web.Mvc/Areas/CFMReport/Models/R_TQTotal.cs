using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMReport.Models
{
    public class R_TQTotal
    {
        public int STT { get; set; }
        public int PO_TYPE { get; set; }
        public string PO_CODE { get; set; }
        public string PO_NAME { get; set; }
        public string PARENT_CODE { get; set; }
        public string PARENT_NAME { get; set; }
        public int TRANS_TYPE { get; set; }
        public long TM_DAU_NGAY { get; set; }
        public long TGNH_DAU_NGAY { get; set; }
        public long TIEN_THU { get; set; }
        public long TIEN_CHI { get; set; }
        public long VAY_QUY_KHAC { get; set; }
        public long QUY_KHAC_VAY { get; set; }
        public long SO_DA_NHAN { get; set; }
        public long SO_DA_NOP { get; set; }
        public long SO_DU_CUOI_NGAY { get; set; }
        public long SO_DU_TGNH_CUOI_NGAY { get; set; }
        public long SO_DU_CHI { get; set; }
        public long HAN_MUC_LUU { get; set; }
        public long SO_CAN_TIEP_NOP { get; set; }
    }
}