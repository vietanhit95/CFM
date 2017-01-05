using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel
{
    public class InfomationNotifiViewModels
    {
        public decimal OpeningAmount { get; set; }
        public decimal ReceipAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal ClosingAmount { get; set; }
        public decimal OpeningAmountPo { get; set; }
        public decimal OpeningAmountSpo { get; set; }
    }
}