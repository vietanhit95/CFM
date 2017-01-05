using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class PostOfficeViewModel
    {
        public int Id { get; set; }

        [DisplayName("Đơn vị quản lý")]
        public int ParentId { get; set; }

        [DisplayName("Mã đơn vị")]
        public string Code { get; set; }

        [DisplayName("Tên đơn vị")]
        public string Name { get; set; }

        [DisplayName("Cấp đơn vị")]
        public int POLevel { get; set; }

        [DisplayName("Trung tâm")]
        public string IsCenter { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Fax")]
        public string FaxNumber { get; set; }

        [DisplayName("Không kết nối mạng")]
        public string IsOffline { get; set; }

        [DisplayName("Chu kỳ")]
        public int CycleDate { get; set; }

        [DisplayName("Khóa")]
        public string IsLock { get; set; }
    }

    public class POTreeViewModel
    {
        public POTreeViewModel()
        {
            this.Childs = new List<POTreeViewModel>();
        }

        public string text { get; set; }
        public string icon { get; set; }
        public string href { get; set; }
        public string tags { get; set; }

        [JsonProperty("nodes")]
        public List<POTreeViewModel> Childs { get; set; }

        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int ParentId { get; set; }

        [JsonIgnore]
        public int POLevel { get; set; }
    }

    public class PostOfficePartialViewModel
    {
        public int Id { get; set; }

        [DisplayName("Đơn vị quản lý")]
        public int ParentId { get; set; }

        [DisplayName("Mã đơn vị")]
        public string Code { get; set; }

        [DisplayName("Tên đơn vị")]
        public string Name { get; set; }

        [DisplayName("Cấp đơn vị")]
        public string POLevel { get; set; }

        [DisplayName("Trung tâm")]
        public bool IsCenter { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Fax")]
        public string FaxNumber { get; set; }

        [DisplayName("Không kết nối mạng")]
        public bool IsOffline { get; set; }

        [DisplayName("Chu kỳ")]
        public int CycleDate { get; set; }

        [DisplayName("Khóa")]
        public bool IsLock { get; set; }
    }

    public class PostOfficeDetailViewModel
    {
        public PostOfficeDetailViewModel()
        {
            this.Childs = new List<PostOfficeDetailViewModel>();
        }
        public int Id { get; set; }

        [DisplayName("Đơn vị quản lý")]
        public int ParentId { get; set; }

        [DisplayName("Mã đơn vị")]
        public string Code { get; set; }

        [DisplayName("Tên đơn vị")]
        public string Name { get; set; }

        [DisplayName("Cấp đơn vị")]
        public string POLevel { get; set; }

        [DisplayName("Trung tâm")]
        public bool IsCenter { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Fax")]
        public string FaxNumber { get; set; }

        [DisplayName("Không kết nối mạng")]
        public bool IsOffline { get; set; }

        [DisplayName("Chu kỳ")]
        public int CycleDate { get; set; }

        [DisplayName("Khóa")]
        public bool IsLock { get; set; }

        [DisplayName("Danh sách đơn vị cấp dưới")]
        public List<PostOfficeDetailViewModel> Childs { get; set; }
    }
}