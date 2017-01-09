var Accounting = {
    CreateAllocateFund: function (refType) {
        var urlApi = Accounting.GetUrlAPI(refType, ActionMode.Create);
        var dataRequest =
        {
            RefType: refType
        };
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "error");
            }
        });
    },

    AccountingSuccess: function (refType, data) {
        common.EndLoading();
        if (data.Code == "00") {
            common.Message("Thông báo", data.Mes, "success");
            $("#tempModal").modal("hide");         
            Accounting.SearchAccounting(refType, 1);
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    SearchAccounting: function (refType, PageIndex, dateRangeCtr) {
        common.PageIndex = PageIndex;
        var urlApi = Accounting.GetUrlAPI(refType, ActionMode.Get);
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            dateRange: $('#' + dateRangeCtr).val()
        };
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#ListFunds_" + refType).html(data);
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "error");
            }
        });
    },

    DeleteAccounting: function (AccountEntryId, refNumber, refType)
    {
        var html = '<div id="confirmDelEntry" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có muốn xóa bút toán với số hiệu giao dịch [' + refNumber + '] không?</div>';
        html += '<div class="modal-footer">';
        html += '<button type="button" class="btn  btn-primary btn-sm confirmDel">Đồng ý</button>';
        html += '<button type="button" class="btn  btn-danger btn-sm reject" data-dismiss="modal">Hủy bỏ</button>';
        html += '</div></div></div>';

        $("#confirmModal").html(html);
        $("#confirmDelEntry").modal('show');

        $('.confirmDel').click(function () {
            $("#confirmDelEntry").modal('hide');
            var token = $('input[name="__RequestVerificationToken"]', ConfigForm).val();
            var urlApi = Accounting.GetUrlAPI(refType, ActionMode.Delete);
            console.log(urlApi);
            var dataRequest =
           {
               id: AccountEntryId,
               __RequestVerificationToken: token
           };

            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    common.Message("Thông báo", "Xóa bút toán thành công!", "success");
                    Accounting.SearchAccounting(refType, common.PageIndex);
                }
                else
                {
                    common.Message("Thông báo", data.Mes, "warning");
                }
            });
        });
    },

    AccountingDetail: function (Id, refType) {
       
        $(".ExpandRow").each(function () {

            if ($(this).hasClass("ShownInfo") && $(this).attr('id') != "row-" + Id) {
                $(this).removeClass("ShownInfo");
                $(this).toggle(50);
            }
        });

        $(".LinkView").each(function () {

            if ($(this).hasClass("Displayed") && $(this).attr('id') != "txtRef" + Id) {
                $(this).removeClass("Displayed");

            }
        });

        if ($('#txtRef' + Id).hasClass("Displayed"))
        {
            ($('#txtRef' + Id)).removeClass("Displayed");
            $("#row-" + Id).removeClass("ShownInfo");
            $("#row-" + Id).toggle(50);
        }
        else
        {
            var urlApi = Accounting.GetUrlAPI(refType, ActionMode.Detail);
            var dataRequest = { Id: Id, refType: refType };
            common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                if (data != null) {
                    ($('#txtRef' + Id)).addClass("Displayed");
                    $("#col-" + Id).html(data)
                    $("#row-" + Id).addClass("ShownInfo");
                    $("#row-" + Id).toggle(50);
                }
                else {
                    common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "warning");
                }
            });
        }
       
    },

    GetUrlAPI: function (refType, action) {
        var controllerName = "";

        if (action == ActionMode.Delete)
        {
            controllerName = "BorrowFund";
        }
        else
        {
            controllerName = Accounting.GetControlerName(refType);
        }
      
        return "CFMCounter/AccountingEntry/" + controllerName + action;
    },

    GetControlerName: function (refType) {
        switch (refType) {
            case "888":
                return "AllocateFund";                      //Tiếp nộp quỹ   
            case "886":
                return "AllocateFund";
            case "686":
                return "AllocateFund";
            case "868":
                return "AllocateFund";
        }
    },

    
    PageIndex: 1
}

