using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMCounter.Models.ViewModel
{
    public class CD04HeaderViewModel
    {
        public int Id { get; set; }
        public string ReportDate { get; set; }
        public int PoId { get; set; }
        public string PoCode { get; set; }
        public string PoName { get; set; }
        public int PoApprovedId { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedDate { get; set; }
        public int ApprovedEmpId { get; set; }
        public string CreatedDate { get; set; }
        public List<CD04DetailViewModel> ListDetail { get; set; }
        [DisplayFormat(DataFormatString = "{d:2}")]
        public decimal TotalAmountVnd { get; set; }

        public decimal TotalAmountUsd { get; set; }
        public string ReportStatus { get; set; }
        public string ReportStatusName { get; set; }
        public string ApprovedEmpName { get; set; }
        public string IsApprovedName { get; set; }
        public int AdjEntryId { get; set; }
    }
}