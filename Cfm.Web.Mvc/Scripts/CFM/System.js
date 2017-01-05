var ReportConfigItemPO = {
    AddReportConfig: function () {
        var table = $("#tblReportConfig");
        var rows = $(table).find(".rowReportConfig:not(.hidden)");
        var html = "";
        $.each(rows, function (i, v) {

            if ($(this).find("input").is(":checked")) {
                var checkbox = $(this).find("td:eq(0) input[type='checkbox']:checked");
                var Id = $(checkbox).data("id");
                var Code = $(checkbox).data("code");
                var Name = $(checkbox).data("name");
                var ReportName = $(checkbox).data("reportname");
                var Exists = $(checkbox).data("exists");
                var ReportPoId = $(checkbox).data("reportpoid");

                var oItem = {
                    Id: Id,
                    Code: Code,
                    Name: Name,
                    ReportName: ReportName,
                    Exists: Exists,
                    ReportPoId: ReportPoId
                };

                ReportConfigItemPO.AddId.push(oItem);
                //$("#rowReportConfig-" + Id).addClass("hidden");
                $("#rowReportConfig-" + Id).remove();

                html += '<tr class="rowReportConfigPO ' + Exists + '" id="rowReportConfigPO-' + Id + '">';
                html += '<td class="mid"><input type="checkbox" data-exists="' + Exists + '" data-id="' + Id + '" data-reportpoid="' + ReportPoId + '" data-code="' + Code + '" data-name="' + Name + '" data-reportname="' + ReportName + '" class="checkOnePo" /></td>';
                html += '<td>' + Code + '</td>';
                html += '<td>' + Name + '</td>';
                html += '<td>' + ReportName + '</td>';
                html += '</tr>';

            }
        });
        ReportConfigItemPO.CompareReportConfig(false);

        $("#bodyReportConfigPO tr:first-child").before(html);

        $.each(ReportConfigItemPO.AddId, function (i, v) {
            $("#rowReportConfigPO-" + v.Id + ":not(.Y)").addClass("add");
        });
        //$(".none").remove();
    },

    DelReportConfig: function () {
        var table = $("#bodyReportConfigPO");
        var rows = $(table).find(".rowReportConfigPO:not(.hidden)");
        var html = "";
        $.each(rows, function (i, v) {
            if ($(this).find("input").is(":checked")) {
                var checkbox = $(this).find("td:eq(0) input[type='checkbox']:checked");
                var Id = $(checkbox).data("id");
                var Code = $(checkbox).data("code");
                var Name = $(checkbox).data("name");
                var ReportName = $(checkbox).data("reportname");
                var Exists = $(checkbox).data("exists");
                var ReportPoId = $(checkbox).data("reportpoid");

                var oItem = {
                    Id: Id,
                    Code: Code,
                    Name: Name,
                    ReportName: ReportName,
                    Exists: Exists,
                    ReportPoId: ReportPoId
                };

                ReportConfigItemPO.DeleteId.push(oItem);

                $("#rowReportConfigPO-" + Id).remove();

                html += '<tr class="rowReportConfig ' + Exists + '" id="rowReportConfig-' + Id + '">';
                html += '<td class="mid"><input type="checkbox" data-exists="' + Exists + '"  data-id="' + Id + '" data-code="' + Code + '" data-reportpoid="' + ReportPoId + '" data-name="' + Name + '" data-reportname="' + ReportName + '" class="checkOne" /></td>';
                html += '<td>' + Code + '</td>';
                html += '<td>' + Name + '</td>';
                html += '<td>' + ReportName + '</td>';
                html += '</tr>';
            }
        });
        ReportConfigItemPO.CompareReportConfig(true);

        $("#bodyReportConfig tr:first-child").before(html);


        $.each(ReportConfigItemPO.DeleteId, function (i, v) {
            $("#rowReportConfig-" + v.Id + ":not(.N)").addClass("delete").re;
        });
        //$(".none").remove();
    },

    CompareReportConfig: function (Del) {
        var arrAddId = [];
        var arrDelId = [];

        $.each(ReportConfigItemPO.AddId, function (i, v) {
            arrAddId.push(v);
        });

        $.each(ReportConfigItemPO.DeleteId, function (i, v) {
            arrDelId.push(v);
        });

        $.each(arrAddId, function (i, v) {
            $.each(arrDelId, function (k, t) {
                var AddId = v.Id;
                var DelId = t.Id;

                if (AddId == DelId) {
                    if (Del == true) {
                        ReportConfigItemPO.AddId.pop(t);
                    }
                    else {
                        ReportConfigItemPO.DeleteId.pop(t);
                    }
                }
            });
        });
    },

    LoadConfigReport: function (Ref, PageIndex) {
        var dataRequest = {
            PageIndex: PageIndex
        };

        var urlApi = "CFMCounter/System/ListReportConfig"
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#tblReportConfig").html(data);
                ReportConfigItemPO.UpdateElementReportConfig();
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "error");
            }
        });
    },

    LoadConfigReportPO: function (Ref, PageIndex) {
        var dataRequest = {
            PageIndex: PageIndex
        };

        var urlApi = "CFMCounter/System/ListReportConfigPo"
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#tblReportConfigPo").html(data);
                ReportConfigItemPO.UpdateElementReportConfigPO();
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "error");
            }
        });
    },

    UpdateElementReportConfigPO: function () {
        var html = "";
        $.each(ReportConfigItemPO.AddId, function (i, v) {
            if (v.Exists == "N") {
                html += '<tr class="add rowReportConfigPO ' + v.Exists + '" id="rowReportConfigPO-' + v.Id + '">';
                html += '<td class="mid"><input type="checkbox" data-exists="' + v.Exists + '"  data-id="' + v.Id + '" data-code="' + v.Code + '" data-reportpoid="' + v.ReportPoId + '" data-name="' + v.Name + '" data-reportname="' + v.ReportName + '" class="checkOnePo" /></td>';
                html += '<td>' + v.Code + '</td>';
                html += '<td>' + v.Name + '</td>';
                html += '<td>' + v.ReportName + '</td>';
                html += '</tr>';
            }
        });

        $.each(ReportConfigItemPO.DeleteId, function (i, v) {
            $("#rowReportConfigPO-" + v.Id).remove();
        });

        $("#bodyReportConfigPO tr:first-child").before(html);
    },

    UpdateElementReportConfig: function () {
        html = "";
        $.each(ReportConfigItemPO.DeleteId, function (i, v) {
            if (v.Exists == "Y") {
                html += '<tr class="delete rowReportConfig ' + v.Exists + '" id="rowReportConfig-' + v.Id + '">';
                html += '<td class="mid"><input type="checkbox" data-exists="' + v.Exists + '"  data-id="' + v.Id + '" data-code="' + v.Code + '" data-reportpoid="' + v.ReportPoId + '" data-name="' + v.Name + '" data-reportname="' + v.ReportName + '" class="checkOne" /></td>';
                html += '<td>' + v.Code + '</td>';
                html += '<td>' + v.Name + '</td>';
                html += '<td>' + v.ReportName + '</td>';
                html += '</tr>';
            }
        });

        $.each(ReportConfigItemPO.AddId, function (i, v) {
            $("#rowReportConfig-" + v.Id).remove();
        });

        $("#bodyReportConfig tr:first-child").before(html);
    },

    SubmitReportConfig: function () {
        var postAdd = { Report: ReportConfigItemPO.AddId };
        var postDel = { Report: ReportConfigItemPO.DeleteId };
        $("#AddString").val(JSON.stringify(postAdd));
        $("#DelString").val(JSON.stringify(postDel));
        common.StartLoading();
        $("#fReportConfigUpdate").submit();
    },

    Onsuccess: function (data) {
        common.EndLoading();
        if (data.Code == "00") {
            common.Message("Thông báo", "Cập nhập dịch vụ thành công!", "success");
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
        ReportConfigItemPO.AddId = [];
        ReportConfigItemPO.DeleteId = [];
        ReportConfigItemPO.LoadConfigReportPO('', 1);
        ReportConfigItemPO.LoadConfigReport('', 1);
    },

    AddId: [],
    DeleteId: []
};

