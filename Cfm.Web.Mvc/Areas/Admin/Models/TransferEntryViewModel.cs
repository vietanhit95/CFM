using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class TransferEntryViewModel
    {
        public int Id { get; set; }
        public int TransferType { get; set; }
        public string TransferTypeName { get; set; }
        public int SourceItemId { get; set; }
        public int TargetItemId { get; set; }
        public string ReportType { get; set; }
        public string SourceItemCode { get; set; }
        public string SourceItemName { get; set; }
        public string TargetItemCode { get; set; }
        public string TargetItemName { get; set; }
    }
}