var Accounting_Replace = {
    CreateAllocateFund: function (refType) {
        var urlApi = Accounting_Replace.GetUrlAPI(refType, ActionMode.Create);
        var dataRequest =
        {
            RefType: refType
        };
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "error");
            }
        });
    },

    AccountingSuccess: function (refType, data) {
        common.EndLoading();
        if (data.Code == "00") {
            $("#tempModal").modal("hide");
            Accounting_Replace.SearchAccounting(refType, 1);
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    SearchAccounting: function (refType, PageIndex, dateRangeCtl) {
        common.PageIndex = PageIndex;
        var urlApi = Accounting_Replace.GetUrlAPI(refType, ActionMode.Get);
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            dateRange: $("#" + dateRangeCtl).val()
        };
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#ListFunds_" + refType).html(data);
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "error");
            }
        });
    },

    DeleteAccounting: function (AccountEntryId, refNumber, refType) {
        var html = '<div id="confirmDelEntry" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có muốn xóa bút toán với số hiệu giao dịch [' + refNumber + '] không?</div>';
        html += '<div class="modal-footer">';
        html += '<button type="button" class="btn  btn-primary btn-sm confirmDel">Đồng ý</button>';
        html += '<button type="button" class="btn  btn-danger btn-sm reject" data-dismiss="modal">Hủy bỏ</button>';
        html += '</div></div></div>';

        $("#confirmModal").html(html);
        $("#confirmDelEntry").modal('show');

        $('.confirmDel').click(function () {
            $("#confirmDelEntry").modal('hide');
            var token = $('input[name="__RequestVerificationToken"]', ConfigForm).val();
            var urlApi = Accounting_Replace.GetUrlAPI(refType, ActionMode.Delete);
            console.log(urlApi);
            var dataRequest =
           {
               id: AccountEntryId,
               __RequestVerificationToken: token
           };

            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    common.Message("Thông báo", "Xóa bút toán thành công!", "success");
                    Accounting_Replace.SearchAccounting(refType, common.PageIndex);
                }
                else {
                    common.Message("Thông báo", data.Mes, "warning");
                }
            });
        });
    },

    AccountingDetail: function (Id, refType) {
        $(".ExpandRow").each(function () {

            if ($(this).hasClass("ShownInfo") && $(this).attr('id') != "row-" + Id) {
                $(this).removeClass("ShownInfo");
                $(this).toggle(50);
            }
        });

        $(".LinkView").each(function () {

            if ($(this).hasClass("Displayed") && $(this).attr('id') != "txtRef" + Id) {
                $(this).removeClass("Displayed");

            }
        });
        if ($('#txtRef' + Id).hasClass("Displayed")) {
            ($('#txtRef' + Id)).removeClass("Displayed");
            $("#row-" + Id).removeClass("ShownInfo");
            $("#row-" + Id).toggle(50);
        }
        else {
           
            var urlApi = Accounting_Replace.GetUrlAPI(refType, ActionMode.Detail);
            var dataRequest = { Id: Id, refType: refType };
            common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                if (data != null) {
                    ($('#txtRef' + Id)).addClass("Displayed");
                    $("#col-" + Id).html(data)
                    $("#row-" + Id).addClass("ShownInfo");
                    $("#row-" + Id).toggle(50);
                }
                else {
                    common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "warning");
                }
            });
        }
    },

    GetUrlAPI: function (refType, action) {
        var controllerName = "";

        if (action == ActionMode.Delete) {
            controllerName = "BorrowFundReplace";
        }
        else {
            controllerName = Accounting_Replace.GetControlerName(refType);
        }

        return "CFMDistrict/AccountingEntry/" + controllerName + action;
    },

    GetControlerName: function (refType) {
        switch (refType) {
            case "888":
                return "AllocateFundReplace";                      //Tiếp nộp quỹ   
            case "886":
                return "AllocateFundReplace";
            case "686":
                return "AllocateFundReplace";
            case "868":
                return "AllocateFundReplace";
        }
    },


    PageIndex: 1
}
