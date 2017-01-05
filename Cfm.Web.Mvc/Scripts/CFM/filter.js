var filterConfig = {
    dateRange: { name: 'DateRange' },
    dateRangeOne: { name: 'DateRangeOne' },
    dateRangeTwo: { name: "DateRangeTwo" },
    dateRangeThree: { name: "DateRangeThree" },
    dateRangeFour: { name: "DateRangeFour" },
    dateRangeFive: { name: "DateRangeFive" },
};

var filter = {
    initDateRange: function (control, startDate, endDate, positionOpen) {
        $('#txt' + control.name).daterangepicker(
            {
                ranges: {
                    'Hôm nay':[moment(),moment()],
                    '7 ngày trước': [moment().subtract('days', 6), moment()],
                    '30 ngày trước': [moment().subtract('days', 29), moment()],
                    'Quí 1': ['1/1/' + moment().year(), '3/31/' + moment().year()],
                    'Quí 2': ['4/1/' + moment().year(), '6/30/' + moment().year()],
                    'Quí 3': ['7/1/' + moment().year(), '9/30/' + moment().year()],
                    'Quí 4': ['10/1/' + moment().year(), '12/31/' + moment().year()]
                },
                format: 'DD/MM/YYYY',
                startDate: (startDate == null || startDate == undefined || startDate == '') ?  moment() : startDate,
                endDate: (endDate == null || endDate == undefined || endDate == '') ? moment() : endDate,
                showDropdowns: true,
                opens: (positionOpen == null || positionOpen == undefined || positionOpen == '') ? 'left' : positionOpen,
                locale: {
                    applyLabel: 'Đồng ý',
                    fromLabel: 'Từ ngày',
                    toLabel: 'Đến ngày',
                    customRangeLabel: 'Tùy chọn',
                    weekLabel: 'Tuần',
                    daysOfWeek: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
                    monthNames: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
                    firstDay: 1
                }
            }
            );
        $('#txt' + control.name).attr('readonly', 'readonly');
    }
}