using Cfm.Web.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc
{
    public class Repository
    {
        public static Dictionary<int, string> POLevel = new Dictionary<int, string>()
        {
            { 1, "Đơn vị đầu mối" },
            { 2, "Bưu điện Tỉnh" },
            { 3, "Bưu điện Huyện" },
            { 4, "Bưu cục" }
        };
    }
}