var PoManage = {
    OnLoad: function (Id) {

        $(".parent_li a").removeClass("treeActiveA");
        $(".parent_li span").removeClass("treeActiveSpan");
        $("#po_" + Id).addClass("treeActiveA");
        $("#node-" + Id).addClass("treeActiveSpan");

        PoManage.PoDetail(Id);
        if (!$("#po_child_" + Id).hasClass("loaded")) {
            PoManage.ListTreePoChild(Id);
        }
        //else {
        //    $("#po_child_" + Id).toggle(100);
        //}

        if (common.ViewCurent == ArrView[0])
        {
            UserManage.GetUser();
        }
        else {
            PoManage.ListPoChild(Id);
        }
    },

    PoDetail: function (_Id) {
        var Info = $("#po_" + _Id);
        var Id = Info.data("id");
        var Code = Info.data("code");
        var Name = Info.data("name");
        var Address = Info.data("address");
        var PhoneNumber = Info.data("phonenumber");
        var FaxNumber = Info.data("faxnumber");
        try
        {
            $("#Code", fPoDetail).val(Code);
            $("#PoDetail_Code", fPoDetail).val(Code);
            $("#PoDetail_Id", fPoDetail).val(Id);
            $("#Name", fPoDetail).val(Name);
            $("#PhoneNumber", fPoDetail).val(PhoneNumber);
            $("#FaxNumber", fPoDetail).val(FaxNumber);
            $("#Address", fPoDetail).val(Address);
        }
        catch(e){}
       
        $("#po-Name").html(Code + "-" + Name);
        PoManage.ParentID = Id;
    },

    ListPoChild: function (ParentId) {
        var dataRequest = {
            ParentId: ParentId
        };

        var urlApi = "CFMCounter/System/ListPoChild"
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#ListPoChild").html(data);
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "error");
            }
        });
    },

    ListTreePoChild: function (ParentId) {
        var dataRequest = {
            ParentId: ParentId
        };

        var urlApi = "CFMCounter/System/PoChild"
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                $("#po_child_" + ParentId).html(data);
                $("#po_child_" + ParentId).addClass("loaded");
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "error");
            }
        });
    },

    CollapseTree: function (ParentId) {
        if ($("#po_child_" + ParentId).hasClass("loaded")) {
            $("#po_child_" + ParentId).toggle(100);
        }
    },

    AddPoView: function (Id, ParentId) {
        var dataRequest = {
            Id: Id,
            ParentId: ParentId
        };
        var urlApi = "CFMCounter/System/PoAdd";
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            if (data != null) {
                common.EndLoading();
                $("#tempContainer").html(data);
                $("#tempModal").modal('show');
            }
            else {
                common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "error");
            }
        });
    },

    Onsuccess: function (data) {
        common.EndLoading();
        if (data.Code == "00") {
            $("#tempModal").modal('hide');
            common.Message("Thông báo", data.Mes, "success");
            PoManage.ListTreePoChild(PoManage.ParentID);
            PoManage.ListPoChild(PoManage.ParentID);
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    OnsuccessDetail: function (data) {
        common.EndLoading();
        if (data.Code == "00") {
            common.Message("Thông báo", data.Mes, "success");
            var ID = data.Value;
            var Code = $("#Code", fPoDetail).val();
            var Name = $("#Name", fPoDetail).val();
            var PhoneNumber = $("#PhoneNumber", fPoDetail).val();
            var FaxNumber = $("#FaxNumber", fPoDetail).val();
            var Address = $("#Address", fPoDetail).val();

            $("#po_" + ID).attr('data-id', ID);
            $("#po_" + ID).attr('data-code', Code);
            $("#po_" + ID).attr('data-name', Name);
            $("#po_" + ID).attr('data-phonenumber', PhoneNumber);
            $("#po_" + ID).attr('data-faxnumber', FaxNumber);
            $("#po_" + ID).attr('data-address', Address);


            $("#po_" + ID).html(Name);
            $("#po-Name").html(Code + "-" + Name);

        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    DeletePo: function (Id, Name) {
        var html = '<div id="confirmDelPo" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có muốn xóa đơn vị [' + Name + '] không?</div>';
        html += '<div class="modal-footer">';
        html += '<button type="button" class="btn  btn-primary btn-sm confirmDel">Đồng ý</button>';
        html += '<button type="button" class="btn  btn-danger btn-sm reject" data-dismiss="modal">Hủy bỏ</button>';
        html += '</div></div></div>';

        $("#confirmModal").html(html);
        $("#confirmDelPo").modal('show');

        $('.confirmDel').click(function () {
            $("#confirmDelPo").modal('hide');
            var token = $('input[name="__RequestVerificationToken"]', ConfirmListPo).val();
            var urlApi = "CFMCounter/System/DeletePo";
            var dataRequest =
           {
               Id: Id,
               __RequestVerificationToken: token
           };

            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    PoManage.ListTreePoChild(PoManage.ParentID);
                    PoManage.ListPoChild(PoManage.ParentID);
                }
                else {
                    common.Message("Thông báo", data.Mes, "warning");
                }
            });
        });
    },

    ChangeStatusPo: function (Id) {
        var urlApi = "CFMCounter/System/PoChangeStatus";
        var token = $('input[name="__RequestVerificationToken"]', ConfirmListPo).val();
        var dataRequest =
       {
           Id: Id,
           __RequestVerificationToken: token
       };

        common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
            if (data.Code == "00") {
                PoManage.ListTreePoChild(PoManage.ParentID);
                PoManage.ListPoChild(PoManage.ParentID);
            }
            else {
                common.Message("Thông báo", data.Mes, "warning");
            }
        });
    },

    ParentID: 0,
    PageIndex: 1
};

