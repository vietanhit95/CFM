using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel
{
    public class ReportConfigPo
    {
        public int Id { get; set; }
        public int ReportItemId { get; set; }
        public int PoId { get; set; }
        public int ReportId { get; set; }
        public int ItemId { get; set; }
        public string DisplayName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ReportCode { get; set; }
        public string ReportName { get; set; }
    }

    public class ReportPoSubmit
    {
        public string DelString { get; set; }
        public string AddString { get; set; }
    }
}