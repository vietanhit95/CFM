using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel
{
    public class TQ04HeaderViewModel
    {
        public int Id { get; set; }
        public string ReportDate { get; set; }
        public int PoId { get; set; }
        public string PoCode { get; set; }
        public string PoName { get; set; }
        public int DistrictPoId { get; set; }
        public string DistrictPoCode { get; set; }
        public string DistrictPoName { get; set; }
        public int AmndEmpId { get; set; }
        public string AmndEmpName { get; set; }
        public string CreatedDate { get; set; }
        public List<TQ04DetailViewModel> ListDetail { get; set; }
    }
}