var UserManage = {
    UserCreateView: function (UserId, UserName, PoId) {
        if (PoId != PoManage.ParentID)
        {
            common.Message("Thông báo", "Có lỗi trong quá trình xử lý dữ liệu!", "warning");
        }
        else
        {
            var urlApi = "CFMCounter/System/CreateUser";
            var dataRequest =
            {
                UserId: UserId,
                UserName: UserName,
                PoId: PoManage.ParentID
            };
            common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
                common.EndLoading();
                if (data != null) {
                    $("#tempContainer").html(data);
                    $("#tempModal").modal('show');
                }
                else {
                    common.Message("Thông báo", "Đăng nhập lại để tiếp tục thao tác!", "warning");
                }
            });
        }
    },

    GetUser: function () {
        var urlApi = "CFMCounter/System/ListUser";
        var dataRequest =
        {
            PoId: PoManage.ParentID
        };
        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            common.EndLoading();
            if (data != null) {
                $("#ListUser").html(data);
            }
            else {
                common.Message("Thông báo", "Đăng nhập lại để tiếp tục thao tác!", "warning");
            }
        });
    },

    Onsuccess: function (data) {
        common.EndLoading();
        if(data.Code == "00")
        {
            $("#tempModal").modal('hide');
            common.Message("Thông báo", data.Mes, "success");
            UserManage.GetUser();
        }
        else {
            common.Message("Thông báo", data.Mes, "warning");
        }
    },

    DeleteUser: function (Id, UserName, FullName) {
        var html = '<div id="confirmDelUser" class="bootbox modal fade in"><div class="modal-dialog modal-sm" role="document"><div class="modal-content">';
        html += '<div class="modal-header" style="padding:3px !important">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        html += '<h4>Thông báo</h4></div>';
        html += '<div class="modal-body">Bạn có muốn xóa Người dùng [' + FullName + '] không?</div>';
        html += '<div class="modal-footer">';
        html += '<button type="button" class="btn  btn-primary btn-sm confirmDel">Đồng ý</button>';
        html += '<button type="button" class="btn  btn-danger btn-sm reject" data-dismiss="modal">Hủy bỏ</button>';
        html += '</div></div></div>';

        $("#confirmModal").html(html);
        $("#confirmDelUser").modal('show');

        $('.confirmDel').click(function () {
            $("#confirmDelUser").modal('hide');
            var token = $('input[name="__RequestVerificationToken"]', ConfirmListUser).val();
            var urlApi = "CFMCounter/System/DeleteUser";

            var dataRequest =
            {
                Id: Id,
                UserName: UserName,
                __RequestVerificationToken: token
            };

            common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
                if (data.Code == "00") {
                    UserManage.GetUser();
                }
                else {
                    common.Message("Thông báo", data.Mes, "warning");
                }
            });
        });
    },

    ChangeStatusUser: function (Id, UserName) {
        var urlApi = "CFMCounter/System/UserChangeStatus";
        var token = $('input[name="__RequestVerificationToken"]', ConfirmListUser).val();
        var dataRequest =
        {
            Id: Id,
            UserName: UserName,
            __RequestVerificationToken: token
        };

        common.CallXhttp(urlApi, typeAjax.Post, dataTypeAjax.Json, dataRequest, true, true, function (data) {
            if (data.Code == "00") {
                UserManage.GetUser();
            }
            else {
                common.Message("Thông báo", data.Mes, "warning");
            }
        });
    },

    ChangePass: function () {
        var dataRequest = {

        };

        var urlApi = "Admin/User/ChangePassword";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            common.EndLoading();
            if (data != null) {
                $("#tempContainer_sm").html(data);
                $("#tempModal_sm").modal('show');
            }
            else {
                common.Message("Thông báo", "Bạn không có quyền sử dụng chức năng này!", "warning");
            }
        });
    },

    OnsucessChangePass: function (data) {
        common.EndLoading();
        if(data.Code == "00")
        {
            common.Message("Thông báo", data.Mes, "success");
            $("#tempModal_sm").modal('hide');
        }
        else
        {
            common.Message("Thông báo", data.Mes, "error");
        }
    },

    OnSuccessRole: function(data)
    {
        common.EndLoading();
        if(data.Code == "00")
        {
            common.Message("Thông báo", data.Mes, "success");
        }
        else {
            common.Message("Thông báo", data.Mes, "success");
        }
    },

    ViewRole: function () {
        var dataRequest = {
            EmpGroupId: $("#ListGroupUser").val()
        };
        var urlApi = "Admin/Home/TreeFunction";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            common.EndLoading();
            if (data != null) {
                $("#RoleBox").html(data);
            }
            else {
                common.Message("Thông báo", "Bạn không có quyền sử dụng chức năng này!", "warning");
            }
        });
    }
}

