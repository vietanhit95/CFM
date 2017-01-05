var Entry_Process = {
    ViewEntryCreate: function (id, refType) {

        var dataRequest = {
            id: id,
            refType: refType
        };

        var urlApi = Entry_Process.GetUrlAPI(refType, ActionMode.Create);

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer_lg").html(data);
                $("#tempModal_lg").modal("show");
            }
        });
    },

    CreateEntrySuccess: function (data, refType) {
        common.EndLoading();
        if (data.Code == "00") {
            $("#tempModal_lg").modal('hide');
            Entry_Process.LoadEntry(refType, Entry_Process.PageIndex);
        }
        else if (data.Code == "02") {
            var STTLabel = $("#STTLabel").html();
            common.Message("Thông báo", STTLabel + " đã tồn tại!", "warning");
        }
        else {
            common.Message("Thông báo","Mã lỗi: " + data.Code + " - " + data.Mes, "warning");
        }
    },

    LoadEntry: function (refType, pageIndex, dateRangeCtr) {
        common.StartLoading();
        var dataRequest = { refType: refType, pageIndex: pageIndex, dateRange: $('#' + dateRangeCtr).val() };
        var urlApi = Entry_Process.GetUrlAPI(refType, ActionMode.Get);

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#Part" + refType).html(data);
            }
        });
    },

    LoadPaymentEntry: function (refType, pageIndex, dateRangeCtr) {
        common.StartLoading();
        var poId = 0;
        if ($("#PoId").length > 0) {
            var poId = $("#PoId").val();
        }
        var dataRequest = { refType: refType, poId: poId, pageIndex: pageIndex, dateRange: $('#' + dateRangeCtr).val() };
        var urlApi = "CFMCounter/AccountingEntry/BorrowSelectGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#listSelectEntry").html(data);
            }
        });
    },

    SelectEntry: function (refType) {
        if ($("#PoId").length > 0)
        {
            var poId = $("#PoId").val();
            if (poId == undefined || poId == null || poId == '' || poId == '0' || poId == 0)
            {
                common.Message("Thông báo", "Bạn chưa chọn Đơn vị cần nhập thay thế!", "warning");
                return;
            }
        }

        var dataRequest = {
            refType: refType,
            budgetTypeID: $('#cboBudgetType').val(),
            cashFllowId: $('#cboCashFllow').val(),
            poId: $("#PoId").val()
        };
        var urlApi = "CFMCounter/AccountingEntry/BorrowSelect";          

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer_Entry").html(data);
                $("#tempModal_Entry").modal("show");
            }
        });
    },

    GetSelectedEntry: function () {
        var exists = false;
        $('#tblcontent_Entry_Select').find('tr').each(function () {
            var row = $(this);
            if (row.find('input[type="checkbox"]').is(':checked')) {
                var sCCY = row.context.cells[14].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                var sCCYUnit = row.context.cells[15].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                var sCCYSaving = row.context.cells[16].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                var sCCYBS = row.context.cells[17].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');

                var sMethod = row.context.cells[18].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                var sMethodUnit = row.context.cells[19].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                var sMethodSaving = row.context.cells[20].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                var sMethodBS = row.context.cells[21].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');

                //\r?\n
                var sAmount = "0";
                var sAmountUnit = "0";
                var sAmountSaving = "0";
                var sAmountBS = "0";
                if (sCCY == "USD")
                    sAmount = row.context.cells[10].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                else
                    sAmount = row.context.cells[6].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');

                if (sCCYUnit == "USD")
                    sAmountUnit = row.context.cells[11].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                else
                    sAmountUnit = row.context.cells[7].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');

                if (sCCYSaving == "USD")
                    sAmountSaving = row.context.cells[12].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                else
                    sAmountSaving = row.context.cells[8].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                if (sCCYBS == "USD")
                    sAmountBS = row.context.cells[13].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
                else
                    sAmountBS = row.context.cells[9].innerText.replace(/\ /g, '').replace(/\r?\n/g, '');
              
                $('#cfm_txt_amount').val(sAmount);
                $('#cfm_txt_amount_unit').val(sAmountUnit);
                $('#cfm_txt_amount_saving').val(sAmountSaving);
                $('#cfm_txt_amount_bs').val(sAmountBS);

                $('#cfm_cbo_center').val(sCCY);
                $('#cfm_cbo_unit').val(sCCYUnit);
                $('#cfm_cbo_saving').val(sCCYSaving);
                $('#cfm_cbo_bs').val(sCCYBS);

                $('#cfm_cbo_method').val(sMethod);
                $('#cfm_cbo_method_unit').val(sMethodUnit);
                $('#cfm_cbo_method_saving').val(sMethodSaving);
                $('#cfm_cbo_method_bs').val(sMethodBS);

                //var oValue = parseFloat(sAmount.replace(/\,/g, ''));
                //sAmountInWord = common.SayMoney(oValue);
                //$('#txt_Amount_In_Word').val(sAmountInWord);
                $('#txtRefTransNumber').val(row.context.cells[2].innerText.replace(/\ /g, '').replace(/\r?\n/g, ''));
                $('#hRefTransNumber').val(row.context.cells[2].innerText.replace(/\ /g, '').replace(/\r?\n/g, ''));
                $("#tempModal_Entry").modal("hide");
                exists = true;
            }
        }
        );
        if (!exists) {
            common.Message("Thông báo", "Bạn chưa chọn khoản vay.", "warning", true);
        }
    },

    DeleteEntry: function (id, entryName, refNumber, refType) {
        var urlApi = Entry_Process.GetUrlAPI(refType, ActionMode.Delete);
        console.log(urlApi);
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
            var dataRequest = { id: id, __RequestVerificationToken: token};
            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    common.Message("Thông báo", data.Mes, "success", true);
                    Entry_Process.LoadEntry(refType, Entry_Process.PageIndex);
                }
                else {
                    common.Message("Thông báo","Mã lỗi: " + data.Code + " - " +  data.Mes, "error", true);
                }
            });
        });
    },

    EntryDetail: function (Id, refType) {
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
           
            var urlApi = Entry_Process.GetUrlAPI(refType, ActionMode.Detail);
            var dataRequest = { Id: Id, refType: refType };
            common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                if (data != null) {
                    ($('#txtRef' + Id)).addClass("Displayed");
                    $("#row-" + Id).addClass("ShownInfo");
                    $("#col-" + Id).html(data)
                    $("#row-" + Id).css({ "display": "" });
                    
                }
                else {
                    common.Message("Thông báo", "Mã lỗi: 90 - Có lỗi trong quá trình xử lý dữ liệu!", "warning");
                }
            });
        }
    },

    GetUrlAPI: function (refType, action) {
        var controllerName = "";
        if (action == ActionMode.Delete) {
            controllerName = "BorrowFund";
        }
        else if (action == ActionMode.Detail) {
            controllerName = "AllocateFund";
        }
        else {
            controllerName = Entry_Process.GetControlerName(refType);
        }       

        return "CFMCounter/AccountingEntry/" + controllerName + action;
    },

    GetControlerName: function (refType) {
        switch (refType) {
            case "866":
                return "BorrowFund";  //Vay quỹ khác     
            case "668":
                return "BorrowFund";  //Quỹ khác vay
            case "666":
                return "PaymentBorrowFund";
            case "688":
                return "PaymentBorrowFund";

        }
    },

    MonthlyFundInfoInDistrict: function(PoId){
        dataRequest = {
            PoId: PoId
        };
        common.CallXhttp("CFMCounter/AccountingEntry/MonthlyFundInfo", typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#tempModal_PO").modal("hide"); //Chọn PO ở Huyện nên Hide
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
            else {
                common.Message("Thông báo", "Mã lỗi: 90 - Có lỗi trong quá trình xử lý dữ liệu!", "warning");
            }
        });
    },

    MonthlyFundInfo: function (id) {
        var dataRequest = {};
        if (id == 0) {
            //dataRequest = {
            //    PoId: common.PoSelect.ID
            //};
            common.CallXhttp("CFMCounter/AccountingEntry/MonthlyFundInfo", typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                if (data != null) {
                    $("#tempContainer").html(data);
                    $("#tempModal").modal("show");
                }
                else {
                    common.Message("Thông báo", "Mã lỗi: 90 - Có lỗi trong quá trình xử lý dữ liệu!", "warning");
                }
            });
        }
        else {
            common.CallXhttp("CFMCounter/AccountingEntry/FundInfo", typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                if (data != null) {
                    $("#tempContainer").html(data);
                    $("#tempModal").modal("show");
                }
                else {
                    common.Message("Thông báo", "Mã lỗi: 90 - Có lỗi trong quá trình xử lý dữ liệu!", "warning");
                }
            });
        }
    },

    GetMonthlyFund: function()
    {
        var token = $('input[name="__RequestVerificationToken"]', fMonthlyFundInfo).val();
        var Month = $('#cboMonth').val();
        var CashFllowId = $('#cboCashFllowId').val();
        var PoId = $("#PoId").val();
        var dataRequest = {
            Month: Month,
            CashFllowId: CashFllowId,
            PoId:PoId,
            __RequestVerificationToken: token
        };

        common.CallXhttp("CFMCounter/AccountingEntry/MonthlyFundSearch", typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
            if (data.data != null) {
                var oMonthlyFundInfo = data.data;
                $("#StringAmountVND").val(oMonthlyFundInfo.AmountVND);
                $("#StringAmountUSD").val(oMonthlyFundInfo.AmountUSD);
                $("#StringAmountBankVND").val(oMonthlyFundInfo.AmountBankVND);
                $("#StringAmountBankUSD").val(oMonthlyFundInfo.AmountBankUSD);
                $("#StringAmountBorrowFund").val(oMonthlyFundInfo.AmountBorrowFund);
                $("#StringAmountLoanFund").val(oMonthlyFundInfo.AmountLoanFund);
                $("#StringAmountTransferVND").val(oMonthlyFundInfo.AmountTransferVND);
                $("#StringAmountTransferUSD").val(oMonthlyFundInfo.AmountTransferUSD);
                $("#MonthlyFundInfoId").val(oMonthlyFundInfo.Id);
            }
            else {
                $("#StringAmountVND").val("0");
                $("#StringAmountUSD").val("0");
                $("#StringAmountBankVND").val("0");
                $("#StringAmountBankUSD").val("0");
                $("#StringAmountBorrowFund").val("0");
                $("#StringAmountLoanFund").val("0");
                $("#StringAmountTransferVND").val("0");
                $("#StringAmountTransferUSD").val("0");
                $("#MonthlyFundInfoId").val("0");
            }
        });
    },

    OnsuccessMonthlyFund: function(data)
    {
        common.EndLoading();
        if(data.Code == "00")
        {
            $("#MonthlyFundInfoId").val(data.Value);
            common.Message("Thông báo", data.Mes, "success");
        }
        else {
            common.Message("Thông báo","Mã lỗi: " + data.Code + " - " + data.Mes, "warning");
        }
    },

    FundInfo: function () {
        var dataRequest = {};
        common.CallXhttp("CFMCounter/AccountingEntry/FundInfo", typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
            else {
                common.Message("Thông báo", "Mã lỗi: 90 - Có lỗi trong quá trình xử lý dữ liệu!", "warning");
            }
        });
    },

    GetFundInfo: function () {
        $("#cboMonth").change(function () {
            var days = new Date($("#cboYear").val(), $("#cboMonth").val(), 0).getDate();
            var ddlDay = $("#cboDay");
            ddlDay.empty();
            for (var i = 1 ; i <= days; i++) {
                ddlDay.append($('<option>', { value: i.toString(), text: i.toString() }, '</option>'));
            }
        });       

        var token = $('input[name="__RequestVerificationToken"]', fFundInfo).val();
        var dataRequest = {
            Day: $("#cboDay").val(),
            Month: $("#cboMonth").val(),
            Year: $("#cboYear").val(),
            Fund_Type: $("#cboCashFlow").val(),
            __RequestVerificationToken: token
        };

        common.CallXhttp("CFMCounter/AccountingEntry/FundInfoSearch", typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
            if (data.data != null) {
                var oFundInfo = data.data;
                $("#StringAmountVND").val(oFundInfo.AmountVND);
                $("#StringAmountUSD").val(oFundInfo.AmountUSD);
                $("#StringAmountBankVND").val(oFundInfo.AmountBankVND);
                $("#StringAmountBankUSD").val(oFundInfo.AmountBankUSD);
                $("#StringAmountBorrowVND").val(oFundInfo.AmountBorrowVND);
                $("#StringAmountLoanVND").val(oFundInfo.AmountLoanVND);
                $("#StringAmountTransferVND").val(oFundInfo.AmountTransferVND);
                $("#StringAmountTransferUSD").val(oFundInfo.AmountTransferUSD);
                $("#FundInfoId").val(oFundInfo.Id);
            }
            else {
                $("#StringAmountVND").val("0");
                $("#StringAmountUSD").val("0");
                $("#StringAmountBankVND").val("0");
                $("#StringAmountBankUSD").val("0");
                $("#StringAmountBorrowVND").val("0");
                $("#StringAmountLoanVND").val("0");
                $("#StringAmountTransferVND").val("0");
                $("#StringAmountTransferUSD").val("0");
                $("#FundInfoId").val("0");
            }
        });
    },

    OnsuccessFundInfo: function (data) {
        common.EndLoading();
        if (data.Code == "00") {
            $("#FundInfoId").val(data.Value);
            common.Message("Thông báo", data.Mes, "success");
        }
        else {
            common.Message("Thông báo","Mã lỗi : " + data.Code + " - " +  data.Mes, "warning");
        }
    },

    SetDay: function()
    {
        var days = new Date($("#cboYear").val(), $("#cboMonth").val(), 0).getDate();
        var ddlDay = $("#cboDay");
        ddlDay.empty();
        for(var i =1 ; i<=days; i++)
        {
            ddlDay.append($('<option>', {value:i.toString(),text:i.toString()},'</option>'));
        }
    },

    ViewAdjEntryCreate: function (reportid, PoId) {

        var dataRequest = {        
            reportid: reportid,
            reportDate: $('#ReportDate').val(),
            PoId: PoId
        };

        var urlApi = "CFMCounter/AccountingEntry/AdjustmentEntryCreate";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer_lg").html(data);
                $("#tempModal_lg").modal("show");
            }
        });
    },
    CreateAdjEntrySuccess: function (data,poId,reportDate,reportId) {
        common.EndLoading();
        if (data.Code == "00") {
           
            $('input[name="AdjEntryId"]').val(data.Value);
            
            //B2: Update vào Chỉ tiêu trên lưới
            var itemCode = $('input[name="ItemCode"]').val();
            var adjType = parseInt($('#cboAdjType').val());
            var adjAmountVnd =0;
            if (adjType == 1 || adjType == 3)
                adjAmountVnd = parseFloat($('#txtStringAmountVnd').val().replace(/\./g, '').replace(/\,/g, '.'));
            else
                adjAmountVnd = parseFloat($('#txtStringAmountVnd').val().replace(/\./g, '').replace(/\,/g, '.')) * (-1);
            if (adjAmountVnd != 0)
                Entry_Process.UpdateItem(itemCode, adjAmountVnd, "VND");

            var adjAmountUsd = 0;
            if (adjType == 1 || adjType == 3)
                adjAmountUsd = parseFloat($('#txtStringAmountUsd').val().replace(/\./g, '').replace(/\,/g, '.'));
            else
                adjAmountUsd = parseFloat($('#txtStringAmountUsd').val().replace(/\,/g, '').replace(/\,/g, '.')) * (-1);
            if (adjAmountUsd != 0)
                Entry_Process.UpdateItem(itemCode, adjAmountUsd, "USD");

            $("#tempModal_lg").modal('hide');

            //B3: Submit báo cáo 04-CĐ
            common.Submitform('fUpdateReportCD');

            Entry_Process.LoadAdjEntry(poId, reportDate, reportId);
        }
        else {
            common.Message("Thông báo","Mã lỗi: " + data.Code + " - " +  data.Mes, "warning");
        }
    },
    UpdateItem: function(itemCode,itemValue,ccy)
    {
        var amount = parseFloat($('.' + itemCode + ccy).val().replace(/\./g, '').replace(/\,/g, '.'));
        $('.' + itemCode + ccy).val(amount + itemValue);

        $('.' + itemCode + ccy).trigger('')

        var e = $.Event('keyup');
       
        $('.' + itemCode + ccy).trigger(e);
    },
    LoadAdjEntry: function (poId, reportDate, reportId) {
        common.StartLoading();
        var dataRequest = { poId: poId, reportDate: reportDate, reportId: reportId };
        var urlApi = "CFMCounter/AccountingEntry/AdjustmentEntryGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#listAdjEntry").html(data);
            }
        });
    },
    SubmitAdjEntry: function () {
        var exists = false;
        $('#tblListItem').find('tr').each(function () {
            var row = $(this);
            if (row.find('input[type="checkbox"]').is(':checked')) {
                var sItemName = row.context.cells[3].innerText;                
                $('#txtItemName').val(sItemName);             
                $('input[name="ItemId"]').val(row.context.cells[1].innerText);
                $('input[name="ItemCode"]').val(row.context.cells[2].innerText);
                exists = true;
                //B1: Ghi bút toán điều chỉnh
                common.Submitform('fAdjustmentEntryCreate', true);
               
            }
        }
        );
        if (!exists) {
            common.Message("Thông báo", "Mã lỗi: 90 - Bạn chưa chọn chỉ tiêu.", "warning", true);
        }
    },
    DeleteAdjEntry: function (id, adjTypeName, itemName, poId, reportDate, reportId,itemCode,amountVnd,amountUsd,adjTypeId) {
        var urlApi = "CFMCounter/AccountingEntry/AdjustmentEntryDelete";
       
        var html = '<div id="confirmDelEntry" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có muốn xóa [' + adjTypeName + '] của chỉ tiêu [' + itemName + '] không?</div>';
        html += '<div class="modal-footer">';
        html += '<button type="button" class="btn  btn-primary btn-sm confirmDel">Đồng ý</button>';
        html += '<button type="button" class="btn  btn-danger btn-sm reject" data-dismiss="modal">Hủy bỏ</button>';
        html += '</div></div></div>';

        $("#confirmModal").html(html);
        $("#confirmDelEntry").modal('show');

        $('.confirmDel').click(function () {
            $("#confirmDelEntry").modal('hide');
            // var token = $('input[name="__RequestVerificationToken"]', ConfigForm).val();
            var dataRequest = { id: id, reportDate: reportDate };
            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    var adjAmountVnd = 0;
                    if (adjTypeId == 1 || adjTypeId == 3)
                        adjAmountVnd = parseFloat(amountVnd.replace(/\./g, '').replace(/\,/g, '.')) * (-1);
                    else
                        adjAmountVnd = parseFloat(amountVnd.replace(/\./g, '').replace(/\,/g, '.'));
                    if (adjAmountVnd != 0)
                        Entry_Process.UpdateItem(itemCode, adjAmountVnd, "VND");

                    var adjAmountUsd = 0;
                    if (adjTypeId == 1 || adjTypeId == 3)
                        adjAmountUsd = parseFloat(amountUsd.replace(/\./g, '').replace(/\,/g, '.')) * (-1);
                    else
                        adjAmountUsd = parseFloat(amountUsd.replace(/\./g, '').replace(/\,/g, '.'));
                    if (adjAmountUsd != 0)
                        Entry_Process.UpdateItem(itemCode, adjAmountUsd, "USD");

                    common.Submitform('fUpdateReportCD');

                    Entry_Process.LoadAdjEntry(poId, reportDate, reportId);
                }
                else {
                    common.Message("Thông báo","Mã lỗi: " + data.Code + " - " + data.Mes, "error", true);
                }
            });
        });
    },
    SubDomain: "/",

    PageIndex: 1
}

