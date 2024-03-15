$(document).ready(function () {

    function showAlert(content) {
        $('#message').css('display', 'block');
        $('#message').html(content);
        setTimeout(function () {
            $('#message').css('display', 'none');
        }, 3000)
    }

    $('.color-option').each(function (e) {
        $('input[type=radio]', this).get(0).checked = true;
    });

    $('.storage-option').each(function (e) {
        $('input[type=radio]', this).get(0).checked = true;
    });


    $('#btn-1').click(function () {
        let id = $('#productId').val();

        let color_col = document.getElementsByName('color-choice');
        for (let i = 0; i < color_col.length; i++) {
            if (color_col[i].type === "radio" && color_col[i].checked === true) {
                var color = color_col[i].value;
            }
        }
        let storage_col = document.getElementsByName('storage-choice');
        for (let i = 0; i < storage_col.length; i++) {
            if (storage_col[i].type === "radio" && storage_col[i].checked === true) {
                var storage = storage_col[i].value;
            }
        }

        $.ajax({
            type: "post",
            url: "/Cart/AddToCart",
            data: {
                id: id,
                num: 1,
                cid: color,
                sid: storage
            },
            success: function (response) {
                if (response.status === 'Success') {
                    showAlert('Đã thêm sản phẩm vào giỏ hàng!');
                }
            }
        });
    });
});
