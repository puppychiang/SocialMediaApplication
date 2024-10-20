// 提問頁面js

//自動登入
$(function () {
    login();
});

function login() {
    liff.init({
        liffId: '1661441976-R3dwolX5',
        //withLoginOnExternalBrowser: true, // Enable automatic login process
    }).then(() => {
        //liff.logout();
        if (!liff.isLoggedIn()) {
            //alert("尚未登入");
            const context = liff.getContext();
            console.log(context);

            //執行Liff LINE登入
            //liff.login({ redirectUri: "https://liff.line.me/1661441976-ZJrQ1aqK" });
        }
        else {
            //alert("LINE登入成功");
            //$('#lineVersion').html(liff.getLineVersion());
            //$('#os').html(liff.getOS());

            //取得登入資訊
            liff.getProfile()
                .then(profile => {
                    $('#userId').val(profile.userId);
                    $('#userIdtext').html(profile.userId);

                    //取消loading
                    //$('.wrap').show();
                    //$('.loading').hide();
                })
                .catch((err) => {
                    alert("error", err);
                });
        }
    }).catch((err) => {
        alert("error", err)
    });
}

//關閉liff網頁
$(document).on('click', '#close', function () {
    liff.closeWindow();
});

//綁定提問送出按鈕
$(document).on('click', '#formQuestion .save-button', function (e) {
    e.preventDefault();

    //驗證表單(需要jquery validate js, 在Modal頁面載入)
    if ($('#formQuestion').valid()) {
        var _url = $('#formQuestion').attr('action');
        // 取得form表單內所有input的值
        var _data = new FormData($('#formQuestion')[0]);
        var _setting = {
            type: "POST",
            url: _url,
            data: _data
        }
        AjaxReturnJson(_setting, "FormDataFile").then(res => {
            alert(res.message);
            console.log(res.success);
            if (res.success) {
                //重整畫面
                window.location.reload();
            }
        }).catch(err => {
            alert('與伺服器連線發生錯誤，請稍後再試')
        })
    }
})