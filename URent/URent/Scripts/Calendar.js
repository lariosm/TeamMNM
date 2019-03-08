$(function () {
    $('#dob').datepicker(
        {
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            minDate: "-100Y",
            maxDate: "-14Y",
            yearRange: "-100:-14"
        });
});

$(function () {
    $("#StartDate").datepicker({
        minDate: 0,
        maxDate: '+1Y+6M',
        onSelect: function (dateStr) {
            var min = $(this).datepicker('getDate'); // Get selected date
            $("#EndDate").datepicker('option', 'minDate', min || '0'); // Set other min, default to today
        }
    });

    $("#EndDate").datepicker({
        minDate: '0',
        maxDate: '+1Y+6M',
        onSelect: function (dateStr) {
            var max = $(this).datepicker('getDate'); // Get selected date
            $('#datepicker').datepicker('option', 'maxDate', max || '+1Y+6M'); // Set other max, default to +18 months
            var start = $("#StartDate").datepicker("getDate");
            var end = $("#EndDate").datepicker("getDate");
            var days = (end - start) / (1000 * 60 * 60 * 24);
            days = days.toFixed(0);
            $("#TextBox3").val(days);
            console.log(days);
            calculate();
        }
    });
});