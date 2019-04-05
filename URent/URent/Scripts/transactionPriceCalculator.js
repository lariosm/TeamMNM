function calculate() {
    var days = $("#TextBox3").val();
    console.log(days);
    var price = $('#price').data('price');
    console.log(price);
    var totalPrice = days * price;
    totalPrice = totalPrice.toFixed(1);
    console.log(totalPrice);
    $("#TotalPrice").val(totalPrice);
};

function totalDays() {
    var start = $("#StartDate").datepicker("getDate");
    var end = $("#EndDate").datepicker("getDate");
    var currentDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

    if (start >= currentDate && end > start) {
        var days = (end - start) / (1000 * 60 * 60 * 24);
        days = days.toFixed(0);
        $("#TextBox3").val(days);
        console.log(days);
        calculate();
    }
    else {
        return false;
    }
}