var Config_Process = {
    ViewConfigCreate: function (id, reportID, itemGroupID, refType, cashFllowID, parentId, VisibleLevel, VisibleIndex) {

        var dataRequest = {
            id: id,
            reportID: reportID,
            itemGroupID: itemGroupID,
            cashFllowID: cashFllowID,
            parentId: parentId,
            reportType: refType,
            VisibleLevel: VisibleLevel,
            VisibleIndex: VisibleIndex
        };

        var urlApi = Config_Process.GetUrlAPI(refType, ActionMode.Create);

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer_cf").html(data);
                $("#tempModal_cf").modal("show");
            }
        });
    },

    ConfigSuccess: function (data, refType, reportID, itemGroupId) {
        common.EndLoading();
        if (data.Code == "00") {
            $("#tempModal_cf").modal('hide');
            Config_Process.ListReportConfig(refType, reportID, itemGroupId);
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    ListReportConfig: function (refType, reportID, itemGroupID) {
        common.StartLoading();
        var dataRequest = { reportID: reportID, itemGroupID: itemGroupID, reportType: refType };
        var urlApi = Config_Process.GetUrlAPI(refType, ActionMode.Get);

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#Part" + itemGroupID).html(data);
            }
        });
    },

    DeleteConfig: function (id, itemName, refType, reportID, itemGroupID) {
        var urlApi = Config_Process.GetUrlAPI(refType, ActionMode.Delete);
        console.log(urlApi);
        var html = '<div id="confirmDelDic" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có muốn xóa chỉ tiêu [' + itemName + '] không?</div>';
        html += '<div class="modal-footer">';
        html += '<button type="button" class="btn  btn-primary btn-sm confirmDel">Đồng ý</button>';
        html += '<button type="button" class="btn  btn-danger btn-sm reject" data-dismiss="modal">Hủy bỏ</button>';
        html += '</div></div></div>';

        $("#confirmModal").html(html);
        $("#confirmDelDic").modal('show');

        $('.confirmDel').click(function () {
            $("#confirmDelDic").modal('hide');
            var token = $('input[name="__RequestVerificationToken"]', ConfigForm).val();
            var dataRequest = { id: id, reportID: reportID, itemGroupID: itemGroupID, __RequestVerificationToken: token };
            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    common.Message("Thông báo", data.Mes, "success", true);
                    Config_Process.ListReportConfig(refType, reportID, itemGroupID);
                }
                else {
                    common.Message("Thông báo", data.Mes, "error", true);
                }
            });
        });
    },

    GetUrlAPI: function (refType, action) {
        var controllerName = "";
        if (action == ActionMode.Delete)
        {
            controllerName = "Config";
        }
        else {
            controllerName = Config_Process.GetControlerName(refType);
        }

        return "Admin/ReportConfiguration/" + controllerName + action;
    },

    GetControlerName: function (refType) {
        switch (refType) {
            case "01CD":
                return "ConfigCD";                      //Báo cáo cân đối          
            case "02CD":
                return "ConfigCD";
            case "03CD":
                return "ConfigCD";
            case "04CD":
                return "ConfigCD";
            case "01TH":
                return "ConfigTH";
            case "02TH":
                return "ConfigTH";
            case "03TH":                                //Báo cáo tổng hợp
                return "ConfigTH";
            case "04TQ":
                return "ConfigTQ";
        }
    },
        
    GetSelectedItem: function () {
        if ($("#chkIsEmpty").is(':checked'))
        {
            common.Submitform('fReportConfigCreate');
        }
        else
        {
            var exists = false;
            var value = $('input[name="ItemId"]').val();
            $('#tblListItem').find('tr').each(function () {
                var row = $(this);
                if (row.find('input[type="checkbox"]').is(':checked')) {
                    var sId = row.context.cells[1].innerText;
                    var oValue = parseInt(sId.replace(/\,/g, ''));
                    $('input[name="ItemId"]').val(oValue);
                    exists = true;
                    common.Submitform('fReportConfigCreate');
                }
            }
            );
            if (!exists && value == "0") {
                common.Message("Thông báo", "Bạn chưa chọn chỉ tiêu.", "warning", true);
            }
            else if (value != "0")
                common.Submitform('fReportConfigCreate');
        }
        
    },

    SubDomain: "/",

    PageIndex: 1
}