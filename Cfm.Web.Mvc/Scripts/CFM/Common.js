var common = {
    StartLoading: function (text) {
        var textContent = "";
        if (text != undefined && text != null && text != '') {
            textContent = text;
        }
        else {
            textContent = "Đang tiến hành xử lý dữ liệu...  ";
        }

        $("#tempModal_ms").modal('show');
        $("#tempContainer_ms").isLoading({
            text: textContent,
            position: "inside"
        });
    },

    EndLoading: function (timer) {
        var Time = 100;
        if (timer != null && timer != undefined && timer != '') {
            Time = timer;
        }
        setTimeout(function () {
            $("#tempContainer_ms").isLoading("hide");
            $("#tempModal_ms").modal('hide');
        }, Time);
    },

    Message: function (title, text, type) {
        $(".modal-backdrop").removeClass("fade").removeClass("modal-backdrop").removeClass("in");

        var colorHeader = "#3498DB";
        if (type == "warning") {
            colorHeader = "#f39c12";
        }
        else if (type == "error") {
            colorHeader = "#e74c3c";
        }

        var html = '<div id="CFMMessage" class="bootbox modal fade in" role="dialog"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important; background-color:' + colorHeader + '">';
        html += '<button type="button" style="margin-right:5px; margin-top:8px;" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4 style="color:#fff; margin-left:10px;">' + title + '</h4>';
        html += '</div><div class="modal-body">' + text + '</div>';
        html += '<div class="modal-footer">';
        //html += '<button type="button" class="btn btn-small btn-primary confirm">Đồng ý</button>';
        html += '<button type="button" class="btn btn-small ';
        if (type == "success") {
            html += 'btn-success';
        }
        else if (type == "error") {
            html += 'btn-danger';
        }
        else if (type == "warning") {
            html += 'btn-warning';
        }

        html += '" data-dismiss="modal">Đóng</button>';
        html += '</div></div>';

        $("#confirmModal").html(html);
        $("#CFMMessage").modal('show');
    },

    Submitform: function (formName, isCheckValid) {
        var checkValid = true;

        if (isCheckValid === true) {
            $("#" + formName).removeAttr("novalidate");
            checkValid = $("#" + formName).valid();
        }

        if (checkValid === true) {
            try {
                $("#PoDetail_Id", fPoDetail).val(PoManage.ParentID);
            }
            catch (e) {
            }

            try {
                $("#ParentID", fPoCreate).val(PoManage.ParentID);
            }
            catch (e) {
            }


            common.StartLoading();
            
            $("#" + formName).submit();
        }
    },

    CallXhttp: function (url, type, dataType, data, isAsync, isLoading, runFuntion) {
        if (isLoading == true) {
            common.StartLoading();
        }

        $.ajax({
            url: common.SubDomain + url,
            type: type,
            dataType: dataType,
            async: isAsync,
            data: data,
            success: function (data) {
                if (isLoading == true) {
                    common.EndLoading();
                }

                if (runFuntion != null) {
                    runFuntion(data);
                }

                return data;
            },
            error: function (xhr, textStatus, errorThrown) {
                if (isLoading == true) {
                    common.EndLoading();
                }
                if (xhr.status != 200) {
                    common.Message("Lỗi" + xhr.status, "Để tiếp tục thao tác, hãy kiểm tra lại đường truyền mạng.", "error");
                    return null;
                }
            }
        })
    },

    GetUrlAPI: function (refType, action) {
        var controllerName = common.GetControlerName(refType);

        return "Admin/Dictionary/" + controllerName + action;
    },

    GetControlerName: function (refType) {
        switch (refType) {
            case "001":
                return "Report";                        //Danh mục báo cáo
            case "002":
                return "ItemType";                      //Loại chỉ tiêu
            case "003":
                return "ItemGroup";                     //Nhóm chỉ tiêu
            case "004":
                return "ItemList";                      //Chỉ tiêu dịch vụ
        }
    },

    SubDomain: "/",

    Init: function () {
        $(".NumberMask").inputmask("decimal", {
            radixPoint: ",",
            groupSeparator: ".",
            digits: 0,
            autoGroup: true,
            rightAlign: false
        });
        $(".NumberMaskUsd").inputmask("decimal", {
            radixPoint: ",",
            groupSeparator: ".",
            digits: 2,
            autoGroup: true,
            rightAlign: false
        });
        //$(".NumberMask").format({ format: "#,###", locale: "us" });
    },

    IsNullOrEmpty: function(value){
        if(value == undefined || value == null || value == "")
        {
            return true;
        }
        else {
            return false;
        }
    },

    ArrItemGroupID: [0],
    SayMoney: function (amount) {
        var lan = 0;
        var i = 0;
        var so = 0;
        var result = "";
        var tmp = "";
        var Tien = new Array("", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ");
        var position = new Array();
        if (amount < 0) return "Số tiền âm !";
        if (amount == 0) return "Không đồng !";
        if (amount > 0) {
            so = amount;
        }
        else {
            so = -amount;
        }
        if (amount > 8999999999999999) {
            //SoTien = 0;
            return "Số quá lớn!";
        }
        position[5] = Math.floor(so / 1000000000000000);
        if (isNaN(position[5]))
            position[5] = "0";
        so = so - parseFloat(position[5].toString()) * 1000000000000000;
        position[4] = Math.floor(so / 1000000000000);
        if (isNaN(position[4]))
            position[4] = "0";
        so = so - parseFloat(position[4].toString()) * 1000000000000;
        position[3] = Math.floor(so / 1000000000);
        if (isNaN(position[3]))
            position[3] = "0";
        so = so - parseFloat(position[3].toString()) * 1000000000;
        position[2] = parseInt(so / 1000000);
        if (isNaN(position[2]))
            position[2] = "0";
        position[1] = parseInt((so % 1000000) / 1000);
        if (isNaN(position[1]))
            position[1] = "0";
        position[0] = parseInt(so % 1000);
        if (isNaN(position[0]))
            position[0] = "0";
        if (position[5] > 0) {
            lan = 5;
        }
        else if (position[4] > 0) {
            lan = 4;
        }
        else if (position[3] > 0) {
            lan = 3;
        }
        else if (position[2] > 0) {
            lan = 2;
        }
        else if (position[1] > 0) {
            lan = 1;
        }
        else {
            lan = 0;
        }
        for (i = lan; i >= 0; i--) {
            tmp = common.SayThreeNumber(position[i]);
            result += tmp;
            if (position[i] > 0) result += Tien[i];
            if ((i > 0) && (tmp.length > 0)) result += ',';//&& (!string.IsNullOrEmpty(tmp))
        }
        if (result.substring(result.length - 1) == ',') {
            result = result.substring(0, result.length - 1);
        }
        result = result.substring(1, 2).toUpperCase() + result.substring(2);
        return result + " đồng";
    },

    SayThreeNumber: function (threeNumber) {
        var hundred;
        var ten;
        var unit;
        var result = "";
        var ChuSo = new Array(" không ", " một ", " hai ", " ba ", " bốn ", " năm ", " sáu ", " bảy ", " tám ", " chín ");

        hundred = parseInt(threeNumber / 100);
        ten = parseInt((threeNumber % 100) / 10);
        unit = threeNumber % 10;
        if (hundred == 0 && ten == 0 && unit == 0) return "";
        if (hundred != 0) {
            result += ChuSo[hundred] + " trăm ";
            if ((ten == 0) && (unit != 0)) result += " linh ";
        }
        if ((ten != 0) && (ten != 1)) {
            result += ChuSo[ten] + " mươi";
            if ((ten == 0) && (unit != 0)) result = result + " linh ";
        }
        if (ten == 1) result += " mười ";
        switch (unit) {
            case 1:
                if ((ten != 0) && (ten != 1)) {
                    result += " mốt ";
                }
                else {
                    result += ChuSo[unit];
                }
                break;
            case 5:
                if (ten == 0) {
                    result += ChuSo[unit];
                }
                else {
                    result += " lăm ";
                }
                break;
            default:
                if (unit != 0) {
                    result += ChuSo[unit];
                }
                break;
        }
        return result;
    },

    PoSelect: {
        ID: 0,
        ParentID: 0,
        Code: "",
        Name: "",
        POLevel: "",
        IsCenter: "",
        Address: "",
        PhoneNumber: "",
        FaxNumber: "",
        IsOffline: "",
        CycleDate: "",
        IsLock: ""
    },
    ViewCurent: "",
    TimeSystem: new Date(),
    ClockSystem: function (control) {
        common.TimeSystem.setSeconds(common.TimeSystem.getSeconds() + 1);
        var h = common.TimeSystem.getHours();
        var m = common.TimeSystem.getMinutes();
        var s = common.TimeSystem.getSeconds();
        var day = common.TimeSystem.getDay();
        var date = common.TimeSystem.getDate();
        var month = common.TimeSystem.getMonth();
        var year = common.TimeSystem.getFullYear();

        var days = new Array("Chủ nhật", "Thứ hai", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7");
        var months = new Array("1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12");
        var am_pm;

        if (s < 10) { s = "0" + s }

        if (m < 10) { m = "0" + m }

        if (h > 12)

        { h -= 12; am_pm = "PM" }

        else { am_pm = "AM" }

        if (h < 10) { h = "0" + h }

        $("#" + control).html(h + ":" + m + ":" + s + " " + am_pm + ", " + days[day] + " Ngày " + date + "/" + months[month] + "/" + year);
    },
    ReportID: 0,
    ReportDate: "",
    Flag: 0
};

var dataTypeAjax = {
    Html: "Html",
    Json: "Json"
};

var typeAjax = {
    Get: "Get",
    Post: "Post"
};

var ActionMode = {
    Create: "Create",
    Get: "Get",
    Delete: "Delete",
    Lock: "Lock",
    Select: "Select",
    Detail: "Detail"
};

var ArrView = [
    "USER"
];

$(function () {
    $(".NumberMask").keyup(function () {
        if (!$(this).hasClass("ItemValue"))
        {
            var value = $(this).val();
            if (value == null || value == "" || value == NaN || value == undefined)
                $(this).val(0);
        }
        
    });

    $(".NumberMaskUsd").keyup(function () {
        if (!$(this).hasClass("ItemValue")) {
            var value = $(this).val();
            if (value == null || value == "" || value == NaN || value == undefined)
                $(this).val(0);
        }

    });

});