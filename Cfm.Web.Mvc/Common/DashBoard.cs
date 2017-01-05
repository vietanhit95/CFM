using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Common
{
    public static class DashBoard
    {
        public const string DASHBOARD_NOTIFI_STATUS = "Trạng thái xử lý";
        public const string DASHBOARD_NOTIFI_STATUS_PASS = "Đã xử lý";
        public const string DASHBOARD_NOTIFI_STATUS_NOTPASS = "Chưa xử lý";
        public const string DASHBOARD_NOTIFI_STATUS_PASS_FUN= "Đã xác nhận";
        public const string DASHBOARD_NOTIFI_STATUS_NOTPASS_FUN = "Chưa xác nhận";
        public const string DASHBOARD_NOTIFI_STATUS_NOTPASS_FUN_1 = "Chưa lập báo cáo";
        public const string DASHBOARD_NOTIFI_TYPE = "Loại cảnh báo";
        public const string DASHBOARD_NOTIFI_TYPE_1 = "Vượt hạn mức lưu quỹ";
        public const string DASHBOARD_NOTIFI_TYPE_2 = "Tiếp nộp quỹ";
    }
}