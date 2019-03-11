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