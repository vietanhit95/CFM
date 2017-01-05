using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Common
{
    public static class Constant
    {
        public const string EMP_SESSION = "Employee";
        public const string PO_SESSION = "PostOffice";
        public const string TIMEWORK_SESSION = "TimeWork";
        public static int PageSize = 50;
        public static int PageSizeForModal = 12;
        public static string GroupSeparator = ".";
        public static string DecimalSeparator = ",";
        public static string StandardDecimalSeparator = ".";
        public enum Method
        {
            GET, POST, PUT, HEAD, DELETE
        }

        public enum NotifiDashBoard
        {
            NotifiDashBoard = 223,
            ReportDashBoard = 224,
            FunInfomation = 225,
            RefResh = 226
        }
        public enum RefType
        {
            ReportList =1,
            ItemType =2,
            ItemGroup = 3,
            ItemList = 4,
            Mapping =5,
            TransferEntry =6
        }

        public enum CurrencyType
        {
            VND = 1,
            USD = 2
        }

        public enum BorrowMethod
        {
            TM = 1,
            CK = 2
        }

        public enum AccountingRefType
        {
            FundsSend = 888, //Tiếp nộp quỹ
            FundsReceive = 886, //Nhận quỹ
            BorrowOtherFunds = 866, //Vay quỹ khác
            PayOtherFunds = 666, //Trả quỹ khác
            OtherFundsBorrow = 668, //Quỹ khác vay
            OtherFundsPay = 688, //Quỹ Khác trả
            ReceiveTGNH = 686, //Nhận TGNH
            SendTGNH = 868 //Nộp TGNH
        }

        public enum FunctionType 
        { 
            ConfigCenter = 1,
            UnitCenter = 2,
            ReportCenter = 3,
            FundBranch = 4
        }

        public enum CashFllowType
        {
            General = 1,//Dòng tổng hợp (chỉ dùng cho 04-CĐ)
            Center =2,
            Unit=3,
            Saving=4,
            BusinessService=5,
            
        }

        public enum POLevel
        {
            Center = 1,
            Branch = 2,
            District = 3,
            Counter = 4
        }

        public enum Role
        {
            //Nghiệp vụ Đơn vị đầu mối
            NVCenter = 1,
            //Kế toán Đơn vị đầu mối
            KTCenter = 2,
            //Phụ trách Đơn vị đầu mối
            PTCenter = 3,
            //Nghiệp vụ Bưu điện Tỉnh
            NVProvince = 4,
            //Kế toán Bưu điện Tỉnh
            KTProvince = 5,
            //Phụ trách Bưu điện Tỉnh
            PTProvince = 6,
            //Nghiệp vụ Bưu điện Huyện
            NVDistrict = 7,
            //Kế toán Bưu điện Huyện
            KTDistrict = 8,
            //Phụ trách Bưu điện Huyện
            PTDistrict = 9,
            //Nghiệp vụ Bưu cục
            NVPO = 10,
            //Kế toán Bưu cục
            KTPO = 11,
            //Trưởng Bưu cục
            PTPO = 12
        }

        public enum Areas
        {
            Admin = 1,
            CFMBranch = 2,
            CFMCounter = 3,
            CFMDistrict = 4,
            CFMReport = 5
        }

        public enum Function
        {
            #region Bưu cục
            //4 - Counter
            FunPO = 4,

            //Hệ thống
            System = 41,
            //Thông số Quỹ Đầu Tháng
            System_MonthlyFundInfo = 401,
            System_MonthlyFundInfo_Update = 40100,

            //Tham Số dịch vụ
            System_ReportConfigItemPO = 402,
            System_ReportConfigItemPO_Update = 40200,

            //Quản lý Đơn vị
            System_PoManage = 403,
            System_PoManage_Add = 40301,
            System_PoManage_Del = 40302,
            System_PoManage_Edit = 40303,
            System_PoManage_Lock = 40304,

            //Quản lý Người dùng
            System_UserManage = 404,
            System_UserManage_Add = 40401,
            System_UserManage_Del = 40402,
            System_UserManage_Edit = 40403,
            System_UserManage_Lock = 40404,

            //Chức năng Phân Quyền
            System_RoleManage = 412,

            //Chức năng bưu cục
            FunctionPO = 42,
            //Kiểm tra và xác nhận 04CĐ
            FunctionPO_Report04CD = 405,
            FunctionPO_Report04CD_ViewTH = 40501,
            FunctionPO_Report04CD_ViewCT = 40502,
            FunctionPO_Report04CD_Confirm = 40503,
            FunctionPO_Report04CD_Update = 40504,
            FunctionPO_Report04CD_Search = 40505,
            FunctionPO_Report04CD_Search1 = 40506,
            FunctionPO_Report04CD_AddEntry = 40507,

            //Thống kê báo cáo tại Bưu cục
            FuntionPO_Report04CDManage = 406,
            FuntionPO_Report04CDManage_Search = 40601,


            //Quản trị 04 TQ
            FuntionPO_Report04TQManage = 407,
            FuntionPO_Report04TQManage_Add = 40701,
            FuntionPO_Report04TQManage_Search = 40702,


            //Tiếp nộp quỹ 
            FuntionPO_AllocateFundManage = 408,
            FuntionPO_AllocateFundManage_FundsReceive_Search = 40801,
            FuntionPO_AllocateFundManage_FundsReceive_Add = 40802,
            FuntionPO_AllocateFundManage_FundsReceive_Del = 40803,

            FuntionPO_AllocateFundManage_FundsSend_Search = 40804,
            FuntionPO_AllocateFundManage_FundsSend_Add = 40805,
            FuntionPO_AllocateFundManage_FundsSend_Del = 40806,


            //Vay quỹ khác
            FuntionPO_BorrowFundManage = 409,
            FuntionPO_BorrowFundManage_BorrowOtherFunds_Search = 40901,
            FuntionPO_BorrowFundManage_BorrowOtherFunds_Add = 40902,
            FuntionPO_BorrowFundManage_BorrowOtherFunds_Del = 40903,

            FuntionPO_BorrowFundManage_PayOtherFunds_Search = 40904,
            FuntionPO_BorrowFundManage_PayOtherFunds_Add = 40905,
            FuntionPO_BorrowFundManage_PayOtherFunds_Del = 40906,

            FuntionPO_BorrowFundManage_OtherFundsBorrow_Search = 40907,
            FuntionPO_BorrowFundManage_OtherFundsBorrow_Add = 40908,
            FuntionPO_BorrowFundManage_OtherFundsBorrow_Del = 40909,

            FuntionPO_BorrowFundManage_OtherFundsPay_Search = 40910,
            FuntionPO_BorrowFundManage_OtherFundsPay_Add = 40911,
            FuntionPO_BorrowFundManage_OtherFundsPay_Del = 40912,

            //Nộp ngân hàng
            FuntionPO_BorrowFundManage_TGNH = 410,
            FuntionPO_BorrowFundManage_ReceiveTGNH_Search = 41001,
            FuntionPO_BorrowFundManage_ReceiveTGNH_Add = 41002,
            FuntionPO_BorrowFundManage_ReceiveTGNH_Del = 41003,

            FuntionPO_BorrowFundManage_SendTGNH_Search = 41004,
            FuntionPO_BorrowFundManage_SendTGNH_Add = 41005,
            FuntionPO_BorrowFundManage_SendTGNH_Del = 41006,


            //Nhu cầu tiếp quỹ
            FuntionPO_FundRequiredManage = 411,
            FuntionPO_FundRequiredManage_Add = 41101,
            FuntionPO_FundRequiredManage_Edit = 41102,
            FuntionPO_FundRequiredManage_Del = 41103,
            FuntionPO_FundRequiredManage_Search = 41104,
            FuntionPO_FundRequiredManage_Confirm = 41105,
            #endregion

            #region Bưu điện Huyện
            //3 - District
            FunDistrict = 3,

            System_District = 31,
            //Thông số quỹ đầu tháng
            System_District_MonthlyFundInfo = 301,
            System_District_MonthlyFundInfo_Search = 30101,
            System_District_MonthlyFundInfo_Update = 30102,

            //Phân quyền Người Dùng
            System_District_RoleManage = 320,

            //Quản lý đơn vị
            System_District_PoManage = 321,
            System_District_PoManage_Add = 32101,
            System_District_PoManage_Del = 32102,
            System_District_PoManage_Edit = 32103,
            System_District_PoManage_Lock = 32104,

            //Quản lý Người dùng
            System_District_UserManage = 322,
            System_District_UserManage_Add = 32201,
            System_District_UserManage_Del = 32202,
            System_District_UserManage_Edit = 32203,
            System_District_UserManage_Lock = 32204,

            //Báo cáo Kế toán BĐH
            Report_District = 32,
            //Nhập số liệu Kế toán BĐH
            Report_District_Report03CD = 302,
            Report_District_Report03CD_SearchTH = 30201,
            Report_District_Report03CD_SearchCT = 30202,
            Report_District_Report03CD_Update = 30203,
            Report_District_Report03CD_Search = 30204,

            //Quản trị 03 CĐ
            Report_District_Report03CDManage = 303,
            Report_District_Report03CDManage_Search = 30301,


            //Tình hình báo cáo của Bưu cục
            Report_District_ReportPOManage = 304,
            Report_District_ReportPOManage_Search = 30401,

            //Nhập Thay thế 04CĐ
            Report_District_Report04CDReplace = 305,
            Report_District_Report04CDReplace_ViewTH = 30501,
            Report_District_Report04CDReplace_ViewCT = 30502,
            Report_District_Report04CDReplace_Update = 30504,
            Report_District_Report04CDReplace_Search1 = 30505,
            Report_District_Report04CDReplace_Search2 = 30506,
            Report_District_Report04CDReplace_AddEntry = 30507,


            //Quản trị 04CĐ
            Report_District_Report04CDReplaceManage = 306,
            Report_District_Report04CDReplaceManage_Search = 30601,


            //Quản trị và điều tiền BĐH
            MoneyManage_District = 33,
            //Lập báo cáo 03TH
            MoneyManage_District_Report03TH = 307,
            MoneyManage_District_Report03TH_Search = 30701,

            //Quản trị 03TH
            MoneyManage_District_Report03THManage = 318,
            MoneyManage_District_Report03THManage_Search = 31801,

            //Quản trị 03TQ
            MoneyManage_District_Report03TQManage = 319,
            MoneyManage_District_Report03TQManage_Search = 31901,


            //Báo cáo chất lượng BĐH
            Quality_District = 34,

            //Đánh giá chất lượng toàn Huyện
            Quality_District_ReviewQualityPO = 308,
            Quality_District_ReviewQualityPO_Search = 30801,

            //Chất lượng Báo cáo BĐH
            Quality_District_ReviewQualityDistrict = 309,
            Quality_District_ReviewQualityDistrict_Search = 30901,

            //Nghiệp vụ Hỗ trợ BĐH
            BusinessSupport_District = 35,
            //Hạn mức lưu quỹ tiền mặt
            BusinessSupport_District_FundLimitsManage = 310,
            BusinessSupport_District_FundLimitsManage_Search = 31001,
            BusinessSupport_District_FundLimitsManage_Update = 31002,
            //Cảnh báo lưu quỹ vượt hạn mức
            BusinessSupport_District_SaveCashWarning = 311,
            BusinessSupport_District_SaveCashWarning_Search = 31101,
            //Nhu cầu tiếp quỹ BĐH
            BusinessSupport_District_FundRequired = 323,
            //Kế toán quỹ BĐH
            AccountingEntry_District = 36,
            //Tiếp nộp quỹ
            AccountingEntry_District_AllocateFundManage = 312,
            AccountingEntry_District_FundsReceive_Search = 31201,
            AccountingEntry_District_FundsReceive_Add = 31202,
            AccountingEntry_District_FundsReceive_Del = 31203,

            AccountingEntry_District_FundsSend_Search = 31204,
            AccountingEntry_District_FundsSend_Add = 31205,
            AccountingEntry_District_FundsSend_Del = 31206,

            //Vay trả quỹ khác
            AccountingEntry_District_BorrowFundManage = 313,
            AccountingEntry_District_BorrowOtherFunds_Search = 31301,
            AccountingEntry_District_BorrowOtherFunds_Add = 31302,
            AccountingEntry_District_BorrowOtherFunds_Del = 31303,


            AccountingEntry_District_PayOtherFunds_Search = 31304,
            AccountingEntry_District_PayOtherFunds_Add = 31305,
            AccountingEntry_District_PayOtherFunds_Del = 31306,


            AccountingEntry_District_OtherFundsBorrow_Search = 31307,
            AccountingEntry_District_OtherFundsBorrow_Add = 31308,
            AccountingEntry_District_OtherFundsBorrow_Del = 31309,


            AccountingEntry_District_OtherFundsPay_Search = 31310,
            AccountingEntry_District_OtherFundsPay_Add = 31311,
            AccountingEntry_District_OtherFundsPay_Del = 31312,

            //Nộp Ngân hàng
            AccountingEntry_District_PayBankManage = 314,
            AccountingEntry_District_ReceiveTGNH_Search = 31401,
            AccountingEntry_District_ReceiveTGNH_Add = 31402,
            AccountingEntry_District_ReceiveTGNH_Del = 31403,

            AccountingEntry_District_SendTGNH_Search = 31404,
            AccountingEntry_District_SendTGNH_Add = 31405,
            AccountingEntry_District_SendTGNH_Del = 31406,

            //Nhập thay thế tiếp nộp quỹ
            AccountingEntry_District_AllocateFundReplaceManage = 315,
            AccountingEntry_District_FundsReceiveReplace_Search = 31501,
            AccountingEntry_District_FundsReceiveReplace_Add = 31502,
            AccountingEntry_District_FundsReceiveReplace_Del = 31503,


            AccountingEntry_District_FundsSendReplace_Search = 31504,
            AccountingEntry_District_FundsSendReplace_Add = 31505,
            AccountingEntry_District_FundsSendReplace_Del = 31506,


            //Nhập thay thế vay trả quỹ khác
            AccountingEntry_District_BorrowFundReplaceManage = 316,
            AccountingEntry_District_BorrowOtherFundsReplace_Search = 31601,
            AccountingEntry_District_BorrowOtherFundsReplace_Add = 31602,
            AccountingEntry_District_BorrowOtherFundsReplace_Del = 31603,

            AccountingEntry_District_PayOtherFundsReplace_Search = 31604,
            AccountingEntry_District_PayOtherFundsReplace_Add = 31605,
            AccountingEntry_District_PayOtherFundsReplace_Del = 31606,

            AccountingEntry_District_OtherFundsBorrowReplace_Search = 31607,
            AccountingEntry_District_OtherFundsBorrowReplace_Add = 31608,
            AccountingEntry_District_OtherFundsBorrowReplace_Del = 31609,

            AccountingEntry_District_OtherFundsPayReplace_Search = 31610,
            AccountingEntry_District_OtherFundsPayReplace_Add = 31611,
            AccountingEntry_District_OtherFundsPayReplace_Del = 31612,


            //Nộp ngân hàng thay thế
            AccountingEntry_District_PayBankReplaceManage = 317,
            AccountingEntry_District_ReceiveTGNHReplace_Search = 31701,
            AccountingEntry_District_ReceiveTGNHReplace_Add = 31702,
            AccountingEntry_District_ReceiveTGNHReplace_Del = 31703,

            AccountingEntry_District_SendTGNHReplace_Search = 31704,
            AccountingEntry_District_SendTGNHReplace_Add = 31705,
            AccountingEntry_District_SendTGNHReplace_Del = 31706,
            #endregion

            #region Bưu điện Tỉnh
            // 2 - Province
            FunBranch = 2,

            System_Branch = 21,
            //Quản lý đơn vị
            System_Branch_PoManage = 201,
            System_Branch_PoManage_Add = 20101,
            System_Branch_PoManage_Del = 20102,
            System_Branch_PoManage_Edit = 20103,
            System_Branch_PoManage_Lock = 20104,

            //Quản lý Người dùng
            System_Branch_UserManage = 202,
            System_Branch_UserManage_Add = 20201,
            System_Branch_UserManage_Del = 20202,
            System_Branch_UserManage_Edit = 20203,
            System_Branch_UserManage_Lock = 20204,

            //Phân quyền người dùng
            System_Branch_RoleManage = 209,

            //Thông số quỹ đầu tháng
            System_Branch_MonthlyFundInfo = 210,

            //Lập báo cáo Ngân vụ
            AccountingBranch = 22,
            //Thống kê báo cáo toàn Tỉnh
            BusinessSupport_Branch_ReportPOManage = 212,

            //Tổng hợp, Thống kê
            StatisticGeneral = 23,


            //Hỗ trợ nghiệp vụ Bưu điện Tỉnh
            BusinessSupport_Branch = 24,
            //Hạn mức lưu quỹ tiền mặt
            BusinessSupport_Branch_FundLimitsManage = 210,

            //Cảnh báo lưu quỹ vượt hạn mức
            BusinessSupport_Branch_SaveCashWarning = 211,

            //Kế toán Quỹ BĐT
            AccountingEntry_Branch = 25,
            //Tiếp nộp quỹ 
            FuntionBranch_AllocateFundManage = 203,
            FuntionBranch_AllocateFundManage_FundsReceive_Search = 20301,
            FuntionBranch_AllocateFundManage_FundsReceive_Add = 20302,
            FuntionBranch_AllocateFundManage_FundsReceive_Del = 20303,

            FuntionBranch_AllocateFundManage_FundsSend_Search = 20304,
            FuntionBranch_AllocateFundManage_FundsSend_Add = 20305,
            FuntionBranch_AllocateFundManage_FundsSend_Del = 20306,


            //Vay quỹ khác
            FuntionBranch_BorrowFundManage = 204,
            FuntionBranch_BorrowFundManage_BorrowOtherFunds_Search = 20401,
            FuntionBranch_BorrowFundManage_BorrowOtherFunds_Add = 20402,
            FuntionBranch_BorrowFundManage_BorrowOtherFunds_Del = 20403,

            FuntionBranch_BorrowFundManage_PayOtherFunds_Search = 20404,
            FuntionBranch_BorrowFundManage_PayOtherFunds_Add = 20405,
            FuntionBranch_BorrowFundManage_PayOtherFunds_Del = 20406,

            FuntionBranch_BorrowFundManage_OtherFundsBorrow_Search = 20407,
            FuntionBranch_BorrowFundManage_OtherFundsBorrow_Add = 20408,
            FuntionBranch_BorrowFundManage_OtherFundsBorrow_Del = 20409,

            FuntionBranch_BorrowFundManage_OtherFundsPay_Search = 20410,
            FuntionBranch_BorrowFundManage_OtherFundsPay_Add = 20411,
            FuntionBranch_BorrowFundManage_OtherFundsPay_Del = 20412,

            //Nộp ngân hàng
            FuntionBranch_BorrowFundManage_TGNH = 205,
            FuntionBranch_BorrowFundManage_ReceiveTGNH_Search = 20501,
            FuntionBranch_BorrowFundManage_ReceiveTGNH_Add = 20502,
            FuntionBranch_BorrowFundManage_ReceiveTGNH_Del = 20503,

            FuntionBranch_BorrowFundManage_SendTGNH_Search = 20504,
            FuntionBranch_BorrowFundManage_SendTGNH_Add = 20505,
            FuntionBranch_BorrowFundManage_SendTGNH_Del = 20506,


            //Nhập thay thế tiếp nộp quỹ
            AccountingEntry_Branch_AllocateFundReplaceManage = 206,
            AccountingEntry_Branch_FundsReceiveReplace_Search = 20601,
            AccountingEntry_Branch_FundsReceiveReplace_Add = 20602,
            AccountingEntry_Branch_FundsReceiveReplace_Del = 20603,


            AccountingEntry_Branch_FundsSendReplace_Search = 20604,
            AccountingEntry_Branch_FundsSendReplace_Add = 20605,
            AccountingEntry_Branch_FundsSendReplace_Del = 20606,


            //Nhập thay thế vay trả quỹ khác
            AccountingEntry_Branch_BorrowFundReplaceManage = 207,
            AccountingEntry_Branch_BorrowOtherFundsReplace_Search = 20701,
            AccounSystem_BranchtingEntry_Branch_BorrowOtherFundsReplace_Add = 20702,
            AccountingEntry_Branch_BorrowOtherFundsReplace_Del = 20703,

            AccountingEntry_Branch_PayOtherFundsReplace_Search = 20704,
            AccountingEntry_Branch_PayOtherFundsReplace_Add = 20705,
            AccountingEntry_Branch_PayOtherFundsReplace_Del = 20706,

            AccountingEntry_Branch_OtherFundsBorrowReplace_Search = 20707,
            AccountingEntry_Branch_OtherFundsBorrowReplace_Add = 20708,
            AccountingEntry_Branch_OtherFundsBorrowReplace_Del = 20709,

            AccountingEntry_Branch_OtherFundsPayReplace_Search = 20710,
            AccountingEntry_Branch_OtherFundsPayReplace_Add = 20711,
            AccountingEntry_Branch_OtherFundsPayReplace_Del = 20712,


            //Nộp ngân hàng thay thế
            AccountingEntry_Branch_PayBankReplaceManage = 208,
            AccountingEntry_Branch_ReceiveTGNHReplace_Search = 20801,
            AccountingEntry_Branch_ReceiveTGNHReplace_Add = 20802,
            AccountingEntry_Branch_ReceiveTGNHReplace_Del = 20803,

            AccountingEntry_Branch_SendTGNHReplace_Search = 20804,
            AccountingEntry_Branch_SendTGNHReplace_Add = 20805,
            AccountingEntry_Branch_SendTGNHReplace_Del = 20806,
            #endregion

            #region Tổng công ty
            //1 - Center
            FunCenter = 1,

            //Hệ thống
            System_Center = 11,
            //Quản lý đơn vị
            System_Center_PoManage = 101,
            System_Center_PoManage_Add = 10101,
            System_Center_PoManage_Del = 10102,
            System_Center_PoManage_Edit = 10103,
            System_Center_PoManage_Lock = 10104,

            //Quản lý người dùng
            System_Center_UserManage = 102,
            System_Center_UserManage_Add = 10201,
            System_Center_UserManage_Del = 10202,
            System_Center_UserManage_Edit = 10203,
            System_Center_UserManage_Lock = 10204,

            //Phân quyền Người Dùng
            System_Center_RoleManage = 103,

            //Tham số dịch vụ
            ServiceParam_Center_Manage = 12,

            //Danh mục báo cáo
            ServiceParam_Center_ReportList = 104,

            //Loại chỉ tiêu
            ServiceParam_Center_ItemType = 105,

            //Nhóm chỉ tiêu
            ServiceParam_Center_ItemGroup = 106,

            //Danh mục chỉ tiêu
            ServiceParam_Center_ItemList = 107,

            //Danh mục chỉ tiêu kết nối
            ServiceParam_Center_MappingManager = 108,

            //Bút toán kết chuyển
            ServiceParam_Center_TransferEntryManager = 109,
            #endregion
        }
    }
}