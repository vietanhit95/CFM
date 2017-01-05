var DashBoard = {
    //CFMCounter
    RefReshNotifiCationInfo: function (refType, PageIndex, dateRangeCtr) {
        common.PageIndex = PageIndex;
        var urlApi = "CFMCounter/Home/InfoNotifiGet";
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            dateRange: $('#' + dateRangeCtr).val()
        };
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#ListInfoNotifi_" + refType).html(data);
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "error");
            }
        });
    },

    SearchNotifiDashBoard_CFMCounter: function (refType, PageIndex, dateRangeCtr, orderby) {
        common.PageIndex = PageIndex;
        var urlApi = "CFMCounter/Home/DashBoardNotifiGet";
        var status = $("#Status").val();
        var type = $("#Type").val();
        var date = $("#txtDateRangeOne").val();
        $("#sl_Status_FunNotifi").hide();
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            status,
            type,
            date,
            orderby
        };
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#ListFunNotifi_" + refType).html(data);
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu", "error");
                $("#sl_Status").hide(100);
            }
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
    SearchNotifiDashBoard_CFMCounter_Fun: function (refType, PageIndex, dateRangeCtr, orderby) {
        common.PageIndex = PageIndex;
        var urlApi = "CFMCounter/Home/FunnotifiGet";
        var status = $("#Status_Fun").val();
        var date = $("#txtDateRangeFive").val();
        $("#sl_Status_FunNotifi").hide();
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            status,
            date,
            orderby
            };
    common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
        if (data != null) {
            $("#ListFunNotifiCation_" + refType).html(data);
        }
        else {
            common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu", "error");
            $("#sl_Status").hide(100);
        }
    });
    },
    ViewCreateNotiFi_CFMCounter: function (id) {
        var dataRequest = { id: id };
        var urlApi = "CFMCounter/Home/DashboardNotifiDetails";
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
        });
    },
    //End-CFMCounter

    //CFMBranch
    RefReshNotifiCationInFo_CFMBranch: function (refType, PageIndex, dateRangeCtr) {
        common.PageIndex = PageIndex;
        var Url = "CFMBranch/Home/InfoNotifiGet";
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            dateRange: $('#' + dateRangeCtr).val()
        };
        common.CallXhttp(Url, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#ListInfo_CFMBranch_" + refType).html(data);
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu", "error");
            }
        });
    },
    SeachNotifiDashBoard_CFMBranch: function (refType, PageIndex, dateRangeCtr, orderby) {
        common.PageIndex = PageIndex;
        var Url = "CFMBranch/Home/DashBoardNotifiGet";
        var status = $("#Status").val();
        var type = $("#Type").val();
        var date = $("#txtDateRangeOne").val();
        $("#sl_Status").hide();
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            status,
            type,
            date,
            orderby
            };
        common.CallXhttp(Url, typeAjax.Get, dataTypeAjax.html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#ListNotifi_CFMBranch_" + refType).html(data);
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu", "error");
            }
        });
    },
    ViewCreate_CFMBranch: function (id, ref_id, status, refType) {

        if (ref_id == 1) {
            var dataRequest = { id: id, ref_id: ref_id, status: status, refType: refType };
            var urlApi = "CFMBranch/Home/DashboardNotifiDetails";
            common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                if (data == null) {

                }
                else {
                    $("#tempContainer").html(data);
                    $("#tempModal").modal("show");
                }
            });
        }
        else if (ref_id == 2) {
            var dataRequest = { id: id, ref_id: ref_id, status: status, refType: refType };
            var urlApi = "CFMBranch/Home/DashBoard_AllocateFundDetail";
            common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                if (data == null) {

                }
                else {
                    $("#tempContainer").html(data);
                    $("#tempModal").modal("show");
                }
            });
        }
        function rsData() {
            table_ds.innerHtml = '';
            $("#table_ds").load("Home/DashBoardNotifiGet #table_dash");
        }
        if (status == 'N') {
            var Url = "CFMBranch/Home/IsReaded";
            var dataRequest = {
                id: id,
                status: status
            };

            common.CallXhttp(Url, typeAjax.Get, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                console.log(data);
                if (data == null) {

                }
                else {
                    if (data.Code == "00") {
                        rsData();
                    }
                }
            });
        }

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
    SearchNotifiDashBoard_CFMBranch_Fun: function (refType, PageIndex) {
        common.PageIndex = PageIndex;
        var urlApi = "CFMBranch/Home/FunnotifiGet";
        var status = $("#Status_Fun").val();
        var date = $("#txtDateRangeFive").val();
        $("#sl_Status_FunNotifi").hide();
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            status,
            date
            };
    common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
        if (data != null) {
            $("#ListFunNotifiCation_Branch_" + refType).html(data);
        }
        else {
            common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu", "error");
        }
    });
    },

    //End-CFMBranch
    //CFMDistrict
    RefReshNotifiCationInFo_CFMDistrict: function (refType, PageIndex, dateRangeCtr) {
        common.PageIndex = PageIndex;
        var Url = "CFMDistrict/Home/InfoNotifiGet";
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            dateRange: $('#' + dateRangeCtr).val()
        };
        common.CallXhttp(Url, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#ListInfo_CFMBranch_" + refType).html(data);
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu", "error");
            }
        });
    },
    SeachNotifiDashBoard_CFMDistrict: function (refType, PageIndex, dateRangeCtr, orderby) {
        common.PageIndex = PageIndex;
        var Url = "CFMDistrict/Home/DashBoardNotifiGet";
        var status = $("#Status").val();
        var type = $("#Type").val();
        var date = $("#txtDateRangeOne").val();
        $("#sl_Status").hide();
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            status,
            type,
            date,
            orderby
            };
        common.CallXhttp(Url, typeAjax.Get, dataTypeAjax.html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#ListNotifi_CFMBranch_" + refType).html(data);
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu", "error");
            }
        });
    },
    ViewCreate_CFMDistrict: function (id) {
        var Url = "CFMDistrict/Home/DashboardNotifiDetails";
        var dataRequest = {
            id: id
        };
        common.CallXhttp(Url, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data == null) {

            }
            else {
                $("#tempContainer").html(data);
                $("#tempModal").modal("show");
            }
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
    SearchNotifiDashBoard_CFMDistrict_Fun: function (refType, PageIndex, dateRangeCtr, orderby) {
        common.PageIndex = PageIndex;
        var urlApi = "CFMDistrict/Home/FunnotifiGet";
        var status = $("#Status_Fun").val();
        var date = $("#txtDateRangeFive").val();
        $("#sl_Status_FunNotifi").hide();
        var dataRequest = {
            refType: refType,
            PageIndex: PageIndex,
            status,
            date,
            orderby
            };
    common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
        if (data != null) {
            $("#ListFunNotifiCation_" + refType).html(data);
        }
        else {
            common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu", "error");
            $("#sl_Status").hide(100);
        }
    });
    },
    //End-CFMDistrict
}