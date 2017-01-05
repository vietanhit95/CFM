using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMDistrict.Models.ViewModels
{
    public class CD03DetailViewModel
    {
        public int Id { get; set; }
        public string ReportDate { get; set; }
        public int PoId { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string Formula { get; set; }
        public string AmountVnd { get; set; }
        public string AmountUsd { get; set; }

        public decimal dAmountVnd { get; set; }
        public decimal dAmountUsd { get; set; }

        public int HeaderId { get; set; }
        public int ParentId { get; set; }
        public int ItemGroupId { get; set; }
        public bool IsVisible { get; set; }
        public string DisplayName { get; set; }
        public bool FontBold { get; set; }
        public int EditMode { get; set; }
        public bool AllowSummary { get; set; }
        public bool AllowVnd { get; set; }
        public bool AllowUsd { get; set; }
        public int VisibleLevel { get; set; }
        public bool AllowSummaryBottom { get; set; }
    }
}