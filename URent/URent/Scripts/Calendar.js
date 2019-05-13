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
        onSelect: function (danewListr) {
            var min = $(this).datepicker('getDate'); // Get selected date
            var startDate = $('#StartDate').datepicker('getDate');
            if (startDate != null) {
                startDate.setDate(startDate.getDate() + 1)
                $("#EndDate").datepicker('option', 'minDate', startDate); // Set other min, default to today
                totalDays();
            }
        }
    });

    $("#EndDate").datepicker({
        minDate: '+1D',
        maxDate: '+1Y+6M',
        onSelect: function (danewListr) {
            var startDate = $('#StartDate').datepicker('getDate');
            if (startDate != null) {
                //startDate.setDate(startDate.getDate() + 1 )
                $('#StartDate').datepicker('option', 'minDate'); // Set other max, default to +18 months
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

//$(function () {
//    $(".datepicker").datepicker({
//        inline: true,
//        altField: "#myDatePicker",
//        dateFormat: 'yy-mm-dd'
//    })
//        .datepicker("setDate", "0");
//});

//$("#dateHidden").on('input propertychange paste', function () {

//    var Loc = $("#Cafe").val();
//    var PDate = $("#datePicker").val();
////------Functions you call or actions you perform--------//
////GetLoc(Loc,PDate);

//});
var dateRange = [];
$('#z').datepicker({
    minDate: '0',
    dateFormat: 'mm/dd/yy',
    inline: true,
    altField: '#d',
    beforeShowDay: function (date) {
        //console.log(date)
        var danewListring = jQuery.datepicker.formatDate('mm/dd/yy', date);
        return [dateRange.indexOf(danewListring) == -1];
    }
});
$('#d').change(function () {
    $('#z').datepicker('option', $(this).val());
});


function transactionHistory() {
    var input = $('#itemId').data('id');
    //var input = document.getElementById("itemId").value; //retrieves listing ID from HTML ID attribute
    var id = parseInt(input); //parses the input as an int
    console.log(id);
    var source = "/SUPTransactions/ExcludeTransactionDates/" + id;
    console.log(source);

    $.ajax({
        type: "GET",
        dataType: "json",
        url: source,
        success: showPreviousTransactions,
        error: ajaxError
    })
}

function showPreviousTransactions(dates) {
    // Date is returned as a list which contains the header of the db 
    // column and the value stored within that column in a single item.
    // The start and end dates are stored in the same elements within the list. 
    //console.log(dates)
    var startDate, endDate;
    var splitStrings = [];
    var newList = [];
    var temp = []
    if (dates.length == 0) {
        console.log('nothing in list');
    } else {
        // startdate:datetime(13429385204985),enddate:datetime(13094205729) start,end
        for (var i = 0; i < dates.length; i++) {
            // We are turning 
            console.log(i)
            temp = JSON.stringify(dates[i]).split(',');
            splitStrings.push(temp[0]);
            splitStrings.push(temp[1]);
            console.log(splitStrings)
        }
        //console.log(splitStrings.length)
        for (var j = 0; j < splitStrings.length; j++) {
            newList.push(new Date(parseInt(splitStrings[j].replace(/\D/g, ""))));
            
        }
        //console.log("new List array " + newList);
        //console.log(newList)
        for (i = 0; i < newList.length; i += 2) {
            startDate = newList[i];
            endDate = newList[i + 1];
            //dateRange.forEach(function (j) { j = 0; });
            //console.log(dateRange);
            console.log("Start Date: " + newList[i] + " End Date: " + newList[i + 1]);
            //console.log("start date " + newList[i] + " end date " + endDate)
            for (var d = startDate; d <= endDate; d.setDate(d.getDate() + 1)) {
                //console.log(d);
                dateRange.push($.datepicker.formatDate('mm/dd/yy', d));
                //console.log(d);
            }
            
        }
        console.log("Date Range array " + dateRange);
    }    
}

function ajaxError() {
    alert("Unable to reach server. Please try again.");
}

function main() {
    transactionHistory();
}

$(document).ready(main());