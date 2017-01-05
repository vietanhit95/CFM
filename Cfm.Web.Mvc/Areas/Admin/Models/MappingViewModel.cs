using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Areas.Admin.Models
{
    public class MappingViewModel
    {
        public int Id { get; set; }
        public string Map_Code { get; set; }
        public int Item_Id { get; set; }
        public string Item_Code { get; set; }
        public string Item_Name { get; set; }
        public string Pa_Code { get; set; }
        public int Map_Type { get; set; }
    }
}