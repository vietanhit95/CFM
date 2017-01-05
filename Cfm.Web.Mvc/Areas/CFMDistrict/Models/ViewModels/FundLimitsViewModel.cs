using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.CFMDistrict.Models.ViewModels
{
    public class FundLimitsViewModel
    {
        public int Id { get; set; }
        [Range(1,Double.MaxValue,ErrorMessage="Mã Đơn vị không chính xác!")]
        public int PoId { get; set; }
        public long DinhMucDuocGiao { get; set; }
        public long K1 { get; set; }
        public long K2 { get; set; }
        public long K3 { get; set; }
        public long ThuTB { get; set; }
        public long ChiTB { get; set; }
        public int Thang { get; set; }
        [Required(ErrorMessage="Năm không chính xác!")]
        public int Nam { get; set; }
        public long DinhMucDeXuat { get; set; }
        [Required(ErrorMessage = "Mã Đơn vị không chính xác!")]
        public string PoCode { get; set; }
        [Required(ErrorMessage = "Mã Đơn vị không chính xác!")]
        public string PoName { get; set; }


        [Required(ErrorMessage="Định mức được giao không đúng!")]
        public string DinhMucDuocGiaoString { get; set; }

        [Required(ErrorMessage = "Hệ số K1 không đúng!")]
        public string K1String { get; set; }

        [Required(ErrorMessage = "Hệ số K2 không đúng!")]
        public string K2String { get; set; }

        [Required(ErrorMessage = "Hệ số K3 không đúng!")]
        public string K3String { get; set; }

        [Required(ErrorMessage = "Thu TB không đúng!")]
        public string ThuTBString { get; set; }

        [Required(ErrorMessage = "Chi TB không đúng!")]
        public string ChiTBString { get; set; }

        [Required(ErrorMessage = "Định mức đề xuất không đúng!")]
        public string DinhMucDeXuatString { get; set; }
    }
}