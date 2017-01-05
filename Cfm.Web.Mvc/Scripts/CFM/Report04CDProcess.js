$(function () {
    function contains(a, obj) {
        for (var i = 0; i < a.length; i++) {
            if (a[i] === obj) {
                return true;
            }
        }
        return false;
    }

    $(".ItemValue").keyup(function () {
        //Tinh chi tieu trong cung group

        var parent = $(this).data('parentid');
        var value = $(this).val();
        if (value == null || value == "" || value == NaN || value == undefined)
            $(this).val(0);
        var control = $('#' + parent).length;
        var dataItemGroup = $(this).data('itemgroupid');
        if (!$("#" + parent).hasClass("HasFormula"))
            value = Cal(parent);
        while (control > 0) {
            if (!$("#" + parent).hasClass("HasFormula"))
                $("#" + parent).val(value.toLocaleString('de-DE'));

            parent = $("#" + parent).data('parentid');
            control = $('#' + parent).length;
            if (!$("#" + parent).hasClass("HasFormula"))
                value = Cal(parent);
        }

        var ccy = $(this).data("currencytype");
        var itemcode = $(this).data("itemcode");

        $(".HasFormula").each(function () {
            if ($(this).data("currencytype") == ccy) {
                var Formula = $(this).data("itemformula");
                var arrItem = [""];
                var arrSign = ["", "+"];
                var itemString = "";
                var index = 0;
                for (i = 0; i < Formula.length; i++) {
                    if (Formula[i] != " " && Formula[i] != "=") {
                        if (Formula[i] != "+" && Formula[i] != "-")
                            itemString += Formula[i]
                        else {
                            arrItem.push(itemString);
                            arrSign.push(Formula[i]);

                            itemString = "";
                        }
                    }
                }

                if (itemString != "")
                    arrItem.push(itemString);

                var value = 0;
                $.each(arrItem, function (i, v) {
                    if (v != "") {

                        var val = $("." + v + ccy).val();
                        if (val != null && val != undefined && val != "") {
                            value += (arrSign[i] == "+") ? parseFloat(val.replace(/\./g, '').replace(/\,/g, '.')) : (-1) * parseFloat(val.replace(/\./g, '').replace(/\,/g, '.'));
                        }
                    }
                });
                $(this).val(value.toLocaleString('de-DE'));
                var parent = $(this).data('parentid');
                var value = $(this).val();
                var dataItemGroup = $(this).data('itemgroupid');
                var control = $('#' + parent).length;
                if (!$("#" + parent).hasClass("HasFormula", "IsSummary" + ccy))
                    value = Cal(parent);
                while (control > 0) {
                    if (!$("#" + parent).hasClass("HasFormula", "IsSummary" + ccy))
                        $("#" + parent).val(value.toLocaleString('de-DE'));

                    parent = $("#" + parent).data('parentid');
                    control = $('#' + parent).length;
                    if (!$("#" + parent).hasClass("HasFormula", "IsSummary" + ccy))
                        value = Cal(parent);
                }

                var ItemGroup = $(this).data('itemgroupid');
                calFooter(ItemGroup);
            }
        });

        if (!$(this).hasClass("IsSummary" + ccy)) {
            calSummary(ccy);


        }
        calFooter(dataItemGroup);
    });

    function Cal(control) {
        var value = 0;
        $("." + control).each(function () {

            value += parseFloat($(this).val().replace(/\./g, '').replace(/\,/g, '.'));
        });
        return value;
    }

    function calFooter(dataItemGroupid) {
        var totalCashReceiptVnd = 0;
        $(".0" + dataItemGroupid).each(function () {
            if (!$(this).hasClass("NotAllowSummaryBottom")) {
                totalCashReceiptVnd += parseFloat($(this).val().replace(/\./g, '').replace(/\,/g, '.'));
            }
        });
        $("#total" + dataItemGroupid).val(totalCashReceiptVnd.toLocaleString('de-DE'));
    }

    function calSummary(ccy) {
        //Tinh chi tieu tong hop
        var sumValue = 0;
        var itemCode = "";

        $(".IsSummary" + ccy).each(function () {
            itemCode = $(this).data("itemcode");
            sumValue = 0;
            var bExists = false;
            $("." + itemCode + ccy).each(function () {
                if (!$(this).hasClass("IsSummary" + ccy)) {
                    sumValue += parseFloat($(this).val().replace(/\./g, '').replace(/\,/g, '.'));
                    bExists = true;
                }
            });
            if (bExists)
                $(this).val(sumValue.toLocaleString('de-DE'));

            var parent = $(this).data('parentid');
            var value = $(this).val();
            var dataItemGroup = $(this).data('itemgroupid');
            var control = $('#' + parent).length;
            if (!$("#" + parent).hasClass("HasFormula", "IsSummary" + ccy))
                value = Cal(parent);
            while (control > 0) {
                if (!$("#" + parent).hasClass("HasFormula", "IsSummary" + ccy))
                    $("#" + parent).val(value.toLocaleString('de-DE'));

                parent = $("#" + parent).data('parentid');
                control = $('#' + parent).length;
                if (!$("#" + parent).hasClass("HasFormula", "IsSummary" + ccy))
                    value = Cal(parent);
            }

            calFooter(dataItemGroup);

        });


        $(".HasFormula" + ".IsSummary" + ccy).each(function () {
            if ($(this).data("currencytype") == ccy) {
                var Formula = $(this).data("itemformula");
                var arrItem = [""];
                var arrSign = ["", "+"];
                var itemString = "";
                var index = 0;
                for (i = 0; i < Formula.length; i++) {
                    if (Formula[i] != " " && Formula[i] != "=") {
                        if (Formula[i] != "+" && Formula[i] != "-")
                            itemString += Formula[i]
                        else {
                            arrItem.push(itemString);
                            arrSign.push(Formula[i]);

                            itemString = "";
                        }
                    }
                }

                if (itemString != "")
                    arrItem.push(itemString);

                var value = 0;
                $.each(arrItem, function (i, v) {
                    if (v != "") {

                        var val = $("." + v + ccy).val();
                        if (val != null && val != undefined && val != "") {
                            value += (arrSign[i] == "+") ? parseFloat(val.replace(/\./g, '').replace(/\,/g, '.')) : (-1) * parseFloat(val.replace(/\./g, '').replace(/\,/g, '.'));
                        }
                    }
                });
                $(this).val(value.toLocaleString('de-DE'));

                var parent = $(this).data('parentid');
                var value = $(this).val();
                var dataItemGroup = $(this).data('itemgroupid');
                var control = $('#' + parent).length;
                if (!$("#" + parent).hasClass("HasFormula", "IsSummary" + ccy))
                    value = Cal(parent);
                while (control > 0) {
                    if (!$("#" + parent).hasClass("HasFormula", "IsSummary" + ccy))
                        $("#" + parent).val(value.toLocaleString('de-DE'));

                    parent = $("#" + parent).data('parentid');
                    control = $('#' + parent).length;
                    if (!$("#" + parent).hasClass("HasFormula", "IsSummary" + ccy))
                        value = Cal(parent);
                }

                var ItemGroup = $(this).data('itemgroupid');
                calFooter(ItemGroup);
            }
        });

    }

});