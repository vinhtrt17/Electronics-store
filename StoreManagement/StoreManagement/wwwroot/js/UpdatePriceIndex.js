$(document).ready(function () {
    "use strict";

    var connection = new signalR.HubConnectionBuilder().withUrl("/storeHub").build();
    connection.on("ReceivePriceUpdate", function (name, price) {
        $("h3[name='pname']").each(function (index, item) {
            if ($(this).text() === name) {
                console.log($(this).text())
                $("p[name=price]").eq(index).text(formatCurrency(price));
            }
        })
    });

    function formatCurrency(number) {
        var formatter = new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' });
        return formatter.format(number);
    }

    var amount = 18490000;
    var formattedAmount = formatCurrency(amount);
    console.log(formattedAmount);


    connection.start().then(function () {
        console.log("Đã kết nối tới máy chủ SignalR.");
    }).catch(function (err) {
        console.error(err.toString());
    });
})