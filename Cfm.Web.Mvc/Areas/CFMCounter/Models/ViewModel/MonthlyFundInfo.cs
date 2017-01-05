using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel
{
    public class MonthlyFundInfo
    {
        public int Id { get; set; }
        public int AmndEmp { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public long AmountVND { get; set; }
        public long AmountUSD { get; set; }
        public long AmountBankVND { get; set; }
        public long AmountBankUSD { get; set; }
        public long AmountBorrowFund { get; set; }
        public long AmountLoanFund { get; set; }
        public long AmountTransferVND { get; set; }
        public long AmountTransferUSD { get; set; }

        public string StringAmountVND { get; set; }
        public string StringAmountUSD { get; set; }
        public string StringAmountBankVND { get; set; }
        public string StringAmountBankUSD { get; set; }
        public string StringAmountBorrowFund { get; set; }
        public string StringAmountLoanFund { get; set; }
        public string StringAmountTransferVND { get; set; }
        public string StringAmountTransferUSD { get; set; }
        public int PoId { get; set; }
        public string PoCode { get; set; }
        public string PoName { get; set; }
        public int PoLevel { get; set; }
        public int CashFllowId { get; set; }
    }
}