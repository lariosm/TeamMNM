function notificationRequest() {
    var source = "/Resources/NotificationRequest/"

    $.ajax({
        type: "GET",
        dataType: "json",
        url: source,
        success: showNotifications,
        error: notificationError
    })
}

function showNotifications(notificationList) {
    console.log(notificationList);

    if (notificationList.length == 0) {
        $("#message").empty();
        $("#message").append("No notifications to display")
    }
    else {
        $("#message").empty();
        $(".notification-info").remove();
        for (var i = 0; i < notificationList.length; i++) {
            $(".table").append("<tr class=\"notification-info\"><td>" + notificationList[i].ItemName + "</td><td>" + notificationList[i].FirstName + " " + notificationList[i].LastName + "</td><td>" + formatJSONDate(notificationList[i].StartDate) + "</td><td>" + formatJSONDate(notificationList[i].EndDate) + "</td><td>" + formatJSONDate(notificationList[i].TimeStamp) + "</td><td>$" + Number(notificationList[i].TotalPrice).toLocaleString('en-US', { minimumFractionDigits: 2 }) + "</td></tr>");
        }
    }
}

function notificationError() {
    console.error("ERROR: Unable to resolve notifications. Please try again later.");
}

function formatJSONDate(jsonDate) {
    //Formats Microsoft JSON date (i.e. /Date(1556686986603)/) into a date object that JavaScript can read
    var parsedDate = new Date(parseInt(jsonDate.substr(6)));
    parsedDate.setDate(parsedDate.getDate() + 1); //Quick dirty fix for deployed site where dates are a day behind

    //Converts date object to MM/DD/YYYY format
    var mm = parsedDate.getMonth() + 1; //January is 0!
    var dd = parsedDate.getDate();
    var yyyy = parsedDate.getFullYear();

    if (mm < 10) {
        mm = '0' + mm;
    }
    if (dd < 10) {
        dd = '0' + dd;
    }

    var date = mm + '/' + dd + '/' + yyyy;

    return date;
}

function main() {
    notificationRequest();
    var interval = 1000 * 5; //Used to call notificationRequest() every 5 seconds.
    window.setInterval(notificationRequest, interval);
}

$(document).ready(main);