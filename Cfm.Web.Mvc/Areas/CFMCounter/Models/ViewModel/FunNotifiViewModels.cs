using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel
{
    public class FunNotifiViewModels
    {
        public int Id { get; set; }
        public int poId { get; set; }
        public int poCode { get; set; }
        public string poName { get; set; }
        public int poApprovedId { get; set; }
        public bool isApproved { get; set; }
        public int ApprovedEmpId { get; set; }
        public string statusName { get; set; }
        public string reportStatus { get; set; }
        public string CreateDate { get; set; }
        public string ListValue { get; set; }
        public string ApprovedName { get; set; }
        public string isApproveName { get; set; }
        public int AdjEntryId { get; set; }
        public int AmndEmpId { get; set; }
        public int IsReplace { get; set; }
    }
}