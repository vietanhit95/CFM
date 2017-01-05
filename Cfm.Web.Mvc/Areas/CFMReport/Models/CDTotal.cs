using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMReport.Models
{
    public class CDTotal
    {
        public int Service_type { get; set; }
        public int Group_type { get; set; }
        public int Trans_type { get; set; }
        public string From_date { get; set; }
        public string To_date { get; set; }
        public string Po_Code { get; set; }
        public string Po_Name { get; set; }
        public string Parent_Code { get; set; }
        public string Parent_Name { get; set; }
        public int STT { get; set; }
        public int font_bold_ph { get; set; }
        public int level_ph { get; set; }
        public string CFM_Code_thu { get; set; }
        public string Ma_thu { get; set; }
        public string Ten_ma_thu { get; set; }
        public long thu_vnd { get; set; }
        public double thu_usd { get; set; }
        public long thu_qd_vnd { get; set; }
        public int font_bold_tra { get; set; }
        public int level_tra { get; set; }
        public string CFM_Code_chi { get; set; }
        public string Ma_chi { get; set; }
        public string Ten_ma_chi { get; set; }
        public long chi_vnd { get; set; }
        public double chi_usd { get; set; }
        public long chi_qd_vnd { get; set; }
        public double Ty_gia { get; set; }
        public long Chenh_lech_vnd { get; set; }
        public double Chenh_lech_usd { get; set; }        
        public long TDC_DK { get; set; }
        public double TDC_DK_USD { get; set; }
        public long TDC_CK { get; set; }
        public double TDC_CK_USD { get; set; }
        public long TDC_BDH_DK { get; set; }
        public double TDC_BDH_DK_USD { get; set; }
        public long TDC_BDH_CK { get; set; }
        public double TDC_BDH_CK_USD { get; set; }
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