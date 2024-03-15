$(document).ready(function () {
    //Load category
    $.ajax({
        type: "get",
        url: "/HomePage/Index?handler=LoadCategory",
        success: function (receive) {
            let categories = JSON.parse(receive);
            for (let i in categories) {
                $('#catemenu').append(`<a class="dropdown-item" role="presentation" href="/HomePage/Index?id=${categories[i].Cid}">${categories[i].Cname}</a>`);
            }
        }
    });
})