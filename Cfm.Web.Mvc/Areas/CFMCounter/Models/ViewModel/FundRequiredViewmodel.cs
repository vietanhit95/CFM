using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel
{
    public class FundRequiredViewmodel
    {
        public int Id { get; set; }
        public int CreatedEmpId { get; set; }
        public int CreatedPoId { get; set; }
        public string CreatedPoCode { get; set; }
        public string CreatedPoName { get; set; }
        public string CreatedDate { get; set; }
        public int ApprovedEmpId { get; set; }
        public int ApprovedPoId { get; set; }
        public string ApprovedPoCode { get; set; }
        public string ApprovedPoName { get; set; }
        public string ApprovedDate { get; set; }
        public decimal OpeningAmount { get; set; }
        public string OpeningAmountString { get; set; }
        public decimal ReceiveAmount { get; set; }
        public string ReceiveAmountString { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentAmountString { get; set; }
        public decimal ClosingAmount { get; set; }
        public string ClosingAmountString { get; set; }
        public string CurrencyType { get; set; }
        public decimal ExpectedAmount { get; set; }
        public string ExpectedAmountString { get; set; }
        public decimal NormAmount { get; set; }
        public string NormAmountString { get; set; }
        public decimal RecommentAmount { get; set; }
        public string RecommentAmountString { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string ApprovedAmountString { get; set; }
        public string Description { get; set; }
        public int CashFllowId { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
    }
}