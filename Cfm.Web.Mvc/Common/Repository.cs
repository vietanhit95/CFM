using Cfm.Web.Common;
using Cfm.Web.Log;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Cfm.Web.Mvc.Common
{
    public class Repository
    {
        public static Dictionary<int, FunctionModel> DicFuns = null;

        public static Dictionary<int, string> POLevel = new Dictionary<int, string>()
        {
            { 1, "Đơn vị đầu mối" },
            { 2, "Bưu điện Tỉnh" },
            { 3, "Bưu điện Huyện" },
            { 4, "Bưu cục" }
        };

        public static Dictionary<string, string> ListReceiptItem = new Dictionary<string, string>()
        {
            { "Y", "Thu" },
            { "N", "Chi" }
        };

        public static Dictionary<int, string> ListMoneyType = new Dictionary<int, string>()
        {
            { 1, "Tiền mặt" },
            { 2, "Tiền gửi ngân hàng" },
            { 3, "Tiền USD" },
            { 4, "Tiền USD CK" },
            { 5, "Số dư vay TQK" }
        };

        public static Dictionary<int, string> ListBudgetType = new Dictionary<int, string>()
        {
            { 1, "Quỹ Kinh Doanh" },
            { 2, "Quỹ tiết kiệm Bưu điện" },
            { 3, "Quỹ dịch vụ TCBC" }
        };

        public static Dictionary<int, string> ListBorrowType = new Dictionary<int, string>()
        {
            { 1, "Vay quỹ khác" },
            { 2, "Trả quỹ khác" },
            { 3, "Quỹ khác vay" },
            { 4, "Quỹ khác trả" }
        };

        public static Dictionary<int, string> ListCashFlow = new Dictionary<int, string>()
        {
            { 1, "Tổng hợp" },
            { 2, "Dòng tiền TCBC tập trung" },
            { 3, "Dòng tiền TCBC tại đơn vị" },
            { 4, "Dòng tiền TKBĐ" },
            { 5, "Dòng tiền kinh doanh tại đơn vị" }

        };

        public static Dictionary<int, string> ListItemEditMode = new Dictionary<int, string>()
        {
            { 1, "Hiển thị, cho phép nhập" },
            { 2, "Hiển thị, không cho phép nhập" },
            { 3, "Không hiển thị" }
        };

        public static Dictionary<string, string> ListReportType = new Dictionary<string, string>()
        {
            { "04CD", "Báo cáo cân đối 04-CĐ" },
            { "03CD", "Báo cáo cân đối 03-CĐ" },
            { "03CD_DETAIL", "Báo cáo 03-CĐ chi tiết BC" },
            { "02CD", "Báo cáo cân đối 02-CĐ" },
            { "01CD", "Báo cáo cân đối 01-CĐ" },
            { "03TH", "Báo cáo tổng hợp 03-TH" },
            { "02TH", "Báo cáo tổng hợp 02-TH" },
            { "01TH", "Báo cáo tổng hợp 01-TH" },
            { "04TQ", "Báo cáo tiếp quỹ 04-TQ" },
            { "03TQ", "Báo cáo tiếp quỹ 03-TQ" },
            { "02TQ", "Báo cáo tiếp quỹ 02-TQ" },
        };

        public static Dictionary<int, string> ListAdjType = new Dictionary<int, string>()
        {
            { 1, "Bút toán tăng thu" },
            { 2, "Bút toán tăng thu TM, giảm chi dịch vụ" },
            { 3, "Bút toán tăng chi" },
            { 4, "Bút toán tăng chi TM, giảm thu dịch vụ" }
        };

        public static Dictionary<int, string> ListTransferType = new Dictionary<int, string>()
        {
            { 1, "Kết chuyển số dư" },
            { 2, "Kết chuyển dữ liệu báo cáo" }
          
        };

        public static Dictionary<string, string> ListReportStatus = new Dictionary<string, string>()
        {
            { "", "Tất cả" },
            { "C", "Chưa làm báo cáo" },
            { "L", "Chưa xác nhận" },
            { "A", "Đã xác nhận" }

        };

        public static PostOfficeViewModel GetPOByID(int id)
        {
            PostOfficeViewModel postOffice = null;
            try
            {
                var rs = Helper.Invoke("GET", string.Format("api/PostOffice/GetPOByID?id={0}", new object[]
                    {
                        id
                    }), null);
                if (rs.Code == "00")
                {
                    if (rs.Value != null)
                    {
                        dynamic dyn = rs.Value;

                        postOffice = new PostOfficeViewModel();
                        postOffice.ID = dyn.Id;
                        postOffice.ParentID = dyn.ParentId;
                        postOffice.Code = dyn.Code;
                        postOffice.Name = dyn.Name;
                        postOffice.POLevel = dyn.POLevel;
                        postOffice.IsCenter = dyn.IsCenter == "Y" ? true : false;
                        postOffice.Address = dyn.Address;
                        postOffice.PhoneNumber = dyn.PhoneNumber;
                        postOffice.FaxNumber = dyn.FaxNumber;
                        postOffice.IsOffline = dyn.IsOffline == "Y" ? true : false;
                        postOffice.CycleDate = dyn.CycleDate;
                        postOffice.IsLock = dyn.IsLock == "Y" ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogToFile(LogFileType.EXCEPTION, "Repository.GetPOByID: " + ex.Message);
            }
            return postOffice;
        }

        public static void SetFunction()
        {
            DicFuns = new Dictionary<int, FunctionModel>();

            #region Chức năng Bưu cục
            DicFuns.Add((int)Constant.Function.FunPO, new FunctionModel { FunctionID = (int)Constant.Function.FunPO, FunctionName = "Chức năng Bưu cục", ParentId = 0, Area = (int)Constant.Function.FunPO }); 
            
            //Hệ thống
            DicFuns.Add((int)Constant.Function.System, new FunctionModel { FunctionID = (int)Constant.Function.System, FunctionName = "Hệ thống", ParentId = (int)Constant.Function.FunPO, Area = (int)Constant.Function.FunPO }); 
            DicFuns.Add((int)Constant.Function.System_MonthlyFundInfo, new FunctionModel { FunctionID = (int)Constant.Function.System_MonthlyFundInfo, FunctionName = "Thông số quỹ đầu tháng", ParentId = (int)Constant.Function.System, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.System_MonthlyFundInfo_Update, new FunctionModel { FunctionID = (int)Constant.Function.System_MonthlyFundInfo_Update, FunctionName = "Cập nhật", ParentId = (int)Constant.Function.System_MonthlyFundInfo, Area = (int)Constant.Function.FunPO }); 
            
            //Tham số dịch vụ
            DicFuns.Add((int)Constant.Function.System_ReportConfigItemPO, new FunctionModel { FunctionID = (int)Constant.Function.System_ReportConfigItemPO, FunctionName = "Tham số dịch vụ", ParentId = (int)Constant.Function.System, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.System_ReportConfigItemPO_Update, new FunctionModel { FunctionID = (int)Constant.Function.System_ReportConfigItemPO_Update, FunctionName = "Cập nhật", ParentId = (int)Constant.Function.System_ReportConfigItemPO, Area = (int)Constant.Function.FunPO }); 

            //Quản lý Đơn vị
            DicFuns.Add((int)Constant.Function.System_PoManage, new FunctionModel { FunctionID = (int)Constant.Function.System_PoManage, FunctionName = "Quản lý đơn vị", ParentId = (int)Constant.Function.System, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.System_PoManage_Add, new FunctionModel { FunctionID = (int)Constant.Function.System_PoManage_Add, FunctionName = "Thêm mới", ParentId = (int)Constant.Function.System_PoManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.System_PoManage_Edit, new FunctionModel { FunctionID = (int)Constant.Function.System_PoManage_Edit, FunctionName = "Sửa", ParentId = (int)Constant.Function.System_PoManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.System_PoManage_Del, new FunctionModel { FunctionID = (int)Constant.Function.System_PoManage_Del, FunctionName = "Xóa", ParentId = (int)Constant.Function.System_PoManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.System_PoManage_Lock, new FunctionModel { FunctionID = (int)Constant.Function.System_PoManage_Lock, FunctionName = "Khóa", ParentId = (int)Constant.Function.System_PoManage, Area = (int)Constant.Function.FunPO }); 
            
            //Quản lý Người dùng
            DicFuns.Add((int)Constant.Function.System_UserManage, new FunctionModel { FunctionID = (int)Constant.Function.System_UserManage, FunctionName = "Quản lý Người dùng", ParentId = (int)Constant.Function.System, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.System_UserManage_Add, new FunctionModel { FunctionID = (int)Constant.Function.System_UserManage_Add, FunctionName = "Thêm mới", ParentId = (int)Constant.Function.System_UserManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.System_UserManage_Edit, new FunctionModel { FunctionID = (int)Constant.Function.System_UserManage_Edit, FunctionName = "Sửa", ParentId = (int)Constant.Function.System_UserManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.System_UserManage_Del, new FunctionModel { FunctionID = (int)Constant.Function.System_UserManage_Del, FunctionName = "Xóa", ParentId = (int)Constant.Function.System_UserManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.System_UserManage_Lock, new FunctionModel { FunctionID = (int)Constant.Function.System_UserManage_Lock, FunctionName = "Khóa", ParentId = (int)Constant.Function.System_UserManage, Area = (int)Constant.Function.FunPO }); 
            
            //Phân quyền
            DicFuns.Add((int)Constant.Function.System_RoleManage, new FunctionModel { FunctionID = (int)Constant.Function.System_RoleManage, FunctionName = "Phân quyền Người dùng", ParentId = (int)Constant.Function.System, Area = (int)Constant.Function.FunPO }); 

            //Chức năng Bưu cục
            DicFuns.Add((int)Constant.Function.FunctionPO, new FunctionModel { FunctionID = (int)Constant.Function.FunctionPO, FunctionName = "Chức năng Bưu cục", ParentId = (int)Constant.Function.FunPO, Area = (int)Constant.Function.FunPO }); 
            DicFuns.Add((int)Constant.Function.FunctionPO_Report04CD, new FunctionModel { FunctionID = (int)Constant.Function.FunctionPO_Report04CD, FunctionName = "Kiểm tra và xác nhận 04 CĐ", ParentId = (int)Constant.Function.FunctionPO, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FunctionPO_Report04CD_ViewTH, new FunctionModel { FunctionID = (int)Constant.Function.FunctionPO_Report04CD_ViewTH, FunctionName = "Xem 04 CĐ Tổng hợp", ParentId = (int)Constant.Function.FunctionPO_Report04CD, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FunctionPO_Report04CD_ViewCT, new FunctionModel { FunctionID = (int)Constant.Function.FunctionPO_Report04CD_ViewCT, FunctionName = "Xem 04 CĐ Chi tiết", ParentId = (int)Constant.Function.FunctionPO_Report04CD, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FunctionPO_Report04CD_Confirm, new FunctionModel { FunctionID = (int)Constant.Function.FunctionPO_Report04CD_Confirm, FunctionName = "Xác nhận", ParentId = (int)Constant.Function.FunctionPO_Report04CD, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FunctionPO_Report04CD_Update, new FunctionModel { FunctionID = (int)Constant.Function.FunctionPO_Report04CD_Update, FunctionName = "Cập nhật", ParentId = (int)Constant.Function.FunctionPO_Report04CD, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FunctionPO_Report04CD_Search, new FunctionModel { FunctionID = (int)Constant.Function.FunctionPO_Report04CD_Search, FunctionName = "Hiển thị dữ liệu", ParentId = (int)Constant.Function.FunctionPO_Report04CD, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FunctionPO_Report04CD_Search1, new FunctionModel { FunctionID = (int)Constant.Function.FunctionPO_Report04CD_Search1, FunctionName = "Lấy lại dữ liệu đẩy", ParentId = (int)Constant.Function.FunctionPO_Report04CD, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FunctionPO_Report04CD_AddEntry, new FunctionModel { FunctionID = (int)Constant.Function.FunctionPO_Report04CD_AddEntry, FunctionName = "Điểu chỉnh bút toán", ParentId = (int)Constant.Function.FunctionPO_Report04CD, Area = (int)Constant.Function.FunPO }); 

            //Thống kê báo cáo tại Bưu cục
            DicFuns.Add((int)Constant.Function.FuntionPO_Report04CDManage, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_Report04CDManage, FunctionName = "Thống kê báo cáo tại Bưu cục", ParentId = (int)Constant.Function.FunctionPO, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_Report04CDManage_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_Report04CDManage_Search, FunctionName = "Xem", ParentId = (int)Constant.Function.FuntionPO_Report04CDManage, Area = (int)Constant.Function.FunPO }); 

            //Quản trị 04 TQ
            DicFuns.Add((int)Constant.Function.FuntionPO_Report04TQManage, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_Report04TQManage, FunctionName = "Quản trị 04 TQ", ParentId = (int)Constant.Function.FunctionPO, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_Report04TQManage_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_Report04TQManage_Search, FunctionName = "Xem", ParentId = (int)Constant.Function.FuntionPO_Report04TQManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_Report04TQManage_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_Report04TQManage_Add, FunctionName = "Thêm mới", ParentId = (int)Constant.Function.FuntionPO_Report04TQManage, Area = (int)Constant.Function.FunPO }); 

            //Nhận quỹ
            DicFuns.Add((int)Constant.Function.FuntionPO_AllocateFundManage, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_AllocateFundManage, FunctionName = "Tiếp nộp quỹ", ParentId = (int)Constant.Function.FunctionPO, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_AllocateFundManage_FundsReceive_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_AllocateFundManage_FundsReceive_Search, FunctionName = "Xem (Tiếp nộp quỹ)", ParentId = (int)Constant.Function.FuntionPO_AllocateFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_AllocateFundManage_FundsReceive_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_AllocateFundManage_FundsReceive_Add, FunctionName = "Thêm mới (Tiếp nộp quỹ)", ParentId = (int)Constant.Function.FuntionPO_AllocateFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_AllocateFundManage_FundsReceive_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_AllocateFundManage_FundsReceive_Del, FunctionName = "Xóa (Tiếp nộp quỹ)", ParentId = (int)Constant.Function.FuntionPO_AllocateFundManage, Area = (int)Constant.Function.FunPO }); 

            //Tiếp nộp quỹ
            //DicFuns.Add((int)Constant.Function.FuntionPO_AllocateFundManage_FundsSend_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_AllocateFundManage_FundsSend_Search, FunctionName = "Xem (Nhận quỹ)", ParentId = (int)Constant.Function.FuntionPO_AllocateFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_AllocateFundManage_FundsSend_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_AllocateFundManage_FundsSend_Add, FunctionName = "Thêm mới (Nhận quỹ)", ParentId = (int)Constant.Function.FuntionPO_AllocateFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_AllocateFundManage_FundsSend_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_AllocateFundManage_FundsSend_Del, FunctionName = "Xóa (Nhận quỹ)", ParentId = (int)Constant.Function.FuntionPO_AllocateFundManage, Area = (int)Constant.Function.FunPO }); 


            //Vay quỹ khác
            DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage, FunctionName = "Vay trả quỹ khác", ParentId = (int)Constant.Function.FunctionPO, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_BorrowOtherFunds_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_BorrowOtherFunds_Search, FunctionName = "Xem (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_BorrowOtherFunds_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_BorrowOtherFunds_Add, FunctionName = "Thêm mới (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_BorrowOtherFunds_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_BorrowOtherFunds_Del, FunctionName = "Xóa (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 

            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_PayOtherFunds_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_PayOtherFunds_Search, FunctionName = "Xem (Trả quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_PayOtherFunds_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_PayOtherFunds_Add, FunctionName = "Thêm mới (Trả quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_PayOtherFunds_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_PayOtherFunds_Del, FunctionName = "Xóa (Trả quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 

            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsBorrow_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsBorrow_Search, FunctionName = "Xem (Quỹ khác vay)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsBorrow_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsBorrow_Add, FunctionName = "Thêm mới (Quỹ khác vay)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsBorrow_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsBorrow_Del, FunctionName = "Xóa (Quỹ khác vay)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 

            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsPay_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsPay_Search, FunctionName = "Xem (Quỹ khác trả)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsPay_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsPay_Add, FunctionName = "Thêm mới (Quỹ khác trả)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsPay_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_OtherFundsPay_Del, FunctionName = "Xóa (Quỹ khác trả)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage, Area = (int)Constant.Function.FunPO }); 


            //Nộp ngân hàng
            DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, FunctionName = "Rút nộp Ngân hàng", ParentId = (int)Constant.Function.FunctionPO, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_ReceiveTGNH_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_ReceiveTGNH_Search, FunctionName = "Xem (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_ReceiveTGNH_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_ReceiveTGNH_Add, FunctionName = "Thêm mới (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_ReceiveTGNH_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_ReceiveTGNH_Del, FunctionName = "Xóa (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 

            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_SendTGNH_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_SendTGNH_Search, FunctionName = "Xem (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_SendTGNH_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_SendTGNH_Add, FunctionName = "Thêm mới (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_BorrowFundManage_SendTGNH_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_BorrowFundManage_SendTGNH_Del, FunctionName = "Xóa (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 


            //Nhu cầu tiếp quỹ
            DicFuns.Add((int)Constant.Function.FuntionPO_FundRequiredManage, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_FundRequiredManage, FunctionName = "Nhu cầu tiếp quỹ Bưu cục", ParentId = (int)Constant.Function.FunctionPO, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_FundRequiredManage_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_FundRequiredManage_Search, FunctionName = "Xem (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_FundRequiredManage_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_FundRequiredManage_Add, FunctionName = "Thêm mới (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_FundRequiredManage_Edit, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_FundRequiredManage_Edit, FunctionName = "Xóa (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_FundRequiredManage_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_FundRequiredManage_Del, FunctionName = "Thêm mới (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 
            //DicFuns.Add((int)Constant.Function.FuntionPO_FundRequiredManage_Confirm, new FunctionModel { FunctionID = (int)Constant.Function.FuntionPO_FundRequiredManage_Confirm, FunctionName = "Xóa (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionPO_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunPO }); 
            #endregion

            #region Chức năng Bưu Điện Huyện
            DicFuns.Add((int)Constant.Function.FunDistrict, new FunctionModel { FunctionID = (int)Constant.Function.FunDistrict, FunctionName = "Chức năng Bưu điện Huyện", ParentId = 0, Area = (int)Constant.Function.FunDistrict }); 
            //Hệ thống
            DicFuns.Add((int)Constant.Function.System_District, new FunctionModel { FunctionID = (int)Constant.Function.System_District, FunctionName = "Hệ thống", ParentId = (int)Constant.Function.FunDistrict, Area = (int)Constant.Function.FunDistrict }); 

            //Thông số quỹ đầu tháng
            DicFuns.Add((int)Constant.Function.System_District_MonthlyFundInfo, new FunctionModel { FunctionID = (int)Constant.Function.System_District_MonthlyFundInfo, FunctionName = "Thông số quỹ đầu tháng", ParentId = (int)Constant.Function.System_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.System_District_MonthlyFundInfo_Search, new FunctionModel { FunctionID = (int)Constant.Function.System_District_MonthlyFundInfo_Search, FunctionName = "Xem", ParentId = (int)Constant.Function.System_District_MonthlyFundInfo, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.System_District_MonthlyFundInfo_Update, new FunctionModel { FunctionID = (int)Constant.Function.System_District_MonthlyFundInfo_Update, FunctionName = "Thêm, Sửa, Xóa", ParentId = (int)Constant.Function.System_District_MonthlyFundInfo, Area = (int)Constant.Function.FunDistrict }); 

            //Phân quyền Người dùng
            DicFuns.Add((int)Constant.Function.System_District_RoleManage, new FunctionModel { FunctionID = (int)Constant.Function.System_District_RoleManage, FunctionName = "Phân quyền Người dùng", ParentId = (int)Constant.Function.System_District, Area = (int)Constant.Function.FunDistrict });

            //Quản lý đơn vị
            DicFuns.Add((int)Constant.Function.System_District_PoManage, new FunctionModel { FunctionID = (int)Constant.Function.System_District_PoManage, FunctionName = "Quản lý Đơn vị", ParentId = (int)Constant.Function.System_District, Area = (int)Constant.Function.FunDistrict });
            //DicFuns.Add((int)Constant.Function.System_District_PoManage_Add, new FunctionModel { FunctionID = (int)Constant.Function.System_District_PoManage_Add, FunctionName = "Thêm mới", ParentId = (int)Constant.Function.System_District_PoManage, Area = (int)Constant.Function.FunDistrict });
            //DicFuns.Add((int)Constant.Function.System_District_PoManage_Edit, new FunctionModel { FunctionID = (int)Constant.Function.System_District_PoManage_Edit, FunctionName = "Sửa", ParentId = (int)Constant.Function.System_District_PoManage, Area = (int)Constant.Function.FunDistrict });
            //DicFuns.Add((int)Constant.Function.System_District_PoManage_Del, new FunctionModel { FunctionID = (int)Constant.Function.System_District_PoManage_Del, FunctionName = "Xóa", ParentId = (int)Constant.Function.System_District_PoManage, Area = (int)Constant.Function.FunDistrict });
            //DicFuns.Add((int)Constant.Function.System_District_PoManage_Lock, new FunctionModel { FunctionID = (int)Constant.Function.System_District_PoManage_Lock, FunctionName = "Khóa", ParentId = (int)Constant.Function.System_District_PoManage, Area = (int)Constant.Function.FunDistrict });

            //Quản lý Người dùng
            DicFuns.Add((int)Constant.Function.System_District_UserManage, new FunctionModel { FunctionID = (int)Constant.Function.System_District_UserManage, FunctionName = "Quản lý Người dùng", ParentId = (int)Constant.Function.System_District, Area = (int)Constant.Function.FunDistrict });
            //DicFuns.Add((int)Constant.Function.System_District_UserManage_Add, new FunctionModel { FunctionID = (int)Constant.Function.System_District_UserManage_Add, FunctionName = "Thêm mới", ParentId = (int)Constant.Function.System_District_UserManage, Area = (int)Constant.Function.FunDistrict });
            //DicFuns.Add((int)Constant.Function.System_District_UserManage_Edit, new FunctionModel { FunctionID = (int)Constant.Function.System_District_UserManage_Edit, FunctionName = "Sửa", ParentId = (int)Constant.Function.System_District_UserManage, Area = (int)Constant.Function.FunDistrict });
            //DicFuns.Add((int)Constant.Function.System_District_UserManage_Del, new FunctionModel { FunctionID = (int)Constant.Function.System_District_UserManage_Del, FunctionName = "Xóa", ParentId = (int)Constant.Function.System_District_UserManage, Area = (int)Constant.Function.FunDistrict });
            //DicFuns.Add((int)Constant.Function.System_District_UserManage_Lock, new FunctionModel { FunctionID = (int)Constant.Function.System_District_UserManage_Lock, FunctionName = "Khóa", ParentId = (int)Constant.Function.System_District_UserManage, Area = (int)Constant.Function.FunDistrict });


            //Báo cáo Kế toán BĐH
            DicFuns.Add((int)Constant.Function.Report_District, new FunctionModel { FunctionID = (int)Constant.Function.Report_District, FunctionName = "Báo cáo Kế toán BĐH", ParentId = (int)Constant.Function.FunDistrict, Area = (int)Constant.Function.FunDistrict });
            DicFuns.Add((int)Constant.Function.Report_District_Report03CD, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report03CD, FunctionName = "Nhập số liệu Kế toán BĐH", ParentId = (int)Constant.Function.Report_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report03CD_Search, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report03CD_Search, FunctionName = "Tổng hợp nghiệp vụ từ 04 CĐ", ParentId = (int)Constant.Function.Report_District_Report03CD, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report03CD_SearchTH, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report03CD_SearchTH, FunctionName = "Xem 03 CĐ Tổng hợp", ParentId = (int)Constant.Function.Report_District_Report03CD, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report03CD_SearchCT, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report03CD_SearchCT, FunctionName = "Xem 03 CĐ Chi tiết", ParentId = (int)Constant.Function.Report_District_Report03CD, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report03CD_Update, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report03CD_Update, FunctionName = "Cập nhật", ParentId = (int)Constant.Function.Report_District_Report03CD, Area = (int)Constant.Function.FunDistrict }); 


            //Quản trị 03 CĐ
            DicFuns.Add((int)Constant.Function.Report_District_Report03CDManage, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report03CDManage, FunctionName = "Quản trị 03 CĐ", ParentId = (int)Constant.Function.Report_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report03CDManage_Search, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report03CDManage_Search, FunctionName = "Xem", ParentId = (int)Constant.Function.Report_District_Report03CDManage, Area = (int)Constant.Function.FunDistrict }); 


            //Tình hình báo cáo của Bưu cục
            DicFuns.Add((int)Constant.Function.Report_District_ReportPOManage, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_ReportPOManage, FunctionName = "Tình hình báo cáo của Bưu cục", ParentId = (int)Constant.Function.Report_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_ReportPOManage_Search, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_ReportPOManage_Search, FunctionName = "Xem", ParentId = (int)Constant.Function.Report_District_ReportPOManage, Area = (int)Constant.Function.FunDistrict }); 


            //Nhập Thay thế 04CĐ
            DicFuns.Add((int)Constant.Function.Report_District_Report04CDReplace, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report04CDReplace, FunctionName = "Nhập Thay thế 04CĐ", ParentId = (int)Constant.Function.Report_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report04CDReplace_ViewTH, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report04CDReplace_ViewTH, FunctionName = "Xem 04 CĐ Tổng hợp", ParentId = (int)Constant.Function.Report_District_Report04CDReplace, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report04CDReplace_ViewCT, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report04CDReplace_ViewCT, FunctionName = "Xem 04 CĐ Chi tiết", ParentId = (int)Constant.Function.Report_District_Report04CDReplace, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report04CDReplace_Update, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report04CDReplace_Update, FunctionName = "Cập nhật", ParentId = (int)Constant.Function.Report_District_Report04CDReplace, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report04CDReplace_Search1, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report04CDReplace_Search1, FunctionName = "Hiển thị dữ liệu", ParentId = (int)Constant.Function.Report_District_Report04CDReplace, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report04CDReplace_Search2, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report04CDReplace_Search2, FunctionName = "Lấy lại dữ liệu đẩy", ParentId = (int)Constant.Function.Report_District_Report04CDReplace, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report04CDReplace_AddEntry, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report04CDReplace_AddEntry, FunctionName = "Bút toán điều chỉnh", ParentId = (int)Constant.Function.Report_District_Report04CDReplace, Area = (int)Constant.Function.FunDistrict }); 


            //Quản trị 04CĐ
            DicFuns.Add((int)Constant.Function.Report_District_Report04CDReplaceManage, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report04CDReplaceManage, FunctionName = "Quản trị 04CĐ", ParentId = (int)Constant.Function.Report_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Report_District_Report04CDReplaceManage_Search, new FunctionModel { FunctionID = (int)Constant.Function.Report_District_Report04CDReplaceManage_Search, FunctionName = "Xem", ParentId = (int)Constant.Function.Report_District_Report04CDReplaceManage, Area = (int)Constant.Function.FunDistrict }); 

            //Quản trị và điều tiền BĐH
            DicFuns.Add((int)Constant.Function.MoneyManage_District, new FunctionModel { FunctionID = (int)Constant.Function.MoneyManage_District, FunctionName = "Quản trị và điều tiền BĐH", ParentId = (int)Constant.Function.FunDistrict, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.MoneyManage_District_Report03TH, new FunctionModel { FunctionID = (int)Constant.Function.MoneyManage_District_Report03TH, FunctionName = "Lập báo cáo 03TH", ParentId = (int)Constant.Function.MoneyManage_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.MoneyManage_District_Report03TH_Search, new FunctionModel { FunctionID = (int)Constant.Function.MoneyManage_District_Report03TH_Search, FunctionName = "Xem 04 CĐ Chi tiết", ParentId = (int)Constant.Function.MoneyManage_District_Report03TH, Area = (int)Constant.Function.FunDistrict }); 

            //Quản trị 03TH
            DicFuns.Add((int)Constant.Function.MoneyManage_District_Report03THManage, new FunctionModel { FunctionID = (int)Constant.Function.MoneyManage_District_Report03THManage, FunctionName = "Quản trị 03TH", ParentId = (int)Constant.Function.MoneyManage_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.MoneyManage_District_Report03THManage_Search, new FunctionModel { FunctionID = (int)Constant.Function.MoneyManage_District_Report03THManage_Search, FunctionName = "Xem", ParentId = (int)Constant.Function.MoneyManage_District_Report03THManage, Area = (int)Constant.Function.FunDistrict }); 

            //Quản trị 03TQ
            DicFuns.Add((int)Constant.Function.MoneyManage_District_Report03TQManage, new FunctionModel { FunctionID = (int)Constant.Function.MoneyManage_District_Report03TQManage, FunctionName = "Quản trị 03TQ", ParentId = (int)Constant.Function.MoneyManage_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.MoneyManage_District_Report03TQManage_Search, new FunctionModel { FunctionID = (int)Constant.Function.MoneyManage_District_Report03TQManage_Search, FunctionName = "Xem", ParentId = (int)Constant.Function.MoneyManage_District_Report03TQManage, Area = (int)Constant.Function.FunDistrict }); 


            //Báo cáo chất lượng BĐH
            DicFuns.Add((int)Constant.Function.Quality_District, new FunctionModel { FunctionID = (int)Constant.Function.Quality_District, FunctionName = "Báo cáo chất lượng BĐH", ParentId = (int)Constant.Function.FunDistrict, Area = (int)Constant.Function.FunDistrict }); 
            //Đánh giá chất lượng toàn Huyện
            DicFuns.Add((int)Constant.Function.Quality_District_ReviewQualityPO, new FunctionModel { FunctionID = (int)Constant.Function.Quality_District_ReviewQualityPO, FunctionName = "Đánh giá chất lượng toàn Huyện", ParentId = (int)Constant.Function.Quality_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Quality_District_ReviewQualityPO_Search, new FunctionModel { FunctionID = (int)Constant.Function.Quality_District_ReviewQualityPO_Search, FunctionName = "Đánh giá", ParentId = (int)Constant.Function.Quality_District_ReviewQualityPO, Area = (int)Constant.Function.FunDistrict }); 
            //Chất lượng Báo cáo BĐH
            DicFuns.Add((int)Constant.Function.Quality_District_ReviewQualityDistrict, new FunctionModel { FunctionID = (int)Constant.Function.Quality_District_ReviewQualityDistrict, FunctionName = "Chất lượng Báo cáo BĐH", ParentId = (int)Constant.Function.Quality_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.Quality_District_ReviewQualityDistrict_Search, new FunctionModel { FunctionID = (int)Constant.Function.Quality_District_ReviewQualityDistrict_Search, FunctionName = "Đánh giá", ParentId = (int)Constant.Function.Quality_District_ReviewQualityDistrict, Area = (int)Constant.Function.FunDistrict }); 

            //Nghiệp vụ Hỗ trợ BĐH
            DicFuns.Add((int)Constant.Function.BusinessSupport_District, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_District, FunctionName = "Nghiệp vụ Hỗ trợ BĐH", ParentId = (int)Constant.Function.FunDistrict, Area = (int)Constant.Function.FunDistrict }); 
            //Hạn mức lưu quỹ tiền mặt
            DicFuns.Add((int)Constant.Function.BusinessSupport_District_FundLimitsManage, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_District_FundLimitsManage, FunctionName = "Hạn mức lưu quỹ tiền mặt", ParentId = (int)Constant.Function.BusinessSupport_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.BusinessSupport_District_FundLimitsManage_Search, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_District_FundLimitsManage_Search, FunctionName = "Xem hạn mức", ParentId = (int)Constant.Function.BusinessSupport_District_FundLimitsManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.BusinessSupport_District_FundLimitsManage_Update, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_District_FundLimitsManage_Update, FunctionName = "Cập nhật", ParentId = (int)Constant.Function.BusinessSupport_District_FundLimitsManage, Area = (int)Constant.Function.FunDistrict }); 

            //Cảnh báo lưu quỹ vượt hạn mức
            DicFuns.Add((int)Constant.Function.BusinessSupport_District_SaveCashWarning, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_District_SaveCashWarning, FunctionName = "Cảnh báo lưu quỹ vượt hạn mức", ParentId = (int)Constant.Function.BusinessSupport_District, Area = (int)Constant.Function.FunDistrict });
            //DicFuns.Add((int)Constant.Function.BusinessSupport_District_SaveCashWarning_Search, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_District_SaveCashWarning_Search, FunctionName = "Xem", ParentId = (int)Constant.Function.BusinessSupport_District_SaveCashWarning, Area = (int)Constant.Function.FunDistrict }); 
            DicFuns.Add((int)Constant.Function.BusinessSupport_District_FundRequired, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_District_FundRequired, FunctionName = "Nhu cầu tiếp quỹ BĐH", ParentId = (int)Constant.Function.BusinessSupport_District, Area = (int)Constant.Function.FunDistrict });

            //Kế toán quỹ BĐH
            DicFuns.Add((int)Constant.Function.AccountingEntry_District, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District, FunctionName = "Kế toán quỹ BĐH", ParentId = (int)Constant.Function.FunDistrict, Area = (int)Constant.Function.FunDistrict }); 
            //Tiếp nộp quỹ
            DicFuns.Add((int)Constant.Function.AccountingEntry_District_AllocateFundManage, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_AllocateFundManage, FunctionName = "Tiếp nộp quỹ", ParentId = (int)Constant.Function.AccountingEntry_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsReceive_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsReceive_Search, FunctionName = "Xem (Nhận quỹ)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsReceive_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsReceive_Add, FunctionName = "Thêm mới (Nhận quỹ)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsReceive_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsReceive_Del, FunctionName = "Xóa (Nhận quỹ)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsSend_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsSend_Search, FunctionName = "Xem (Tiếp nộp quỹ)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsSend_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsSend_Add, FunctionName = "Thêm mới (Tiếp nộp quỹ)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsSend_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsSend_Del, FunctionName = "Xóa (Tiếp nộp quỹ)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundManage, Area = (int)Constant.Function.FunDistrict }); 

            //Vay trả quỹ khác
            DicFuns.Add((int)Constant.Function.AccountingEntry_District_BorrowFundManage, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, FunctionName = "Vay trả quỹ khác", ParentId = (int)Constant.Function.AccountingEntry_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_BorrowOtherFunds_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_BorrowOtherFunds_Search, FunctionName = "Xem (Vay quỹ khác)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_BorrowOtherFunds_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_BorrowOtherFunds_Add, FunctionName = "Thêm (Vay quỹ khác)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_BorrowOtherFunds_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_BorrowOtherFunds_Del, FunctionName = "Xóa (Vay quỹ khác)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_PayOtherFunds_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_PayOtherFunds_Search, FunctionName = "Xem (Trả quỹ khác)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_PayOtherFunds_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_PayOtherFunds_Add, FunctionName = "Thêm (Trả quỹ khác)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_PayOtherFunds_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_PayOtherFunds_Del, FunctionName = "Xóa (Trả quỹ khác)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsBorrow_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsBorrow_Search, FunctionName = "Xem (Quỹ khác vay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsBorrow_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsBorrow_Add, FunctionName = "Thêm (Quỹ khác vay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsBorrow_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsBorrow_Del, FunctionName = "Xóa (Quỹ khác vay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsPay_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsPay_Search, FunctionName = "Xem (Quỹ khác trả)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsPay_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsPay_Add, FunctionName = "Thêm (Quỹ khác trả)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsPay_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsPay_Del, FunctionName = "Xóa (Quỹ khác trả)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundManage, Area = (int)Constant.Function.FunDistrict }); 

            //Nộp Ngân hàng
            DicFuns.Add((int)Constant.Function.AccountingEntry_District_PayBankManage, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_PayBankManage, FunctionName = "Nộp Ngân hàng", ParentId = (int)Constant.Function.AccountingEntry_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_ReceiveTGNH_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_ReceiveTGNH_Search, FunctionName = "Xem (Nhận TGNH)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_ReceiveTGNH_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_ReceiveTGNH_Add, FunctionName = "Thêm mới (Nhận TGNH)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_ReceiveTGNH_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_ReceiveTGNH_Del, FunctionName = "Xóa (Nhận TGNH)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_SendTGNH_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_SendTGNH_Search, FunctionName = "Xem (Nộp TGNH)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_SendTGNH_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_SendTGNH_Add, FunctionName = "Thêm mới (Nộp TGNH)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_SendTGNH_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_SendTGNH_Del, FunctionName = "Xóa (Nộp TGNH)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankManage, Area = (int)Constant.Function.FunDistrict }); 

            //Nhập thay thế tiếp nộp quỹ
            DicFuns.Add((int)Constant.Function.AccountingEntry_District_AllocateFundReplaceManage, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_AllocateFundReplaceManage, FunctionName = "Nhập thay thế tiếp nộp quỹ", ParentId = (int)Constant.Function.AccountingEntry_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsReceiveReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsReceiveReplace_Search, FunctionName = "Xem (Nhận quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsReceiveReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsReceiveReplace_Add, FunctionName = "Thêm mới (Nhận quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsReceiveReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsReceiveReplace_Del, FunctionName = "Xóa (Nhận quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsSendReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsSendReplace_Search, FunctionName = "Xem (Tiếp nộp quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsSendReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsSendReplace_Add, FunctionName = "Thêm mới (Tiếp nộp quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_FundsSendReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_FundsSendReplace_Del, FunctionName = "Xóa (Tiếp nộp quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_AllocateFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 

            //Nhập thay thế vay trả quỹ khác
            DicFuns.Add((int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, FunctionName = "Nhập thay thế vay trả quỹ khác", ParentId = (int)Constant.Function.AccountingEntry_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_BorrowOtherFundsReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_BorrowOtherFundsReplace_Search, FunctionName = "Xem (Vay quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_BorrowOtherFundsReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_BorrowOtherFundsReplace_Add, FunctionName = "Thêm (Vay quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_BorrowOtherFundsReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_BorrowOtherFundsReplace_Del, FunctionName = "Xóa (Vay quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_PayOtherFundsReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_PayOtherFundsReplace_Search, FunctionName = "Xem (Trả quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_PayOtherFundsReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_PayOtherFundsReplace_Add, FunctionName = "Thêm (Trả quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_PayOtherFundsReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_PayOtherFundsReplace_Del, FunctionName = "Xóa (Trả quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsBorrowReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsBorrowReplace_Search, FunctionName = "Xem (Quỹ khác vay - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsBorrowReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsBorrowReplace_Add, FunctionName = "Thêm (Quỹ khác vay - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsBorrowReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsBorrowReplace_Del, FunctionName = "Xóa (Quỹ khác vay - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsPayReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsPayReplace_Search, FunctionName = "Xem (Quỹ khác trả - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsPayReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsPayReplace_Add, FunctionName = "Thêm (Quỹ khác trả - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_OtherFundsPayReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_OtherFundsPayReplace_Del, FunctionName = "Xóa (Quỹ khác trả - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_BorrowFundReplaceManage, Area = (int)Constant.Function.FunDistrict }); 

            //Nộp ngân hàng thay thế
            DicFuns.Add((int)Constant.Function.AccountingEntry_District_PayBankReplaceManage, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_PayBankReplaceManage, FunctionName = "Nộp ngân hàng thay thế", ParentId = (int)Constant.Function.AccountingEntry_District, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_ReceiveTGNHReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_ReceiveTGNHReplace_Search, FunctionName = "Xem (Nhận TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_ReceiveTGNHReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_ReceiveTGNHReplace_Add, FunctionName = "Thêm mới (Nhận TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_ReceiveTGNHReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_ReceiveTGNHReplace_Del, FunctionName = "Xóa (Nhận TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_SendTGNHReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_SendTGNHReplace_Search, FunctionName = "Xem (Nộp TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_SendTGNHReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_SendTGNHReplace_Add, FunctionName = "Thêm mới (Nộp TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            //DicFuns.Add((int)Constant.Function.AccountingEntry_District_SendTGNHReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_District_SendTGNHReplace_Del, FunctionName = "Xóa (Nộp TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_District_PayBankReplaceManage, Area = (int)Constant.Function.FunDistrict }); 
            #endregion

            #region Bưu điện Tỉnh
            // 2 - Province
            DicFuns.Add((int)Constant.Function.FunBranch, new FunctionModel { FunctionID = (int)Constant.Function.FunBranch, FunctionName = "Chức năng Bưu điện Tỉnh", ParentId = 0, Area = (int)Constant.Function.FunBranch });

            //Hệ thống
            DicFuns.Add((int)Constant.Function.System_Branch, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch, FunctionName = "Hệ thống", ParentId = (int)Constant.Function.FunBranch, Area = (int)Constant.Function.FunBranch });
            //Quản lý đơn vị
            DicFuns.Add((int)Constant.Function.System_Branch_PoManage, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_PoManage, FunctionName = "Quản lý đơn vị", ParentId = (int)Constant.Function.System_Branch, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.System_Branch_PoManage_Add, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_PoManage_Add, FunctionName = "Thêm mới", ParentId = (int)Constant.Function.System_Branch_PoManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.System_Branch_PoManage_Edit, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_PoManage_Edit, FunctionName = "Sửa", ParentId = (int)Constant.Function.System_Branch_PoManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.System_Branch_PoManage_Del, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_PoManage_Del, FunctionName = "Xóa", ParentId = (int)Constant.Function.System_Branch_PoManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.System_Branch_PoManage_Lock, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_PoManage_Lock, FunctionName = "Khóa", ParentId = (int)Constant.Function.System_Branch_PoManage, Area = (int)Constant.Function.FunBranch });

            //Quản lý người dùng
            DicFuns.Add((int)Constant.Function.System_Branch_UserManage, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_UserManage, FunctionName = "Quản lý người dùng", ParentId = (int)Constant.Function.System_Branch, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.System_Branch_UserManage_Add, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_UserManage_Add, FunctionName = "Thêm mới", ParentId = (int)Constant.Function.System_Branch_UserManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.System_Branch_UserManage_Edit, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_UserManage_Edit, FunctionName = "Sửa", ParentId = (int)Constant.Function.System_Branch_UserManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.System_Branch_UserManage_Del, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_UserManage_Del, FunctionName = "Xóa", ParentId = (int)Constant.Function.System_Branch_UserManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.System_Branch_UserManage_Lock, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_UserManage_Lock, FunctionName = "Khóa", ParentId = (int)Constant.Function.System_Branch_UserManage, Area = (int)Constant.Function.FunBranch });

            //Phân quyền Người dùng
            DicFuns.Add((int)Constant.Function.System_Branch_RoleManage, new FunctionModel { FunctionID = (int)Constant.Function.System_Branch_RoleManage, FunctionName = "Phân quyền người dùng", ParentId = (int)Constant.Function.System_Branch, Area = (int)Constant.Function.FunBranch });
            
            //Lập báo cáo Ngân vụ
            DicFuns.Add((int)Constant.Function.AccountingBranch, new FunctionModel { FunctionID = (int)Constant.Function.AccountingBranch, FunctionName = "Lập báo cáo Ngân vụ", ParentId = (int)Constant.Function.FunBranch, Area = (int)Constant.Function.FunBranch });
            //Thống kê báo cáo toàn Tỉnh
            DicFuns.Add((int)Constant.Function.BusinessSupport_Branch_ReportPOManage, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_Branch_ReportPOManage, FunctionName = "Thống kê báo cáo toàn Tỉnh", ParentId = (int)Constant.Function.AccountingBranch, Area = (int)Constant.Function.FunBranch });
            
            //Tổng hợp, Thống kê
            DicFuns.Add((int)Constant.Function.StatisticGeneral, new FunctionModel { FunctionID = (int)Constant.Function.StatisticGeneral, FunctionName = "Tổng hợp, Thống kê", ParentId = (int)Constant.Function.FunBranch, Area = (int)Constant.Function.FunBranch });
            //Hỗ trợ nghiệp vụ Bưu điện Tỉnh
            DicFuns.Add((int)Constant.Function.BusinessSupport_Branch, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_Branch, FunctionName = "Hỗ trợ nghiệp vụ Bưu điện Tỉnh", ParentId = (int)Constant.Function.FunBranch, Area = (int)Constant.Function.FunBranch });
            //Hạn mức lưu quỹ tiền mặt
            DicFuns.Add((int)Constant.Function.BusinessSupport_Branch_FundLimitsManage, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_Branch_FundLimitsManage, FunctionName = "Hạn mức lưu quỹ tiền mặt", ParentId = (int)Constant.Function.BusinessSupport_Branch, Area = (int)Constant.Function.FunBranch });
            //Cảnh báo lưu quỹ vượt hạn mức
            DicFuns.Add((int)Constant.Function.BusinessSupport_Branch_SaveCashWarning, new FunctionModel { FunctionID = (int)Constant.Function.BusinessSupport_Branch_SaveCashWarning, FunctionName = "Cảnh báo lưu quỹ vượt hạn mức", ParentId = (int)Constant.Function.BusinessSupport_Branch, Area = (int)Constant.Function.FunBranch });
            

            //Kế toán Tiếp quỹ BĐT
            DicFuns.Add((int)Constant.Function.AccountingEntry_Branch, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch, FunctionName = "Kế toán Tiếp quỹ BĐT", ParentId = (int)Constant.Function.FunBranch, Area = (int)Constant.Function.FunBranch });
            //Tiếp nộp quỹ 
            DicFuns.Add((int)Constant.Function.FuntionBranch_AllocateFundManage, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_AllocateFundManage, FunctionName = "Tiếp nộp quỹ", ParentId = (int)Constant.Function.AccountingEntry_Branch, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_AllocateFundManage_FundsReceive_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_AllocateFundManage_FundsReceive_Search, FunctionName = "Xem (Nhận quỹ)", ParentId = (int)Constant.Function.FuntionBranch_AllocateFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_AllocateFundManage_FundsReceive_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_AllocateFundManage_FundsReceive_Add, FunctionName = "Thêm mới (Nhận quỹ)", ParentId = (int)Constant.Function.FuntionBranch_AllocateFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_AllocateFundManage_FundsReceive_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_AllocateFundManage_FundsReceive_Del, FunctionName = "Xóa (Nhận quỹ)", ParentId = (int)Constant.Function.FuntionBranch_AllocateFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_AllocateFundManage_FundsSend_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_AllocateFundManage_FundsSend_Search, FunctionName = "Xem (Tiếp nộp quỹ)", ParentId = (int)Constant.Function.FuntionBranch_AllocateFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_AllocateFundManage_FundsSend_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_AllocateFundManage_FundsSend_Add, FunctionName = "Thêm mới (Tiếp nộp quỹ)", ParentId = (int)Constant.Function.FuntionBranch_AllocateFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_AllocateFundManage_FundsSend_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_AllocateFundManage_FundsSend_Del, FunctionName = "Xóa (Tiếp nộp quỹ)", ParentId = (int)Constant.Function.FuntionBranch_AllocateFundManage, Area = (int)Constant.Function.FunBranch }); 

            //Vay trả quỹ khác
            DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage, FunctionName = "Vay trả quỹ khác", ParentId = (int)Constant.Function.AccountingEntry_Branch, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_BorrowOtherFunds_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_BorrowOtherFunds_Search, FunctionName = "Xem (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_BorrowOtherFunds_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_BorrowOtherFunds_Add, FunctionName = "Thêm (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_BorrowOtherFunds_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_BorrowOtherFunds_Del, FunctionName = "Xóa (Vay quỹ khác)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_PayOtherFunds_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_PayOtherFunds_Search, FunctionName = "Xem (Trả quỹ khác)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_PayOtherFunds_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_PayOtherFunds_Add, FunctionName = "Thêm (Trả quỹ khác)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_PayOtherFunds_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_PayOtherFunds_Del, FunctionName = "Xóa (Trả quỹ khác)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsBorrow_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsBorrow_Search, FunctionName = "Xem (Quỹ khác vay)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsBorrow_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsBorrow_Add, FunctionName = "Thêm (Quỹ khác vay)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsBorrow_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsBorrow_Del, FunctionName = "Xóa (Quỹ khác vay)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsPay_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsPay_Search, FunctionName = "Xem (Quỹ khác trả)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsPay_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsPay_Add, FunctionName = "Thêm (Quỹ khác trả)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsPay_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_OtherFundsPay_Del, FunctionName = "Xóa (Quỹ khác trả)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage, Area = (int)Constant.Function.FunBranch }); 

            //Nộp Ngân hàng
            DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_TGNH, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_TGNH, FunctionName = "Nộp Ngân hàng", ParentId = (int)Constant.Function.AccountingEntry_Branch, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_ReceiveTGNH_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_ReceiveTGNH_Search, FunctionName = "Xem (Nhận TGNH)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_ReceiveTGNH_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_ReceiveTGNH_Add, FunctionName = "Thêm mới (Nhận TGNH)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_ReceiveTGNH_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_ReceiveTGNH_Del, FunctionName = "Xóa (Nhận TGNH)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_SendTGNH_Search, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_SendTGNH_Search, FunctionName = "Xem (Nộp TGNH)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_SendTGNH_Add, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_SendTGNH_Add, FunctionName = "Thêm mới (Nộp TGNH)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.FuntionBranch_BorrowFundManage_SendTGNH_Del, new FunctionModel { FunctionID = (int)Constant.Function.FuntionBranch_BorrowFundManage_SendTGNH_Del, FunctionName = "Xóa (Nộp TGNH)", ParentId = (int)Constant.Function.FuntionBranch_BorrowFundManage_TGNH, Area = (int)Constant.Function.FunBranch }); 

            //Nhập thay thế tiếp nộp quỹ
            DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_AllocateFundReplaceManage, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_AllocateFundReplaceManage, FunctionName = "Nhập thay thế tiếp nộp quỹ", ParentId = (int)Constant.Function.AccountingEntry_Branch, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_FundsReceiveReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_FundsReceiveReplace_Search, FunctionName = "Xem (Nhận quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_AllocateFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_FundsReceiveReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_FundsReceiveReplace_Add, FunctionName = "Thêm mới (Nhận quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_AllocateFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_FundsReceiveReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_FundsReceiveReplace_Del, FunctionName = "Xóa (Nhận quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_AllocateFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_FundsSendReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_FundsSendReplace_Search, FunctionName = "Xem (Tiếp nộp quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_AllocateFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_FundsSendReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_FundsSendReplace_Add, FunctionName = "Thêm mới (Tiếp nộp quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_AllocateFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_FundsSendReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_FundsSendReplace_Del, FunctionName = "Xóa (Tiếp nộp quỹ - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_AllocateFundReplaceManage, Area = (int)Constant.Function.FunBranch }); 

            //Nhập thay thế vay trả quỹ khác
            DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, FunctionName = "Nhập thay thế vay trả quỹ khác", ParentId = (int)Constant.Function.AccountingEntry_Branch, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_BorrowOtherFundsReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_BorrowOtherFundsReplace_Search, FunctionName = "Xem (Vay quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_BorrowOtherFundsReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_BorrowOtherFundsReplace_Add, FunctionName = "Thêm (Vay quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_BorrowOtherFundsReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_BorrowOtherFundsReplace_Del, FunctionName = "Xóa (Vay quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_PayOtherFundsReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_PayOtherFundsReplace_Search, FunctionName = "Xem (Trả quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_PayOtherFundsReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_PayOtherFundsReplace_Add, FunctionName = "Thêm (Trả quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_PayOtherFundsReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_PayOtherFundsReplace_Del, FunctionName = "Xóa (Trả quỹ khác - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_OtherFundsBorrowReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_OtherFundsBorrowReplace_Search, FunctionName = "Xem (Quỹ khác vay - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_OtherFundsBorrowReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_OtherFundsBorrowReplace_Add, FunctionName = "Thêm (Quỹ khác vay - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_OtherFundsBorrowReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_OtherFundsBorrowReplace_Del, FunctionName = "Xóa (Quỹ khác vay - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_OtherFundsPayReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_OtherFundsPayReplace_Search, FunctionName = "Xem (Quỹ khác trả - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_OtherFundsPayReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_OtherFundsPayReplace_Add, FunctionName = "Thêm (Quỹ khác trả - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_OtherFundsPayReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_OtherFundsPayReplace_Del, FunctionName = "Xóa (Quỹ khác trả - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_BorrowFundReplaceManage, Area = (int)Constant.Function.FunBranch }); 

            //Nộp ngân hàng thay thế
            DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_PayBankReplaceManage, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_PayBankReplaceManage, FunctionName = "Nộp ngân hàng thay thế", ParentId = (int)Constant.Function.AccountingEntry_Branch, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_ReceiveTGNHReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_ReceiveTGNHReplace_Search, FunctionName = "Xem (Nhận TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_PayBankReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_ReceiveTGNHReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_ReceiveTGNHReplace_Add, FunctionName = "Thêm mới (Nhận TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_PayBankReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_ReceiveTGNHReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_ReceiveTGNHReplace_Del, FunctionName = "Xóa (Nhận TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_PayBankReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_SendTGNHReplace_Search, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_SendTGNHReplace_Search, FunctionName = "Xem (Nộp TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_PayBankReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_SendTGNHReplace_Add, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_SendTGNHReplace_Add, FunctionName = "Thêm mới (Nộp TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_PayBankReplaceManage, Area = (int)Constant.Function.FunBranch });
            //DicFuns.Add((int)Constant.Function.AccountingEntry_Branch_SendTGNHReplace_Del, new FunctionModel { FunctionID = (int)Constant.Function.AccountingEntry_Branch_SendTGNHReplace_Del, FunctionName = "Xóa (Nộp TGNH - Nhập thay)", ParentId = (int)Constant.Function.AccountingEntry_Branch_PayBankReplaceManage, Area = (int)Constant.Function.FunBranch }); 
            #endregion

            #region Tổng công ty
            // 1 - Center
            DicFuns.Add((int)Constant.Function.FunCenter, new FunctionModel { FunctionID = (int)Constant.Function.FunCenter, FunctionName = "Chức năng Tổng Công ty", ParentId = 0, Area = (int)Constant.Function.FunCenter });

            //Hệ thống
            DicFuns.Add((int)Constant.Function.System_Center, new FunctionModel { FunctionID = (int)Constant.Function.System_Center, FunctionName = "Hệ thống", ParentId = (int)Constant.Function.FunCenter, Area = (int)Constant.Function.FunCenter });
            //Quản lý đơn vị
            DicFuns.Add((int)Constant.Function.System_Center_PoManage, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_PoManage, FunctionName = "Quản lý đơn vị", ParentId = (int)Constant.Function.System_Center, Area = (int)Constant.Function.FunCenter });
            //DicFuns.Add((int)Constant.Function.System_Center_PoManage_Add, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_PoManage_Add, FunctionName = "Thêm mới", ParentId = (int)Constant.Function.System_Center_PoManage, Area = (int)Constant.Function.FunCenter });
            //DicFuns.Add((int)Constant.Function.System_Center_PoManage_Edit, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_PoManage_Edit, FunctionName = "Sửa", ParentId = (int)Constant.Function.System_Center_PoManage, Area = (int)Constant.Function.FunCenter });
            //DicFuns.Add((int)Constant.Function.System_Center_PoManage_Del, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_PoManage_Del, FunctionName = "Xóa", ParentId = (int)Constant.Function.System_Center_PoManage, Area = (int)Constant.Function.FunCenter });
            //DicFuns.Add((int)Constant.Function.System_Center_PoManage_Lock, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_PoManage_Lock, FunctionName = "Khóa", ParentId = (int)Constant.Function.System_Center_PoManage, Area = (int)Constant.Function.FunCenter });

            //Quản lý người dùng
            DicFuns.Add((int)Constant.Function.System_Center_UserManage, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_UserManage, FunctionName = "Quản lý người dùng", ParentId = (int)Constant.Function.System_Center, Area = (int)Constant.Function.FunCenter });
            //DicFuns.Add((int)Constant.Function.System_Center_UserManage_Add, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_UserManage_Add, FunctionName = "Thêm mới", ParentId = (int)Constant.Function.System_Center_UserManage, Area = (int)Constant.Function.FunCenter });
            //DicFuns.Add((int)Constant.Function.System_Center_UserManage_Edit, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_UserManage_Edit, FunctionName = "Sửa", ParentId = (int)Constant.Function.System_Center_UserManage, Area = (int)Constant.Function.FunCenter });
            //DicFuns.Add((int)Constant.Function.System_Center_UserManage_Del, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_UserManage_Del, FunctionName = "Xóa", ParentId = (int)Constant.Function.System_Center_UserManage, Area = (int)Constant.Function.FunCenter });
            //DicFuns.Add((int)Constant.Function.System_Center_UserManage_Lock, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_UserManage_Lock, FunctionName = "Khóa", ParentId = (int)Constant.Function.System_Center_UserManage, Area = (int)Constant.Function.FunCenter });

            //Phân quyền Người dùng
            DicFuns.Add((int)Constant.Function.System_Center_RoleManage, new FunctionModel { FunctionID = (int)Constant.Function.System_Center_RoleManage, FunctionName = "Phân quyền người dùng", ParentId = (int)Constant.Function.System_Center, Area = (int)Constant.Function.FunCenter });

            #endregion
            List<FunctionList> ListFunc = new List<FunctionList>();

            foreach(KeyValuePair<int,FunctionModel> func in DicFuns)
            {
                ListFunc.Add(new FunctionList
                {
                    Id = func.Value.FunctionID,
                    Name = func.Value.FunctionName,
                    ParentId = func.Value.ParentId,
                    AreaId = func.Value.Area
                });

            }

            var rs = Helper.Invoke(Constant.Method.POST.ToString(), "api/Employee/CreateFunctionList", ListFunc);
        }
    }
}