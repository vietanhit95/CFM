var Mapping_Process = {
    ViewCreate: function (id, refType) {
        var dataRequest = { id: id };
        var urlApi = "Admin/Mapping/MappingCreate";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
        });
    },

    LoadDic: function (refType, PageIndex) {
        var urlApi = "Admin/Mapping/MappingGet";
        Mapping_Process.PageIndex = PageIndex;
        common.CallXhttp(
            urlApi,
            typeAjax.Get,
            dataTypeAjax.Html,
            { PageIndex: PageIndex },
            true,
            true,
            function (data) {
                $("#ListDic").html(data);
            });
    },

    DeleteDic: function (id, dicName, dicTitle, refType) {
        var urlApi = "Admin/Mapping/MappingDelete";
        var html = '<div id="confirmDelDic" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có muốn xóa ' + dicTitle + ' [' + dicName + '] không?</div>';
        html += '<div class="modal-footer">';
        html += '<button type="button" class="btn  btn-primary btn-sm confirmDel">Đồng ý</button>';
        html += '<button type="button" class="btn  btn-danger btn-sm reject" data-dismiss="modal">Hủy bỏ</button>';
        html += '</div></div></div>';

        $("#confirmModal").html(html);
        $("#confirmDelDic").modal('show');

        $('.confirmDel').click(function () {
            $("#confirmDelDic").modal('hide');
            var token = $('input[name="__RequestVerificationToken"]', DicForm).val();
            var dataRequest = { id: id, __RequestVerificationToken: token };
            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    common.Message("Thông báo", data.Mes, "success", true);
                    Mapping_Process.LoadDic(refType, Mapping_Process.PageIndex);
                }
                else {
                    common.Message("Thông báo", data.Mes, "error", true);
                }
            });
        });
    },

    OnSuccess: function (data, refType) {
        common.EndLoading();
        if (data.Code == "00") {
            var View = $("#View").val();

            Mapping_Process.LoadDic(refType, Mapping_Process.PageIndex);
            common.Message("Thông báo", data.Mes, "success");

            $("#tempModal").modal("hide");
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    GetMappingConfig: function (RefType, Page) {
        common.StartLoading();
        var Code = "";
        var Name = "";
        Dic.PageIndex = Page;
        $.ajax({
            url: "/Admin/Mapping/MappingGet",
            type: "Get",
            dataType: "html",
            data: { Code: Code, Name: Name, PageIndex: Page, forConfig: 1, searchContent: $('#txtSearchContent').val() },
            success: function (data) {
                common.EndLoading();
                $("#ListDic").html(data);
            }
        });
    },

    SearchMapping: function (RefType, Page) {
        common.StartLoading();
        var Code = "";
        var Map_Code = "";
        Mapping_Process.PageIndex = Page;
        $.ajax({
            url: "/Admin/Mapping/MappingGet",
            type: "Get",
            dataType: "html",
            data: { Map_Code: Map_Code, Code: Code, PageIndex: Page, forConfig: 0, searchContent: $('#txtSearchContent').val() },
            success: function (data) {
                common.EndLoading();
                $("#ListDic").html(data);
            }
        });
    },

    ListItem: function (Page) {
        common.StartLoading();
        var Code = "";
        var Name = "";
        Dic.PageIndex = Page;
        $.ajax({
            url: "/Admin/Dictionary/ItemListGet",
            type: "Get",
            dataType: "html",
            data: { Code: Code, Name: Name, PageIndex: Page },
            success: function (data) {
                common.EndLoading();
                $("#ListDic").html(data);
            }
        });
    },

    PageIndex: 1
}

var Transfer_Process = {
    ViewCreate: function (id, refType) {
        var dataRequest = { id: id };
        var urlApi = "Admin/Mapping/TransferEntryCreate";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
        });
    },
    SelectItem: function (ItemType) {
        var dataRequest = { reportType: $('#cboReportTypeDetail').val(), ItemType: ItemType, TransferType: $('#cboTransferTypeDetail').val() };
        var urlApi = "Admin/Mapping/ItemSelect";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer_te").html(data);
                $("#tempModal_te").modal("show");
            }
        });
    },
    SearchItem: function (RefType, Page, reportType) {
        var dataRequest = { reportType: Transfer_Process.reportType, itemType: Transfer_Process.itemType, TransferType: Transfer_Process.TransferType, PageIndex: Page, searchContent: $('#txtSearchItem').val() };
        var urlApi = "Admin/Mapping/ItemListGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {
                common.EndLoading();
            }
            else {
                common.EndLoading();
                $("#ListDicItem").html(data);

            }
        });
    },
    GetSelectedItem: function (ItemType) {

        var exists = false;
        
        $('#tblListItem').find('tr').each(function () {
            var row = $(this);
            if (row.find('input[type="checkbox"]').is(':checked')) {
                var sId = row.context.cells[1].innerText;
                var sCode = row.context.cells[2].innerText;
                var oValue = parseInt(sId.replace(/\,/g, ''));
                if (ItemType == "1")
                {
                    $('input[name="SourceItemId"]').val(oValue);
                    $('#txtSourceItemCode').val(sCode);
                }
                else
                {
                    $('input[name="TargetItemId"]').val(oValue);
                    $('#txtTargetItemCode').val(sCode);
                }
                $("#tempModal_te").modal("hide");
                exists = true;
              
            }
        }
        );
        if (!exists && value == "0") {
            common.Message("Thông báo", "Bạn chưa chọn chỉ tiêu.", "warning", true);
        }
        


    },
    LoadTransfer: function (refType, PageIndex) {
        var urlApi = "Admin/Mapping/TransferEntryGet";
        Transfer_Process.PageIndex = PageIndex;
        common.CallXhttp(
            urlApi,
            typeAjax.Get,
            dataTypeAjax.Html,
            { PageIndex: PageIndex },
            true,
            true,
            function (data) {
                $("#ListDic").html(data);
            });
    },

    DeleteTransfer: function (id, dicName, dicTitle, refType) {
        var urlApi = "Admin/Mapping/TransferEntryDelete";
        var html = '<div id="confirmDelDic" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có muốn xóa ' + dicTitle + ' đã chọn không?</div>';
        html += '<div class="modal-footer">';
        html += '<button type="button" class="btn  btn-primary btn-sm confirmDel">Đồng ý</button>';
        html += '<button type="button" class="btn  btn-danger btn-sm reject" data-dismiss="modal">Hủy bỏ</button>';
        html += '</div></div></div>';

        $("#confirmModal").html(html);
        $("#confirmDelDic").modal('show');

        $('.confirmDel').click(function () {
            $("#confirmDelDic").modal('hide');
            var token = $('input[name="__RequestVerificationToken"]', DicForm).val();
            var dataRequest = { id: id, __RequestVerificationToken: token };
            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    common.Message("Thông báo", data.Mes, "success", true);
                    Transfer_Process.SearchTransfer(refType, Transfer_Process.PageIndex);
                }
                else {
                    common.Message("Thông báo", data.Mes, "error", true);
                }
            });
        });
    },

    OnSuccess: function (data, refType) {
        common.EndLoading();
        if (data.Code == "00") {
            var View = $("#View").val();

            Transfer_Process.SearchTransfer(refType, Transfer_Process.PageIndex);
            common.Message("Thông báo", data.Mes, "success");

            $("#tempModal").modal("hide");
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    SearchTransfer: function (RefType, Page) {
        common.StartLoading();
        var Code = "";
        var Map_Code = "";
        Transfer_Process.PageIndex = Page;
        $.ajax({
            url: "/Admin/Mapping/TransferEntryGet",
            type: "Get",
            dataType: "html",
            data: { transferType: $('#cboTransferType').val(), reportType: $('#cboReportType').val(), PageIndex: Page, searchContent: $('#txtSearchContent').val() },
            success: function (data) {
                common.EndLoading();
                $("#ListDic").html(data);
            }
        });
    },

    ListItem: function (Page) {
        common.StartLoading();
        var Code = "";
        var Name = "";
        Dic.PageIndex = Page;
        $.ajax({
            url: "/Admin/Dictionary/ItemListGet",
            type: "Get",
            dataType: "html",
            data: { Code: Code, Name: Name, PageIndex: Page },
            success: function (data) {
                common.EndLoading();
                $("#ListDic").html(data);
            }
        });
    },

    PageIndex: 1,
    reportType: "",
    itemType: "1",
    TransferType :"1"

}