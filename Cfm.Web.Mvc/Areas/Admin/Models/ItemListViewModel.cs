using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class ItemListViewModel
    {
        [Range(0,int.MaxValue,ErrorMessage = "Định dạng trường dữ liệu không chính xác!")]
        public Nullable<int> Id { get; set; }
        [Required(ErrorMessage = "Nhập vào Mã Chỉ Tiêu!")]
        [StringLength(32,ErrorMessage = "Độ dài Mã chỉ tiêu nằm trong khoảng [6,32] ký tự!", MinimumLength = 6)]
        public string Code { get; set; }

        public string CodeDisplay { get; set; }

        [Required(ErrorMessage = "Nhập vào Tên Chỉ Tiêu!")]
        [StringLength(120, ErrorMessage = "Độ dài Tên chỉ tiêu nằm trong khoảng [6,150] ký tự!", MinimumLength = 6)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Chọn kiểu Dịch vụ!")]
        public string IsReceiptItem { get; set; }

        [Required(ErrorMessage = "Chọn kiểu Tiền!")]
        public int MoneyType { get; set; }

        [Required(ErrorMessage = "Chọn loại Chỉ Tiêu!")]
        public int ItemTypeId { get; set; }
        public string IsSummary { get; set; }
        public bool bIsSummary { get; set; }

        //[Required(ErrorMessage = "Nhập vào Công thức Chỉ Tiêu!")]
        public string Formula { get; set; }
        public int ParentId { get; set; }

        //[Required(ErrorMessage = "Chọn Đối tác!")]
        public int PartnerId { get; set; }

        //[Required(ErrorMessage = "Chọn Loại quỹ!")]
        public int BudgetTypeId { get; set; }

        //[Required(ErrorMessage = "Chọn Loại Vay trả!")]
        public int BorrowTypeId { get; set; }

        [Required(ErrorMessage = "Chọn Hình thức Phân loại!")]
        public int CashFlowId { get; set; }
        public string Description { get; set; }

        
        [StringLength(32,ErrorMessage="Mã CFM không được vượt quá 32 ký tự")]
        public string Cfm_Code { get; set; }

        public bool Selected { get; set; }
    }
}