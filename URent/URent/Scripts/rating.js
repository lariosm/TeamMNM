$(function () {
    $(".star").mouseover(function () {
        giveRating($(this), "FilledStar.png");
        $(this).css("cursor", "pointer");
    });

    $(".star").mouseout(function () {
        giveRating($(this), "EmptyStar.png");
    });

    $(".star").click(function () {
        $(".star").unbind("mouseout mouseover click");

        var listingId = parseInt($("#listingId").attr("value")); //Item ID of item being rated
        var profileId = parseInt($("#userId").attr("value")) //User ID of user being rated

        if (!isNaN(listingId)) { //Saves rating given to an item being rated
            var source = "/SUPItems/SetItemRating/";

            $.ajax({
                type: 'POST',
                url: source,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ itemId: listingId, rating: parseInt($(this).attr("id")) }),
            });
        }
        else if (!isNaN(profileId)) { //Saves rating given to user being rated
            var source = "/SUPUsers/SetUserRating/";

            $.ajax({
                type: 'POST',
                url: source,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ userId: profileId, rating: parseInt($(this).attr("id")) }),
            });
        }
        else {
            console.log("Something in rating.js is not working right...");
        }
    });
});

//Helper function for selecting all stars up to and including the current star user is hovering on
function giveRating(img, image) {
    img.attr("src", "/Content/Img/" + image)
        .prevAll(".star").attr("src", "/Content/Img/" + image);
}