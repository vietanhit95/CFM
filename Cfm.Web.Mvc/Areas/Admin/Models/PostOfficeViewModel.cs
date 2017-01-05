using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class PostOfficeViewModel
    {
        public int ID { get; set; }

        [DisplayName("Đơn vị quản lý")]
        public int ParentID { get; set; }

        [DisplayName("Mã đơn vị")]
        [Required(ErrorMessage="Mã đơn vị không được để trống!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mã đơn vị chỉ được phép nhập chữ số!")]
        [StringLength(6,ErrorMessage="Nhập vào 6 ký tự của mã đơn vị!",MinimumLength=6)]       
        public string Code { get; set; }

        [DisplayName("Tên đơn vị")]
        [Required(ErrorMessage="Tên đơn vị không được để trống!")]
        [StringLength(100,ErrorMessage="Tên đơn vị không được quá 100 ký tự")]
        public string Name { get; set; }

        [DisplayName("Cấp đơn vị")]
        public int POLevel { get; set; }

        [DisplayName("Trung tâm")]
        public bool IsCenter { get; set; }

        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ đơn vị không được để trống!")]
        [StringLength(1000, ErrorMessage = "Địa chỉ đơn vị không được quá 1000 ký tự")]
        public string Address { get; set; }

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại đơn vị không được để trống!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại chỉ được phép nhập chữ số!")]
        [StringLength(32, ErrorMessage = "Số điện thoại đơn vị không được quá 32 ký tự")]        
        public string PhoneNumber { get; set; }

        [DisplayName("Fax")]       
        [Required(ErrorMessage = "Số fax đơn vị không được để trống!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Số fax chỉ được phép nhập chữ số!")]
        [StringLength(32, ErrorMessage = "Số điện thoại đơn vị không được quá 32 ký tự")]        
        public string FaxNumber { get; set; }

        [DisplayName("Không kết nối mạng")]
        public bool IsOffline { get; set; }

        [DisplayName("Chu kỳ")]
        public int CycleDate { get; set; }

        [DisplayName("Khóa")]
        public bool IsLock { get; set; }
    }

    public class POTreeViewModel
    {
        public POTreeViewModel()
        {
            this.childs = new List<POTreeViewModel>();
        }

        public string text { get; set; }
        public string icon { get; set; }
        public string href { get; set; }
        public string tags { get; set; }

        [JsonProperty("nodes")]
        public List<POTreeViewModel> childs { get; set; }

        [JsonIgnore]
        public int id { get; set; }

        [JsonIgnore]
        public int parentId { get; set; }

        [JsonIgnore]
        public int poLevel { get; set; }
    }
}