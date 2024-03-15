$(document).ready(function () {
    "use strict";

    var connection = new signalR.HubConnectionBuilder().withUrl("/storeHub").build();

    connection.on("ReceiveAddOrder", function () {
        performAjaxRequest()
    })
    function performAjaxRequest() {
        // Gọi Ajax bằng cách sử dụng thư viện jQuery
        $.ajax({
            url: "Cart/ShowCart?handler=Add", // Thay đổi thành địa chỉ endpoint của bạn
            type: "GET", // Thay đổi phương thức HTTP của bạn (POST, GET, PUT, DELETE, v.v.)
            success: function (response) {
                if (response.status === 'Success') {
                    location.reload();
                }
            }
        });
    }


    connection.start().then(function () {
        console.log("Đã kết nối tới máy chủ SignalR.");
    }).catch(function (err) {
        console.error(err.toString());
    });
})