var EntryReplace_Process = {
    ViewEntryCreate: function (id, refType) {

        var dataRequest = {
            id: id,
            refType: refType
        };

        var urlApi = EntryReplace_Process.GetUrlAPI(refType, ActionMode.Create);

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer_lg").html(data);
                $("#tempModal_lg").modal("show");
            }
        });
    },

    CreateEntrySuccess: function (data, refType) {
        common.EndLoading();
        if (data.Code == "00") {
            $("#tempModal_lg").modal('hide');
            EntryReplace_Process.LoadEntry(refType, EntryReplace_Process.PageIndex);
        }
        else if(data.Code == "02")
        {
            var STTLabel = $("#STTLabel").innerText();
            common.Message("Thông báo", STTLabel + " đã tồn tại!", "warning");
        }
        else {
            common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "warning");
        }
    },

    LoadEntry: function (refType, pageIndex, dateRangeCtr) {
        common.StartLoading();
        var dataRequest = { refType: refType, pageIndex: pageIndex, dateRange: $('#' + dateRangeCtr).val() };
        var urlApi = EntryReplace_Process.GetUrlAPI(refType, ActionMode.Get);

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#Part" + refType).html(data);
            }
        });
    },

    LoadPaymentEntry: function (refType, pageIndex, dateRangeCtr) {
        common.StartLoading();
        var dataRequest = { refType: refType, pageIndex: pageIndex, dateRange: $('#' + dateRangeCtr).val() };
        var urlApi = "CFMCounter/AccountingEntry/BorrowSelectGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#listSelectEntry").html(data);
            }
        });
    },

    SelectEntry: function (refType) {
        var dataRequest = {
            refType: refType,
            budgetTypeID: $('#cboBudgetType').val(),
            cashFllowId: $('#cboCashFllow').val()
        };
        var urlApi = "CFMCounter/AccountingEntry/BorrowSelect";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer_Entry").html(data);
                $("#tempModal_Entry").modal("show");
            }
        });
    },

    GetSelectedEntry: function () {
        var exists = false;
        $('#tblcontent_Entry_Select').find('tr').each(function () {
            var row = $(this);
            if (row.find('input[type="checkbox"]').is(':checked')) {
                var sAmount = row.context.cells[4].innerText;
                var sAmountInWord = "";
                $('#cfm_txt_amount').val(sAmount);
                var oValue = parseFloat(sAmount.replace(/\,/g, ''));
                sAmountInWord = common.SayMoney(oValue);
                $('#txt_Amount_In_Word').val(sAmountInWord);
                $('#txtRefTransNumber').val(row.context.cells[2].innerText);
                $('#hRefTransNumber').val(row.context.cells[2].innerText);
                $("#tempModal_Entry").modal("hide");
                exists = true;
            }
        }
        );
        if (!exists) {
            common.Message("Thông báo", "Mã lỗi: 90 - Bạn chưa chọn khoản vay.", "warning", true);
        }
    },

    DeleteEntry: function (id, entryName, refNumber, refType) {
        var urlApi = EntryReplace_Process.GetUrlAPI(refType, ActionMode.Delete);
        console.log(urlApi);
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
            var dataRequest = { id: id, __RequestVerificationToken: token };
            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    common.Message("Thông báo", data.Mes, "success", true);
                    EntryReplace_Process.LoadEntry(refType, EntryReplace_Process.PageIndex);
                }
                else {
                    common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "error", true);
                }
            });
        });
    },

    EntryDetail: function (Id, refType) {
        //if ($('#txtRef' + Id).hasClass("Displayed")) {
        //    ($('#txtRef' + Id)).removeClass("Displayed");
        //    $("#row-" + Id).toggle(50);
        //}
        //else {
        //    var urlApi = EntryReplace_Process.GetUrlAPI(refType, ActionMode.Detail);
        //    var dataRequest = { Id: Id, refType: refType };
        //    common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
        //        if (data != null) {
        //            ($('#txtRef' + Id)).addClass("Displayed");
        //            $("#col-" + Id).html(data)
        //            $("#row-" + Id).toggle(50);
        //        }
        //        else {
        //            common.Message("Thông báo", "Mã lỗi: 90 - Có lỗi trong quá trình xử lý dữ liệu!", "warning");
        //        }
        //    });
        //}
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

            var urlApi = EntryReplace_Process.GetUrlAPI(refType, ActionMode.Detail);
            var dataRequest = { Id: Id, refType: refType };
            common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                if (data != null) {
                    ($('#txtRef' + Id)).addClass("Displayed");
                    $("#row-" + Id).addClass("ShownInfo");
                    $("#col-" + Id).html(data)
                    $("#row-" + Id).css({ "display": "" });

                }
                else {
                    common.Message("Thông báo", "Mã lỗi: 90 - Có lỗi trong quá trình xử lý dữ liệu!", "warning");
                }
            });
        }
    },

    GetUrlAPI: function (refType, action) {
        var controllerName = "";
        if (action == ActionMode.Delete) {
            controllerName = "BorrowFundReplace";
        }
        else if (action == ActionMode.Detail) {
            controllerName = "AllocateFundReplace";
        }
        else {
            controllerName = EntryReplace_Process.GetControlerName(refType);
        }

        return "CFMDistrict/AccountingEntry/" + controllerName + action;
    },

    GetControlerName: function (refType) {
        switch (refType) {
            case "866":
                return "BorrowFundReplace";  //Vay quỹ khác     
            case "668":
                return "BorrowFundReplace";  //Quỹ khác vay
            case "666":
                return "PaymentBorrowFundReplace";
            case "688":
                return "PaymentBorrowFundReplace";

        }
    },

    SubDomain: "/",

    PageIndex: 1
};

