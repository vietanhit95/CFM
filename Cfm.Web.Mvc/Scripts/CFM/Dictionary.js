var Dic = {
    ViewCreateItemGroup: function (id) {
        common.StartLoading();
        $.ajax({
            url: "/Admin/Dictionary/CreateItemGroup",
            type: "Get",
            dataType: "html",
            data: { Id: id },
            success: function (data) {
                common.EndLoading();
                $("#tempContainer").html(data);
                $("#tempModal").modal('show');
            }
        })
    },

    ItemGroupSuccess: function (data) {
        common.EndLoading();
        if (data.Code == "00") {
            $("#tempModal").modal('hide');
            Dic.ListItemGroup(Dic.PageIndex);
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    ListItemGroup: function (Page) {
        common.StartLoading();
        var Code = "";
        var Name = "";
        Dic.PageIndex = Page;
        $.ajax({
            url: "/Admin/Dictionary/ItemGroupGet",
            type: "Get",
            dataType: "html",
            data: { Code: Code, Name: Name, PageIndex: Page },
            success: function (data) {
                common.EndLoading();
                $("#ListDic").html(data);
            }
        });
    },

    GetItemForConfig: function (RefType,Page) {
        common.StartLoading();
        var Code = "";
        var Name = "";
        Dic.PageIndex = Page;
        $.ajax({
            url: "/Admin/Dictionary/ItemListGet",
            type: "Get",
            dataType: "html",
            data: { Code: Code, Name: Name, PageIndex: Page, forConfig:1,searchContent:$('#txtSearchContent').val()},
            success: function (data) {
                common.EndLoading();
                $("#ListDic").html(data);
            }
        });
    },

    GetItemForTransferEntry: function (RefType, Page) {
        common.StartLoading();
        var Code = "";
        var Name = "";
        Dic.PageIndex = Page;
        $.ajax({
            url: "/Admin/Dictionary/ItemListGetForTransferEntry",
            type: "Get",
            dataType: "html",
            data: { reportType: Dic.reportType, searchContent: $('#txtSearchContent').val() },
            success: function (data) {
                common.EndLoading();
                $("#ListDic").html(data);
            }
        });
    },

    SearchItemList: function (RefType, Page) {
        common.StartLoading();
        var Code = "";
        var Name = "";
        Dic.PageIndex = Page;
        $.ajax({
            url: "/Admin/Dictionary/ItemListGet",
            type: "Get",
            dataType: "html",
            data: { Code: Code, Name: Name, PageIndex: Page, forConfig: 0, searchContent: $('#txtSearchContent').val() },
            success: function (data) {
                common.EndLoading();
                $("#ListDic").html(data);
            }
        });
    },

    GetItemForCreateAdjEntry: function (reportId, Page) {
        common.StartLoading();
        var Code = "";
        var Name = "";
        Dic.PageIndex = Page;
        $.ajax({
            url: "/CFMCounter/AccountingEntry/ItemListGet",
            type: "Get",
            dataType: "html",
            data: { reportId: reportId, searchContent: $('#txtSearchContent').val(),reportId:reportId,forConfig:2, PageIndex: Page },
            success: function (data) {
                common.EndLoading();
                $("#ListDic").html(data);
            }
        });
    },

    PageIndex: 1,
    reportType:""
}

//----------------------Base Dictionary-------------------------//

var Dictionary_Process = {
    ViewCreate: function (id, refType) {
        var dataRequest = { id: id };
        var urlApi = common.GetUrlAPI(refType, ActionMode.Create);

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
        var urlApi = common.GetUrlAPI(refType, ActionMode.Get);
        Dictionary_Process.PageIndex = PageIndex;
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

    LockDic: function (DicID, refType) {
        var token = $('input[name="__RequestVerificationToken"]', DicForm).val();
        var dataRequest = { DicID: DicID, __RequestVerificationToken: token };
        var urlApi = common.GetUrlAPI(refType, ActionMode.Lock);
        common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
            if (data.Code == "00") {
                common.Message("Thông báo", data.Mes, "success", true);
                Dictionary_Process.LoadDic(refType, Dictionary_Process.PageIndex);
            }
            else {
                common.Message("Thông báo", data.Mes, "error", true);
            }
        });
    },

    DeleteDic: function (id, dicName, dicTitle, refType) {
        var urlApi = common.GetUrlAPI(refType, ActionMode.Delete);
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
                    Dictionary_Process.LoadDic(refType, Dictionary_Process.PageIndex);
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

            Dictionary_Process.LoadDic(refType, Dictionary_Process.PageIndex);
            common.Message("Thông báo", data.Mes, "success");

            $("#tempModal").modal("hide");
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    PageIndex: 1
}