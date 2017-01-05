using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel
{
    public class FundInfo
    {
        public int Id { get; set; }
        public int AmndEmp { get; set; }
        public int Fund_Day { get; set; }
        public int Fund_Month { get; set; }
        public int Fund_Year { get; set; }
        public int PoId { get; set; }
        public int Po_Parent_Id { get; set; }
        public int Fund_Type { get; set; }
        public int Fund_Level { get; set; }
        public long AmountVND { get; set; }
        public long AmountUSD { get; set; }
        public long AmountBankVND { get; set; }
        public long AmountBankUSD { get; set; }
        public long AmountBorrowVND { get; set; }
        public long AmountBorrowUSD { get; set; }
        public long AmountLoanVND { get; set; }
        public long AmountLoanUSD { get; set; }
        public long AmountTransferVND { get; set; }
        public long AmountTransferUSD { get; set; }
        public string Is_Lock { get; set; }

        public string StringAmountVND { get; set; }
        public string StringAmountUSD { get; set; }
        public string StringAmountBankVND { get; set; }
        public string StringAmountBankUSD { get; set; }
        public string StringAmountBorrowVND { get; set; }
        public string StringAmountBorrowUSD { get; set; }
        public string StringAmountLoanVND { get; set; }
        public string StringAmountLoanUSD { get; set; }
        public string StringAmountTransferVND { get; set; }
        public string StringAmountTransferUSD { get; set; }
    }
}