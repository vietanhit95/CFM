using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections;
namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class ReportConfigViewModel
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemCodeDisplay { get; set; }
        public string ItemName { get; set; }
        public int ParentId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Nhập vào Cấp hiển thị!")]
        [DisplayName("Cấp hiển thị")]
        public int VisibleLevel { get; set; }
        [Range(1,int.MaxValue,ErrorMessage = "Nhập vào Vị trí hiển thị!")]
        [DisplayName("STT hiển thị")]
        public int VisibleIndex { get; set; }
        public bool IsVisible { get; set; }
        public int ItemGroupId { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên nhập liệu!")]
        [DisplayName("Tên hiển thị")]
        public string DisplayName { get; set; }
        public bool IsEmpty { get; set; }
        [DisplayName("Chữ đậm?")]
        public bool FontBold { get; set; }
        [DisplayName("Kiểu hiển thị")]
        public int EditMode { get; set; }
        public bool AllowSummary { get; set; }
        public bool AllowVnd { get; set; }
        public bool AllowUsd { get; set; }
        public bool AccumulateVnd { get; set; }
        public bool AllowAccumulate { get; set; }
        public bool AccumulateUsd { get; set; }
        public bool AllowShowReport { get; set; }
        public bool AllowSummaryBottom { get; set; }
        public bool DisableFormula { get; set; }
        public string InputType { get; set; }
        public List<ItemListViewModel> ListItem { get; set; }
    }
}