var System = {
    LoadViewTimeWork: function () {
        var dataRequest = {

        };
        var urlApi = "Admin/Home/SetTimeWork";

        common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            common.EndLoading();
            if (data != null) {
                $("#tempContainer_sm").html(data);
                $("#tempModal_sm").modal('show');
            }
            else {
                common.Message("Thông báo", "Bạn không có quyền sử dụng chức năng này!", "warning");
            }
        });
    },

    SetToday: function () {
        var date = common.TimeSystem.getDate();
        var month = common.TimeSystem.getMonth();
        var year = common.TimeSystem.getFullYear();
        var dateWork = new Date(year, month, date, 0, 0, 0, 0);
        $('#time-work').datepicker({
            format: 'dd-mm-yyyy', language: "vi",
            endDate: dateWork
        });
        $("#time-work").datepicker("setDate", dateWork);
    },

    SetBeforeDay: function () {
        var date = common.TimeSystem.getDate();
        var month = common.TimeSystem.getMonth();
        var year = common.TimeSystem.getFullYear();
        var dateWork = new Date(year, month, date, 0, 0, 0, 0);
        dateWork.setDate(dateWork.getDate() - 1);
        $('#time-work').datepicker({
            format: 'dd-mm-yyyy', language: "vi",
            endDate: dateWork
        });
        $("#time-work").datepicker("setDate", dateWork);
    },

    SetTimeWork: function () {
        var date = $("#time-work").datepicker('getDate');
        var dateFormat = $.datepicker.formatDate('dd/mm/yy', date);
        $("#TimeWork").val(dateFormat);
        $("#fSetTimeWork").submit();
    },

    Onsuccess: function (data) {
        if(data.Code == "00")
        {
            var url = "/";
            window.location.href = url;
            //$("#tempModal_sm").modal('hide');
            //var dataRequest = {

            //};
            //var urlApi = "Admin/Home/TimeWork";

            //common.CallXhttp(urlApi, typeAjax.Get, dataTypeAjax.Html, dataRequest, true, true, function (data) {
            //    common.EndLoading();
            //    if (data != null) {
            //        $("#TimeWorkSidebar").html(data);
            //    }
            //    else {
            //        common.Message("Thông báo", "Bạn không có quyền sử dụng chức năng này!", "warning");
            //    }
            //});
        }
        else
        {
            common.Message("Thông báo", data.Mes, "warning");
        }
    }
}