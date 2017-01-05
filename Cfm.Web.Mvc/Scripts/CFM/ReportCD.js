ReportCD_Process = {
    GetReportData: function (reportId, isAgain, PoId) {
        var dataRequest = {
            reportId: reportId,
            reportDate: $('#ReportDate').val(),
            isAgain: isAgain,
            PoId: PoId
        };

        var urlApi = "CFMCounter/Accounting/Report04CDGetData";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $('#tabGeneral').addClass("active");
                $('#tabDetail').removeClass("active");
                $("#reportcd").html(data);

            }
        });

    },
    GetReplaceReportData: function (reportId, isAgain, PoId) {
        var dataRequest = {
            reportId: reportId,
            reportDate: $('#ReportDate').val(),
            isAgain: isAgain,
            PoId: PoId
        };

        var urlApi = "CFMDistrict/Accounting/Report04CDReplaceGetData";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $('#tabGeneral').addClass("active");
                $('#tabDetail').removeClass("active");
                $("#reportcd").html(data);

            }
        });

    },
    LoadReportManage: function (refType, pageIndex) {
        common.StartLoading();
        var dataRequest = { dateRange: $('#txtDateRange').val(), reportStatus: $('#cboReportStatus').val(), pageIndex: pageIndex };
        var urlApi = "CFMCounter/Accounting/Report04CDManageGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tblReport").html(data);
            }
        });
    },

    LoadCD04ReplaceManage: function (refType, pageIndex) {
        common.StartLoading();
        var dataRequest = { PoId: $('input[name="PoId"]').val(), dateRange: $('#txtDateRange').val(), reportStatus: $('#cboReportStatus').val(), pageIndex: pageIndex };
        var urlApi = "CFMDistrict/Accounting/Report04CDReplaceManageGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tblReport").html(data);
            }
        });
    },

    Get03ReportData: function (reportId, isAgain, PoId) {
        var dataRequest = {
            reportId: reportId,
            reportDate: $('#ReportDate').val(),
            isAgain: isAgain,
            PoId: PoId
        };

        var urlApi = "CFMDistrict/Accounting/Report03CDGetData";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $('#TTTT').addClass("active");
                $('#TTTDV').removeClass("active");
                $('#TKBD').removeClass("active");
                $('#KDDV').removeClass("active");
                $("#reportcd").html(data);

            }
        });

    },

    Get03CDPOReportData: function (reportId, isAgain, PoId) {
        var dataRequest = {
            reportId: reportId,
            reportDate: $('#ReportDate').val(),
            isAgain: isAgain,
            PoId: $('#PoId').val()
        };

        var urlApi = "CFMDistrict/Accounting/Report03CDPOGetData";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                //$('#TTTT').addClass("active");
                //$('#TTTDV').removeClass("active");
                //$('#TKBD').removeClass("active");
                //$('#KDDV').removeClass("active");
                $("#reportcd").html(data);

            }
        });

    },

    Load03ReportManage: function (refType, pageIndex) {
        common.StartLoading();
        var dataRequest = { dateRange: $('#txtDateRange').val(), reportStatus: $('#cboReportStatus').val(), pageIndex: pageIndex };
        var urlApi = "CFMDistrict/Accounting/Report03CDManageGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tblReport").html(data);
            }
        });
    },

    LoadCD03ReplaceManage: function (refType, pageIndex) {
        common.StartLoading();
        var dataRequest = { PoId: $('input[name="PoId"]').val(), dateRange: $('#txtDateRange').val(), reportStatus: $('#cboReportStatus').val(), pageIndex: pageIndex };
        var urlApi = "CFMBranch/Accounting/Report03CDReplaceManageGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tblReport").html(data);
            }
        });
    },

    LoadPOCDReportManage: function (refType, pageIndex) {
        common.StartLoading();
        var dataRequest = { PoId: $('input[name="PoId"]').val(), dateRange: $('#txtDateRange').val(), reportStatus: $('#cboReportStatus').val(), pageIndex: pageIndex };
        var urlApi = "CFMDistrict/Accounting/ReportPOManageGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tblReport").html(data);
            }
        });
    },
    LoadReviewQualityPO: function (refType, pageIndex) {
        common.StartLoading();
        var dataRequest = { PoId: $('input[name="PoId"]').val(), dateRange: $('#txtDateRange').val(), reportStatus: $('#cboReportStatus').val(), pageIndex: pageIndex };
        var urlApi = "CFMDistrict/Accounting/ReviewQualityPOGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tblReport").html(data);
            }
        });
    },
    LoadReviewQualityDistrict: function (refType, pageIndex) {
        common.StartLoading();
        var dataRequest = { dateRange: $('#txtDateRange').val(), pageIndex: pageIndex };
        var urlApi = "CFMDistrict/Accounting/ReviewQualityDistrictGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tblReport").html(data);
            }
        });
    },
    ViewListPO: function (func, ReportID, ReportDate, flag, isLoadParent, ChosePo, ChoseD, ChoseP, ChoseT, IsSubtract) {
        var dataRequest = {
            func: func,
            isLoadParent: isLoadParent,
            ChosePo: ChosePo,
            ChoseD: ChoseD,
            ChoseP: ChoseP,
            ChoseT: ChoseT,
            IsSubtract: IsSubtract
        };
        var urlApi = "Admin/POCommon/ListPO";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer_PO").html(data);
                $("#tempModal_PO").modal("show");
                common.PoSelect.ID = 0;
                common.PoSelect.ParentID = 0;
                common.PoSelect.Code = "";
                common.PoSelect.Name = "";
                common.PoSelect.POLevel = "";
                common.PoSelect.IsCenter = "";
                common.PoSelect.Address = "";
                common.PoSelect.PhoneNumber = "";
                common.PoSelect.FaxNumber = "";
                common.PoSelect.IsOffline = "";
                common.PoSelect.CycleDate = "";
                common.PoSelect.IsLock = "";
                common.ReportID = ReportID;
                common.Flag = flag;
                common.ReportDate = ReportDate == null ? "" : ReportDate;
            }
        });

    },

    LoadPoChild: function (parentId) {
        if ($("#po_child_" + parentId).hasClass("loaded")) {
            $("#po_child_" + parentId).toggle(100);
        }
        else {
            var ChosePo = $("#ChosePo").val();
            var ChoseD = $("#ChoseD").val();
            var ChoseP = $("#ChoseP").val();
            var ChoseT = $("#ChoseT").val();
            var IsSubtract = $("#IsSubtract").val();

            var dataRequest = {
                ParentId: parentId,
                ChosePo: ChosePo,
                ChoseD: ChoseD,
                ChoseP: ChoseP,
                ChoseT: ChoseT,
                IsSubtract: IsSubtract
            };
            var urlApi = "Admin/POCommon/ListPoChild";
            common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                $("#po_child_" + parentId).addClass("loaded");
                if (data == null) {

                }
                else {
                    $("#po_child_" + parentId).html(data);
                }
            });
        }
    },

    SelectPo: function (func) {
        var checkbox = $("input:checkbox[class=poCheckBox]");
        var CountCheck = 0;
        $.each(checkbox, function (i, v) {
            if ($(checkbox[i]).is(':checked')) {
                CountCheck++;
            }
        });

        if (CountCheck != 1) {
            common.Message("Thông báo", "Bạn phải chọn một Bưu cục để thực hiện làm việc!", "warning");
        }
        else {
            $.each(checkbox, function (i, v) {
                if ($(checkbox[i]).is(':checked')) {
                    var item = $(checkbox[i]);
                    common.PoSelect.ID = $(checkbox[i]).data("id");
                    common.PoSelect.ParentID = $(checkbox[i]).data("parentid");
                    common.PoSelect.Code = $(checkbox[i]).data("code");
                    common.PoSelect.Name = $(checkbox[i]).data("name");
                    common.PoSelect.POLevel = $(checkbox[i]).data("polevel");
                    common.PoSelect.IsCenter = $(checkbox[i]).data("iscenter");
                    common.PoSelect.Address = $(checkbox[i]).data("address");
                    common.PoSelect.PhoneNumber = $(checkbox[i]).data("phonenumber");
                    common.PoSelect.FaxNumber = $(checkbox[i]).data("faxnumber");
                    common.PoSelect.IsOffline = $(checkbox[i]).data("isoffline");
                    common.PoSelect.CycleDate = $(checkbox[i]).data("cycledate");
                    common.PoSelect.IsLock = $(checkbox[i]).data("islock");
                    func();
                }
            });
        }
    },

    func: function () {
        $("#tempModal_PO").modal("hide");
        $('input[name="PoId"]').val(common.PoSelect.ID);
        $("#txtPoCode").val(common.PoSelect.Code);
        $("#txtPoName").val(common.PoSelect.Name);
    },

    Report04CDReplaceLink: function () {
        var html = "/CFMDistrict/Accounting/Report04CDReplace?poId=" + common.PoSelect.ID + "&reportID=" + common.ReportID + "&reportDate=" + common.ReportDate;
        window.location.href = html;
    },

    Report03CDReplaceLink: function () {
        var html = "/CFMBranch/Accounting/Report03CDReplace?poId=" + common.PoSelect.ID + "&reportID=" + common.ReportID + "&reportDate=" + common.ReportDate;
        window.location.href = html;
    },

    Report03CDPOLink: function () {
        var html = "/CFMDistrict/Accounting/Report03CDPO?poId=" + common.PoSelect.ID + "&reportID=" + common.ReportID + "&reportDate=" + common.ReportDate;
        window.location.href = html;
    },

    SelectPoForAccEntryReplace: function () {
        $("#tempModal_PO").modal("hide");
        $('input[name="PoIdTemp1"]').val(common.PoSelect.ID);
        $("#txtPoCode").val(common.PoSelect.Code);
        $("#txtPoName").val(common.PoSelect.Name);
        ReportCD_Process.GetOrdinalNumber();
    },

    GetOrdinalNumber: function () {
        var dataRequest = {
            poId: $('input[name="PoIdTemp1"]').val(),
            refType: $('input[name="RefType"]').val()
        };
        var urlApi = "CFMDistrict/AccountingEntry/GetOrdinalNumber";
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Json, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $('#txtOrdinalNumber').val(data.Value);
            }
        });
    },

    SelectPoForBorrowReplace: function () {
        $("#tempModal_PO").modal("hide");
        $('input[name="PoId"]').val(common.PoSelect.ID);
        $("#txtPoCode").val(common.PoSelect.Code);
        $("#txtPoName").val(common.PoSelect.Name);
        ReportCD_Process.GetBorrowOrdinalNumber();
    },

    SelectPoForAllocateFundReplace1: function () {
        $("#tempModal_PO").modal("hide");
        $("#PoIdTemp1").val(common.PoSelect.ID);
        $("#txtPoCode1").val(common.PoSelect.Code);
        $("#txtPoName1").val(common.PoSelect.Name);
        ReportCD_Process.GetOrdinalNumber();
    },

    SelectPoForAllocateFundReplace2: function () {
        $("#tempModal_PO").modal("hide");
        $("#PoIdTemp2").val(common.PoSelect.ID);
        $("#txtPoCode2").val(common.PoSelect.Code);
        $("#txtPoName2").val(common.PoSelect.Name);
        //ReportCD_Process.GetBorrowOrdinalNumber();
    },

    SelectPoForAllocateFund: function () {
        $("#tempModal_PO").modal("hide");
        $("#PoIdTemp1").val(common.PoSelect.ID);
        $("#txtPoCode").val(common.PoSelect.Code);
        $("#txtPoName").val(common.PoSelect.Name);
    },

    SelectPoStandard: function () {
        $("#tempModal_PO").modal("hide");
        $('input[name="PoId"]').val(common.PoSelect.ID);
        $("#txtPoCode").val(common.PoSelect.Code);
        $("#txtPoName").val(common.PoSelect.Name);
        //ReportCD_Process.GetBorrowOrdinalNumber();
    },
    SelectPoStandardDetail: function () {
        $("#tempModal_PO").modal("hide");
        $('#PoIdDetail').val(common.PoSelect.ID);
        $("#txtPoCodeDetail").val(common.PoSelect.Code);
        $("#txtPoNameDetail").val(common.PoSelect.Name);
        //ReportCD_Process.GetBorrowOrdinalNumber();
    },
 
    GetBorrowOrdinalNumber: function () {
        var dataRequest = {
            poId: $('input[name="PoId"]').val(),
            refType: $('input[name="RefType"]').val()
        };

        if (dataRequest.poId == undefined || dataRequest.poId == null || dataRequest.poId == '' || dataRequest.poId == '0' || dataRequest.poId == 0)
        {
            dataRequest.poId = $('#PoIdTemp2').val();
        }

        var urlApi = "CFMDistrict/AccountingEntry/GetOrdinalNumber";
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Json, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $('#txtOrdinalNumber').val(data.Value);
            }
        });
    },

    ReportCDSuccess: function (data) {
        common.EndLoading();
        if (data.Code == "00") {
            common.Message("Thông báo", data.Mes, "success");
            $('input[name="AdjEntryId"]').val(0);
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }

    },

    ReportCDApprove: function (poId) {
        var dataRequest = {
            poId: poId,
            reportDate: $('#ReportDate').val()
        };
        var urlApi = "CFMCounter/Accounting/Report04CDApprove";
        common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
            if (data == null) {
                common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "warning");

            }
            else {
                if (data.Code == "00")
                    common.Message("Thông báo", data.Mes, "success");
                else
                    common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "warning");
            }
        });
    },

    ConfirmCancel: function (Id, poId, reportDate, poName) {
      
        var html = '<div id="confirmDelDic" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có chắc chắn muốn bỏ xác nhận báo cáo ngày ' + '[' + reportDate + '] của Bưu cục ' + '[' + poName + '] không?</div>';
        html += '<div class="modal-footer">';
        html += '<button type="button" class="btn  btn-primary btn-sm confirmDel">Đồng ý</button>';
        html += '<button type="button" class="btn  btn-danger btn-sm reject" data-dismiss="modal">Hủy bỏ</button>';
        html += '</div></div></div>';

        $("#confirmModal").html(html);
        $("#confirmDelDic").modal('show');

        $('.confirmDel').click(function () {
            ReportCD_Process.ReportCD04CancelApprove(Id, poId, reportDate);
        });
    },

    ReportCD04CancelApprove: function (Id, poId, reportDate) {
        var dataRequest = {
            Id: Id,
            poId: poId,
            reportDate: reportDate
        };
        var urlApi = "CFMDistrict/Accounting/Report04CDCancelApprove";
        common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
            if (data == null) {
                common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "warning");

            }
            else {
                if (data.Code == "00") {
                    common.Message("Thông báo", data.Mes, "success");
                    ReportCD_Process.LoadCD04ReplaceManage(0, 1);
                }
                else
                    common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "warning");
            }
        });
    },

    ReportTypeChange: function(e){
        var ReportType = $(e).val();
        if (ReportType == "04CD")
        {
            $("#groupUnit").css({ "display": "" });
        }
        else {
            $("#groupUnit").css({ "display": "none" });
            $("#PoId").val("");
            $("#txtPoCode").val("");
            $("#txtPoName").val("");
        }
    },

    SaveCashWarningGet: function (Illusion, PageIndex) {
        var ReportType = $("#ListReportType").val();
        var PoId = $("#PoId").val();
        var PoCode = $("#txtPoCode").val();
        var PoName = $("#txtPoName").val();
        var DateRange = $("#txtDateRangeOne").val();
        var Status = $("#ListStatus").val();

        if(ReportType == "04CD")
        {
            ReportCD_Process.SaveCashWarning04CDGet(PoId, PoCode, PoName, DateRange, Status, PageIndex);
        }
        else if(ReportType == "03CD")
        {
            ReportCD_Process.SaveCashWarning03CDGet(DateRange, Status, PageIndex);
        }

    },

    SaveCashWarning03CDGet: function (dateRange, Status, PageIndex) {
        var dataRequest = {
            dateRange: dateRange,
            Status: Status,
            PageIndex: PageIndex
        };
        var urlApi = "CFMBranch/Accounting/SaveCashWarning03CDGet";
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {
                common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "warning");

            }
            else {
                $("#ListSaveCashWarning").html(data);
            }
        });
    },

    SaveCashWarning04CDGet: function (PoId, PoCode, PoName, DateRange, Status, PageIndex) {
        if (common.IsNullOrEmpty(PoId) || common.IsNullOrEmpty(PoCode) || common.IsNullOrEmpty(PoName)) {
            common.Message("Thông báo", "Bạn chưa chọn Đơn vị xem Cảnh báo!", "warning");
        }
        else {
            var dataRequest = {
                poDistrictId: PoId,
                dateRange: DateRange,
                reportStatus: Status,
                PageIndex: PageIndex
            };
            var urlApi = "CFMBranch/Accounting/SaveCashWarning04CDGet";
            common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                if (data == null) {
                    common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "warning");

                }
                else {
                    $("#ListSaveCashWarning").html(data);
                }
            });
        }
    },

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
    }
}
