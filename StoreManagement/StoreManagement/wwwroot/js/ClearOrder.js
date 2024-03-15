$(document).ready(function () {
    "use strict";

    var connection = new signalR.HubConnectionBuilder().withUrl("/storeHub").build();

    $("#submit").click(function () {
        let uid = $("#uid").val().trim();
        console.log(uid);
        connection.invoke("ClrOrder",uid).catch(function (err) {
            return console.error(err.toString());
        });
    })

    connection.start().then(function () {
        console.log("Đã kết nối tới máy chủ SignalR.");
    }).catch(function (err) {
        console.error(err.toString());
    });
})