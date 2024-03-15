$(document).ready(function () {
    "use strict";

    var connection = new signalR.HubConnectionBuilder().withUrl("/storeHub").build();

    connection.on("ReceiveClearOrder", function () {
        console.log("Thinh");
    })

    connection.start().then(function () {
        console.log("Đã kết nối tới máy chủ SignalR.");
    }).catch(function (err) {
        console.error(err.toString());
    });
})