using Cfm.Web.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel
{
    public class AdjustmentEntryViewModel
    {
        public int AmndEmpId { get; set; }
        public string AmndEmpName { get; set; }
        public int Id { get; set; }
        public int PoId { get; set; }
        public string PoCode { get; set; }
        public string PoName { get; set; }
        public string CreatedDate { get; set; }
        public string ReportDate { get; set; }
        public int AdjType { get; set; }
        public string AdjTypeName { get; set; }
        public decimal AdjAmountVnd { get; set; }
        public decimal AdjAmountUsd { get; set; }
        public bool IsReceiptItem { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int ReportId { get; set; }
        public string StringAmountVnd { get; set; }
        public string StringAmountUsd { get; set; }

        public List<ItemListViewModel> ListItem { get; set; }
    }
}