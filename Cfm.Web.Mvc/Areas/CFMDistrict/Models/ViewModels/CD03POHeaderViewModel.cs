using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMDistrict.Models.ViewModels
{
    public class CD03POHeaderViewModel
    {
        public int Id { get; set; }
        public string ReportDate { get; set; }
        public int PoId { get; set; }
        public string PoCode { get; set; }
        public string PoName { get; set; }
        public int PoDistrictId { get; set; }
        public string CreatedDate { get; set; }
        public List<CD03PODetailViewModel> ListDetail { get; set; }      
        public decimal TotalAmountVnd { get; set; }
        public decimal TotalAmountUsd { get; set; }
     
    }
}