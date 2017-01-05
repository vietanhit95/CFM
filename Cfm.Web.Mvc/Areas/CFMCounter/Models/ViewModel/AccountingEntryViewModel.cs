using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel
{
    public class AccountingEntryViewModel
    {
        [Required(ErrorMessage="STT không được để trống!")]
        [Range(1,int.MaxValue,ErrorMessage="STT không hợp lệ!")]
        public int STT { get; set; }
        public string OrdinalNumberString { get; set; }
        public int OrdinalNumber { get; set; }
        public int Id { get; set; }
        public string TransNumber{ get; set; }
        public int AmndEmpId { get; set; }
        public int PoId { get; set; }
        public string PoCode { get; set; }
        public string PoName { get; set; }
        public int CreatedPoId { get; set; }
        public string CreatedPoCode { get; set; }
        public string CreatedPoName { get; set; }
        public int SendPoId { get; set; }
        public string SendPoCode { get; set; }
        public string SendPoName { get; set; }
        public int ReceivePoId { get; set; }
        public string ReceivePoCode { get; set; }
        public string ReceivePoName { get; set; }
        public int RefType { get; set; }
        public string TransDate { get; set; }
        public string RefTransNumber { get; set; }
        public decimal AmountVnd { get; set; }
        public decimal AmountUsd { get; set; }

        [Required(ErrorMessage = "Số tiền không được để trống!")]
        [MinLength(10,ErrorMessage="Độ dài không chính xác!")]
        public string Amount { get; set; }
        public int BudgetTypeId { get; set; }
        public int CashFllowId { get; set; }
        public string Description { get; set; }
        public string CurrencyType { get; set; }
        public string BudgetTypeName { get; set; }
        public string AmountInWord { get; set; }
        public bool Selected { get; set; }
        public decimal PaymentAmountVnd { get; set; }
        public decimal PaymentAmountUsd { get; set; }
        public int PoIdTemp1 { get; set; }
        public int PoIdTemp2 { get; set; }
        public decimal AmountUnitVnd { get; set; }
        public decimal AmountUnitUsd { get; set; }

        [Required(ErrorMessage="Số tiền không được để trống!")]
        [MinLength(10, ErrorMessage = "Độ dài không chính xác!")]
        public string AmountUnitString { get; set; }
        public string CurrencyTypeUnit { get; set; }
        public decimal AmountSavingVnd { get; set; }
        public decimal AmountSavingUsd { get; set; }

        [Required(ErrorMessage = "Số tiền không được để trống!")]
        [CustomMoneyValidation]
        public string AmountSavingString { get; set; }
        public string CurrencyTypeSaving { get; set; }
        public decimal AmountBSVnd { get; set; }
        public decimal AmountBSUsd { get; set; }

        [Required(ErrorMessage = "Số tiền không được để trống!")]
        [CustomMoneyValidation]
        public string AmountBSString { get; set; }
        public string CurrencyTypeBS { get; set; }
        public bool IsLvp { get; set; }
        public string BorrowMethod { get; set; }
        public string BorrowMethodUnit { get; set; }
        public string BorrowMethodSaving { get; set; }
        public string BorrowMethodBS { get; set; }
    }
}