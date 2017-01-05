Report_Process = {
    GetCDTotal: function (rPoID, sReportCode, iViewType) {
        var oParam = { "From_date": "", "To_date": "", "PO_ID": "", "Report_code": "", "ViewType": "" };
        oParam.From_date = $("#ReportDate").val();
        oParam.To_date = $("#ReportDate").val();
        oParam.PO_ID = rPoID;
        oParam.ViewType = iViewType;
        oParam.Report_code = sReportCode;
        var url = "/Areas/CFMReport/ReportView/ReportViewer.aspx?Report_code=" + oParam.Report_code + "&ViewType=" + oParam.ViewType + "&From_date=" + oParam.From_date + "&To_date=" + oParam.To_date + "&PO_ID=" + oParam.PO_ID;
        window.open(url, '_blank');
    },

    ViewSummaryReport: function (rPoID, rFromDate, rToDate, iViewType, sReportCode) {
        var oParam = { "From_date": "", "To_date": "", "PO_ID": "", "Report_code": "", "ViewType": "" };
        oParam.From_date = rFromDate;
        oParam.To_date = rToDate;
        oParam.PO_ID = rPoID;
        oParam.ViewType = iViewType;
        oParam.Report_code = sReportCode;
        var url = "/Areas/CFMReport/ReportView/ReportViewer.aspx?Report_code=" + oParam.Report_code + "&ViewType=" + oParam.ViewType + "&From_date=" + oParam.From_date + "&To_date=" + oParam.To_date + "&PO_ID=" + oParam.PO_ID;
        window.open(url, '_blank');
    },

    Get04CDTotal: function (rPoID, sReportCode, iViewType) {
        var oParam = { "From_date": "", "To_date": "", "PO_ID": "", "Report_code": "", "ViewType": "" };
        oParam.From_date = $("#ReportDate").val();
        oParam.To_date = $("#ReportDate").val();
        oParam.PO_ID = rPoID;
        oParam.ViewType = iViewType;
        oParam.Report_code = sReportCode;
        var url = "/Areas/CFMReport/ReportView/Report04CD.aspx?Report_code=" + oParam.Report_code + "&ViewType=" + oParam.ViewType + "&From_date=" + oParam.From_date + "&To_date=" + oParam.To_date + "&PO_ID=" + oParam.PO_ID;
        window.open(url, '_blank');
    },

    View04CDTotal: function (rPoID, rFromDate, rToDate, iViewType, sReportCode) {
        var oParam = { "From_date": "", "To_date": "", "PO_ID": "", "Report_code": "", "ViewType": "" };
        oParam.From_date = rFromDate;
        oParam.To_date = rToDate;
        oParam.PO_ID = rPoID;
        oParam.ViewType = iViewType;
        oParam.Report_code = sReportCode;
        var url = "/Areas/CFMReport/ReportView/Report04CD.aspx?Report_code=" + oParam.Report_code + "&ViewType=" + oParam.ViewType + "&From_date=" + oParam.From_date + "&To_date=" + oParam.To_date + "&PO_ID=" + oParam.PO_ID;
        window.open(url, '_blank');
    },

    Get03THTotal: function (iTerm_id, iMonth_id, iYear_id, rPoID, sReportCode) {
        var oParam = { "Term_id": "", "Month_id": "", "Year_id": "", "PO_ID": "", "Report_code": "" };
        oParam.Term_id = iTerm_id;
        oParam.Month_id = iMonth_id;
        oParam.Year_id = iYear_id;
        oParam.PO_ID = rPoID;
        oParam.Report_code = sReportCode;
        var url = "/Areas/CFMReport/ReportView/Report03TH.aspx?Report_code=" + oParam.Report_code + "&Term_id=" + oParam.Term_id + "&Month_id=" + oParam.Month_id + "&Year_id=" + oParam.Year_id + "&PO_ID=" + oParam.PO_ID;
        window.open(url, '_blank');
    },

    Get02THTotal: function (iTerm_id, iMonth_id, iYear_id, rPoID, sReportCode) {
        var oParam = { "Term_id": "", "Month_id": "", "Year_id": "", "PO_ID": "", "Report_code": "" };
        oParam.Term_id = iTerm_id;
        oParam.Month_id = iMonth_id;
        oParam.Year_id = iYear_id;
        oParam.PO_ID = rPoID;
        oParam.Report_code = sReportCode;
        var url = "/Areas/CFMReport/ReportView/Report02TH.aspx?Report_code=" + oParam.Report_code + "&Term_id=" + oParam.Term_id + "&Month_id=" + oParam.Month_id + "&Year_id=" + oParam.Year_id + "&PO_ID=" + oParam.PO_ID;
        window.open(url, '_blank');
    },

    Get01THTotal: function (iTerm_id, iMonth_id, iYear_id, rPoID, sReportCode) {
        var oParam = { "Term_id": "", "Month_id": "", "Year_id": "", "PO_ID": "", "Report_code": "" };
        oParam.Term_id = iTerm_id;
        oParam.Month_id = iMonth_id;
        oParam.Year_id = iYear_id;
        oParam.PO_ID = rPoID;
        oParam.Report_code = sReportCode;
        var url = "/Areas/CFMReport/ReportView/Report01TH.aspx?Report_code=" + oParam.Report_code + "&Term_id=" + oParam.Term_id + "&Month_id=" + oParam.Month_id + "&Year_id=" + oParam.Year_id + "&PO_ID=" + oParam.PO_ID;
        window.open(url, '_blank');
    },

    GetTQTotal: function (rPoID, rFrom_date, rToDate) {
        var oParam = { "From_date": "", "To_date": "", "PO_ID": ""};
        oParam.From_date = rFrom_date;
        oParam.To_date = rToDate;
        oParam.PO_ID = rPoID;
        var url = "/Areas/CFMReport/ReportView/ReportViewer.aspx?Report_code=TQ04&ViewType=0&From_date=" + oParam.From_date + "&To_date=" + oParam.To_date + "&PO_ID=" + oParam.PO_ID;
        window.open(url, '_blank');
    },

    GetFundTotal: function (rPoID) {
        var oParam = { "From_date": "", "To_date": "", "PO_ID": "", "Report_code": ""};
        oParam.From_date = $("#ReportDate").val();
        oParam.To_date = $("#ReportDate").val();
        oParam.PO_ID = rPoID;
        oParam.Report_code = "CD03FUND";

        if (oParam.From_date == "") {            
                common.Message("Thông báo", "Bạn chưa chọn ngày in bảng kê.", "warning");
        }
        else {
            var url = "/Areas/CFMReport/ReportView/ReportViewer.aspx?Report_code=" + oParam.Report_code + "&From_date=" + oParam.From_date + "&To_date=" + oParam.To_date + "&PO_ID=" + oParam.PO_ID;
            window.open(url, '_blank');
        }
    },

    ReportMessage: function () {
        common.EndLoading();
        common.Message("Thông báo", "Không có dữ liệu báo cáo.", "success");

    },
}