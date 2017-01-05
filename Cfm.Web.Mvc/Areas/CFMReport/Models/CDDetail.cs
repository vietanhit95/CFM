using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMReport.Models
{
    public class CDDetail
    {
        public int Group_Type { get; set; }
        public int STT { get; set; }
        public int Font_Bold { get; set; }
        public int Cap_hien_thi { get; set; }
        public string CFM_Code { get; set; }
        public string Item_Code { get; set; }
        public string Item_Name { get; set; }
        public string Po_Code { get; set; }
        public string Po_Name { get; set; }
        public string Parent_Code { get; set; }
        public string Parent_Name { get; set; }
        public long GT_Thu { get; set; }
        public long GT_Thu_Usd { get; set; }
        public long DC_Thu { get; set; }
        public long DC_Thu_Usd { get; set; }
        public long GT_Tra { get; set; }
        public long GT_Tra_Usd { get; set; }
        public long DC_Tra { get; set; }
        public long DC_Tra_Usd { get; set; }
        public long Tong_Thu { get; set; }
        public long Tong_Thu_Usd { get; set; }
        public long Tong_Tra { get; set; }
        public long Tong_Tra_Usd { get; set; }        
        public long TDC_DK { get; set; }
        public double TDC_DK_USD { get; set; }
        public long TDC_CK { get; set; }
        public double TDC_CK_USD { get; set; }
        public string ADD_INFO_01 { get; set; }
        public string ADD_INFO_02 { get; set; }
        public string ADD_INFO_03 { get; set; }
        public string ADD_INFO_04 { get; set; }
        public string ADD_INFO_05 { get; set; }
        public long FEE_ADD_INFO_01 { get; set; }
        public long FEE_ADD_INFO_02 { get; set; }
        public long FEE_ADD_INFO_03 { get; set; }
        public double FEE_ADD_INFO_04 { get; set; }
        public double FEE_ADD_INFO_05 { get; set; }
    }
}