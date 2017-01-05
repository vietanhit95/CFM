ReportTQ_Process = {
    ViewCreateReport: function (id, reportId) {

        var dataRequest = {
            id: id,
            reportId: reportId
        };

        var urlApi = "CFMCounter/Accounting/Report04TQCreate";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
        });
    },
    ViewCreate03TQReport: function (id, reportId, poId) {

        var dataRequest = {
            id: id,
            reportId: reportId,
            poId: poId
        };

        var urlApi = "CFMDistrict/Accounting/Report03TQCreate";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
        });
    },

    CreateReportSuccess: function (data, reportId) {
        common.EndLoading();
        if (data.Code == "00") {
            $("#tempModal").modal('hide');
            ReportTQ_Process.LoadReportManage(0, 1, reportId);
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    GetReportData: function (reportId, isAgain) {
        var dataRequest = {
            reportId: reportId,
            reportDate: $('#ReportDate').val(),
            isAgain: isAgain
        };

        var urlApi = "CFMCounter/Accounting/Report04TQGetData";

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

    GetReportTQData: function (reportId) {
        var dataRequest = {
            reportId: reportId,
            reportDate: $('#ReportDate').val(),
            isSummary:'Y'
        };

        var urlApi = "CFMCounter/Accounting/Report04TQGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
             
                $("#tblReportDetail").html(data);

            }
        });

    },
    GetReport03TQData: function (reportId) {
        if ($('#PoIdDetail').val() != null && $('#PoIdDetail').val() != "" && $('#PoIdDetail').val() != NaN && $('#PoIdDetail').val() != undefined)
        {
            var dataRequest = {
                reportId: reportId,
                reportDate: $('#ReportDate').val(),
                poId: $('#PoIdDetail').val(),
                isSummary: 'Y'
            };

            var urlApi = "CFMDistrict/Accounting/Report03TQGet";

            common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                if (data == null) {

                }
                else {

                    $("#tblReportDetail").html(data);

                }
            });
        }
        else
        {
            common.Message("Thông báo", "Mã lỗi: 90 - Bạn chưa chọn Bưu cục.", "warning", true);
        }
    },
    LoadReportManage: function (refType, pageIndex, reportId) {
        common.StartLoading();
        var dataRequest = { dateRange: $('#txtDateRange').val(), reportId: reportId, pageIndex: pageIndex };
        var urlApi = "CFMCounter/Accounting/Report04TQManageGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tblReport").html(data);
            }
        });
    },

    Load03TQReportManage: function (refType, pageIndex, reportId) {
        common.StartLoading();
        var dataRequest = { dateRange: $('#txtDateRange').val(), reportId: reportId, poId: $('input[name="PoId"]').val(), pageIndex: pageIndex };
        var urlApi = "CFMDistrict/Accounting/Report03TQManageGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tblReport").html(data);
            }
        });
    },

    DeleteReport: function (id, reportDate, reportId) {
        var urlApi = "CFMCounter/Accounting/Report04TQDelete";
        console.log(urlApi);
        var html = '<div id="confirmDelEntry" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có muốn xóa Báo cáo tiếp quỹ ngày [' + reportDate + '] không?</div>';
        html += '<div class="modal-footer">';
        html += '<button type="button" class="btn  btn-primary btn-sm confirmDel">Đồng ý</button>';
        html += '<button type="button" class="btn  btn-danger btn-sm reject" data-dismiss="modal">Hủy bỏ</button>';
        html += '</div></div></div>';

        $("#confirmModal").html(html);
        $("#confirmDelEntry").modal('show');

        $('.confirmDel').click(function () {
            $("#confirmDelEntry").modal('hide');
           
            var dataRequest = { id: id };
            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    common.Message("Thông báo", data.Mes, "success", true);
                    ReportTQ_Process.LoadReportManage(0, 1, reportId);
                }
                else {
                    common.Message("Thông báo", data.Mes, "error", true);
                }
            });
        });
    },

    GetTQTotal: function (rPoID, rFrom_date, rToDate) {
        var oParam = { "From_date": "", "To_date": "", "PO_ID": ""};
        oParam.From_date = rFrom_date;
        oParam.To_date = rToDate;
        oParam.PO_ID = rPoID;
        var url = "/Areas/CFMReport/ReportView/ReportViewer.aspx?Report_code=TQ04&ViewType=0&From_date=" + oParam.From_date + "&To_date=" + oParam.To_date + "&PO_ID=" + oParam.PO_ID;
        window.open(url, '_blank');
    },
}