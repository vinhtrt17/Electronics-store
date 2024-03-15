$(document).ready(function () {
    "use strict";

    var connection = new signalR.HubConnectionBuilder().withUrl("/storeHub").build();

    $('#confirmAction').click(function (e) {
        let name = $('input[name=pname]').val().trim();
        let price = $('input[name=price]').val().trim();
        console.log(name)
        console.log(price)
        connection.invoke("UpdatePrice", name, price).catch(function (err) {
            return console.error(err.toString());
        });
        e.preventDefault();
    });

    connection.start().then(function () {
        console.log("Đã kết nối tới máy chủ SignalR.");
    }).catch(function (err) {
        console.error(err.toString());
    });
})