var FundRequired_Process = {
    ViewEntryCreate: function (id) {

        var dataRequest = {
            id: id
        };

        var urlApi = "CFMCounter/AccountingEntry/FundRequiredCreate";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
        });
    },
    ViewFundRequiredApprove: function (id,poId) {

        var dataRequest = {
            id: id,
            poId: poId
        };

        var urlApi = "CFMCounter/AccountingEntry/FundRequiredApprovedCreate";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
        });
    },
    LoadFundRequired: function (refType, PageIndex) {
        common.StartLoading();
        var dataRequest = { dateRange: $('#txtDateRangeOne').val(), PageIndex: PageIndex };
        var urlApi = "CFMCounter/AccountingEntry/FundRequiredGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#FundRequired").html(data);
            }
        });
    },
    LoadApprovedFundRequired: function (refType, PageIndex) {
        common.StartLoading();
        var dataRequest = { dateRange: $('#txtDateRangeTwo').val(), PageIndex: PageIndex };
        var urlApi = "CFMCounter/AccountingEntry/FundRequiredApprovedGet";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#FundRequiredApproved").html(data);
            }
        });
    },
    CreateFundRequiredSuccess: function (data) {
        common.EndLoading();
        if (data.Code == "00") {
            $("#tempModal").modal('hide');
            FundRequired_Process.LoadFundRequired(0, 1);
        }
        else {
            common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "warning");
        }
    },
    ApprovedFundRequiredSuccess: function (data) {
        common.EndLoading();
        if (data.Code == "00") {
            $("#tempModal").modal('hide');
            FundRequired_Process.LoadApprovedFundRequired(0, 1);
        }
        else {
            common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "warning");
        }
    },
    DeleteFundRequired: function (id, createdDate) {
        var urlApi = "CFMCounter/AccountingEntry/FundRequiredDelete";
        console.log(urlApi);
        var html = '<div id="confirmDelEntry" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có muốn xóa Nhu cầu tiếp quỹ ngày [' + createdDate + '] không?</div>';
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
                    FundRequired_Process.LoadFundRequired(0, 1);
                }
                else {
                    common.Message("Thông báo", "Mã lỗi: " + data.Code + " - " + data.Mes, "error", true);
                }
            });
        });
    },
};

var FundLimits_Process = {
    ViewFundLimits: function (PoId,Nam) {

        var dataRequest = {
            PoId: PoId,
            Nam: Nam
        };

        var urlApi = "CFMDistrict/AccountingEntry/FundLimitsDetail";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {
                common.Message("Thông báo!", "Đăng nhập lại trước khi tiếp tục thao tác!", "warning");
            }
            else {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
        });
    },

    ListFundLimits: function () {
        var dataRequest = {
            Nam: $("#FundLimitsNam").val()
        };

        var urlApi = "CFMDistrict/AccountingEntry/FundLimitsGet";
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {
                common.Message("Thông báo!", "Đăng nhập lại trước khi tiếp tục thao tác!", "warning");
            }
            else {
                $("#ListFundLimits").html(data);
            }
        });
    },

    OnSuccess: function (data) {
        common.EndLoading();
        if(data.Code == "00")
        {
            $("#tempModal").modal("hide");
            common.Message("Thông báo", data.Mes, "success");
            FundLimits_Process.ListFundLimits();
        }
        else
        {
            common.Message("Thông báo", data.Mes, "warning");
        }
    }
};