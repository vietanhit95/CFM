using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class NotifiCationViewModels
    {
        public int Id { get; set; }
        public string CreateDate { get; set; }
        public int SendPoId { get; set; }
        public string SendPoCode { get; set; }
        public string SendPoName { get; set; }
        public int ReceivePoId { get; set; }
        public string ReceivePoCode { get; set; }
        public string ReceivePoName { get; set; }
        public string Description { get; set; }
        public long FundLimit { get; set; }
        public long FundRedundance { get; set; }
        public long PassLimits { get; set; }
        public string DescriptionRes { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string IsReaded { get; set; }
        public long Amount { get; set; }
        public int RefId { get; set; }

    }
}