var unavailableDates = [];
var unavailableDateObjects;

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

//$(function () {
//    $("#StartDate").datepicker({
//        minDate: 0,
//        maxDate: '+1Y+6M',
//        onSelect: function (danewListr) {
//            var min = $(this).datepicker('getDate'); // Get selected date
//            var startDate = $('#StartDate').datepicker('getDate');
//            if (startDate != null) {
//                startDate.setDate(startDate.getDate() + 1)
//                $("#EndDate").datepicker('option', 'minDate', startDate); // Set other min, default to today
//                totalDays();
//            }
//        }
//    });

//    $("#EndDate").datepicker({
//        minDate: '+1D',
//        maxDate: '+1Y+6M',
//        onSelect: function (danewListr) {
//            var startDate = $('#StartDate').datepicker('getDate');
//            if (startDate != null) {
//                //startDate.setDate(startDate.getDate() + 1 )
//                $('#StartDate').datepicker('option', 'minDate'); // Set other max, default to +18 months
//                totalDays();

//            }
            
//        }
//    });
//});


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
    var newDate;
    var temp;
    if (dates.length == 0) {
        console.log('nothing in list');
    } else {
        // startdate:datetime(13429385204985),enddate:datetime(13094205729) start,end
        //console.log("Dates length" + dates.length);
        for (var i = 0; i < dates.length; i++) {
            // We are turning 
            //console.log(i)
            temp = JSON.stringify(dates[i]).split(',');
            splitStrings.push(temp[0]);
            splitStrings.push(temp[1]);
            //console.log(splitStrings)
        }
        console.log("Split length:" + splitStrings.length)
        for (var j = 0; j < splitStrings.length; j++) {
            newList.push(new Date(parseInt(splitStrings[j].replace(/\D/g, ""))));
        }
        //console.log("new List array " + newList);
        //console.log("before new list")
        console.log(newList)
        //console.log("after new list")
        for (i = 0; i < newList.length; i += 2) {
            startDate = newList[i];
            endDate = newList[i + 1];
            temp = new Date(startDate);
            console.log(temp);
            //for (var d = startDate; d <= endDate; d.setDate(d.getDate() + 1)) {
            //    //console.log(d);
            //    //unavailableDates.push(d);
            //}
            //console.log(unavailableDates)
            while (temp <= endDate) {
                console.log(temp);

                unavailableDates.push((temp.getMonth() + 1) + "/" + temp.getDate() + "/" + temp.getFullYear());
                temp.setDate(temp.getDate() + 1);
            }
            console.log("U " + unavailableDates);
        }
        //console.log("Dates returned from controller: " + unavailableDates);
        //unavailableDateObjects = convertDisabledFieldToDateObject(unavailableDates);
        //console.log("Date Range array " + dateRange);

    }
    unavailableDateObjects = convertDisabledFieldToDateObject(unavailableDates);
    console.log("Unavalable date objects")
    console.log(unavailableDateObjects);
}

function ajaxError() {
    alert("Unable to reach server. Please try again.");
}

function main() {
    transactionHistory();
}

$(document).ready(main());

// To set mindate in enddate
function unavailable(date) {
    dmy = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();
    if ($.inArray(dmy, unavailableDates) == -1) {
        return [true, ""];
    } else {
        return [false, "", "Unavailable"];
    }
}

function customRange(input) {
    return {
        minDate: (input.id == "EndDate" ? $("#StartDate").datepicker("getDate") : new Date())
    };
}

// To set maxdate in startdate
function customRangeStart(input) {
    return {
        maxDate: (input.id == "StartDate" ? $("#EndDate").datepicker("getDate") : null)
    };
}

$(document).ready(function () {

    $('#StartDate').datepicker(
        {
            beforeShow: customRangeStart,
            beforeShowDay: unavailable,
            minDate: 0,
            dateFormat: "mm/dd/yy",
            changeYear: true,
            onSelect: function () {
                $('#EndDate').removeAttr("disabled");
                triggerOnStartSelect();
                var startDate = $('#StartDate').datepicker('getDate');
                if (startDate != null) {
                    startDate.setDate(startDate.getDate() + 1)
                    $("#EndDate").datepicker('option', 'minDate', startDate); // Set other min, default to today
                    totalDays();
                }
            }
        });

    $('#EndDate').datepicker(
        {
            beforeShow: customRange,
            beforeShowDay: unavailable,
            dateFormat: "mm/dd/yy",
            changeYear: true,
            onSelect: function () {
                var startDate = $('#StartDate').datepicker('getDate');
                if (startDate != null) {
                    $('#StartDate').datepicker('option', 'minDate'); // Set other max, default to +18 months
                    totalDays();
                }
            }
        });
});

//Convert String Date List to Date object List
//function convertDisabledFieldToDateObject(diabledList) {
//    console.log("convert start")
//    console.log(diabledList)
//    var dateList = [];
//    $.each(diabledList, function (i, singleDate) {
//        var parsedDate = $.datepicker.parseDate("mm-dd-yy", singleDate);
//        dateList.push(parsedDate);
//    });
//    //Sort date if the diabled date sets are in jumbled order
//    dateList.sort(function (date1, date2) {
//        return date1 - date2;
//    });
//    console.log("convert end")
//    console.log(dateList)
//    return dateList;
//}

function convertDisabledFieldToDateObject(diabledList) {
    var dateList = [];
    $.each(diabledList, function (i, singleDate) {
        var parsedDate = $.datepicker.parseDate("mm/dd/yy", singleDate);
        dateList.push(parsedDate);
    });
    //Sort date if the diabled date sets are in jumbled order
    dateList.sort(function (date1, date2) {
        return date1 - date2;
    });
    return dateList;
}



//Trigger upon change event of either start or end date
function triggerOnStartSelect() {
    var startDate = new Date($("#StartDate").datepicker("getDate"));
    var endDate = new Date($("#EndDate").datepicker("getDate"));
    //if required you could reset all of the default setting here //
    //And can also validate the date objects 

    //Holds to be set maxdate of EndDate datepicker
    var tempEndDate = null;
    $.each(unavailableDateObjects, function (i, disabledRangeDate) {
        if (startDate < disabledRangeDate) {
            tempEndDate = new Date(disabledRangeDate);
            //subtracts one day from the nearest disabled range date 
            tempEndDate.setDate(tempEndDate.getDate() - 1);
            return false;
        }
    });
    $("#EndDate").datepicker("option", "maxDate", tempEndDate);
}