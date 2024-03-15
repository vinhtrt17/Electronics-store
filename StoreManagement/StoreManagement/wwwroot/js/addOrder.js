$(document).ready(function () {
    "use strict";

    var connection = new signalR.HubConnectionBuilder().withUrl("/storeHub").build();

    $("#btn-1").click(function () {
        connection.invoke("addOrder").catch(function (err) {
            return console.error(err.toString());
        });
    })

    connection.start().then(function () {
        console.log("Đã kết nối tới máy chủ SignalR.");
    }).catch(function (err) {
        console.error(err.toString());
    });
})