function notificationRequest() {
    var source = "/Resources/NotificationRequest/"

    $.ajax({
        type: "GET",
        dataType: "json",
        url: source,
        success: showNotifications,
        error: ajaxError
    })
}

function showNotifications(notificationList) {
    console.log("In show notifications method");
    if (notificationList.length == 0) {
        $("#message").empty();
        $("#message").append("No notifications to display.")
    }
    else {
        $("#message").empty();
        $(".notification-info").remove();
        for (var i = 0; i < notificationList.length; i++) {
            $("#notiftable").prepend("<tr class=\notification-info\"><td>" + notificationList[i].item + "</td></td>" + notificationList[i.renter] + "</td></td>" + notificationList[i].start + "</td></td>" + notificationList[i].end + "</td></td>" + notificationList[i].time + "</td></td>" + notificationList.price + "</td></tr>");
        }
    }
}

function ajaxError() {
    alert("Unable to reach server. Please try again.");
}

function main() {
    notificationRequest();
    var interval = 1000 * 5;
    window.setInterval(notificationRequest, interval);
}

$(document).ready(main());