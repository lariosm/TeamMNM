/**
 * Calls CurrentLocation() to make a request to IPLocate
 */
function locateRequest() {
    var source = "/Resources/CurrentLocation/";

    $.ajax({
        type: "GET",
        dataType: "json",
        url: source,
        success: setLocation,
        error: ajaxError
    });
}

/**
 * Inserts longitude and latitude values to web page.
 * @param {any} locationData JSON data received from IPLocate server
 */
function setLocation(locationData) {
    var lat = locationData.latitude;
    var lon = locationData.longitude;
    var city = locationData.city;
    var state = locationData.subdivision;
    var zip = locationData.postal_code;

    $("#currentloc").remove()
    $("#location").append("<a class=\"nav-link\" id=\"currentloc\" href=\"#\"><i class=\"fa fa-map-marker\"></i> " + city + ", " + state + " " + zip + "</a>");

    $("#geoCoordinate").prepend("<input name = \"lng\" value = \"" + lon + "\" hidden />"); //Appends longitude value to HTML document
    $("#geoCoordinate").prepend("<input name = \"lat\" value = \"" + lat + "\" hidden />"); //Appends latitude value to HTML document
}

/** 
 * Displays AJAX error as pop-up alert
 */
function ajaxError() {
    console.error("ERROR: Unable to resolve location");
}

$(document).ready(locateRequest); //Calls main() when page is loaded.