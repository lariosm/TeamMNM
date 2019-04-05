﻿$(function () {
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
            $("#EndDate").datepicker('option', 'minDate', min || '+1D'); // Set other min, default to today
            totalDays();
        }
    });

    $("#EndDate").datepicker({
        minDate: '+1D',
        maxDate: '+1Y+6M',
        onSelect: function (dateStr) {
            var startDate = $('#StartDate').datepicker('getDate');
            if (startDate != null) {
                startDate.setDate(startDate.getDay() + 1)
                $('#StartDate').datepicker('option', 'minDate', startDate); // Set other max, default to +18 months
                totalDays();

            }
            
        }
    });
});


$(document).ready(function () {
    $('#StartRequest').datepicker(
        {
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            minDate: "0D",
            maxDate: "3Y"
        });

    $('#EndRequest').datepicker(
        {
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            minDate: "0D",
            maxDate: "3Y"
        });
});