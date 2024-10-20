// 綁定頁面js

//自動登入
$(function () {
    login();
});

function login() {
    liff.init({
        liffId: '1661441976-8pxLDble',
        //withLoginOnExternalBrowser: true, // Enable automatic login process
    }).then(() => {
        //liff.logout();
        if (!liff.isLoggedIn()) {
            //alert("尚未登入");
            const context = liff.getContext();
            console.log(context);

            //執行Liff LINE登入
            //liff.login({ redirectUri: "https://liff.line.me/1661441976-8pxLDble" });
        }
        else {
            //alert("LINE登入成功");
            //$('#lineVersion').html(liff.getLineVersion());
            //$('#os').html(liff.getOS());

            //取得登入資訊
            liff.getProfile()
                .then(profile => {
                    //取得 userId
                    $('#userId').val(profile.userId);
                    $('#userIdtext').html(profile.userId);
                    //取得 用戶名稱
                    $('#userName').val(profile.displayName);
                    $('#userNametext').html(profile.displayName);
                    //取消loading
                    //$('.wrap').show();
                    //$('.loading').hide();
                })
                .catch((err) => {
                    alert("error", err);
                });

            //取得登入者信箱
            const user = liff.getDecodedIDToken();
            if (user && user.email) {
                console.log(user.email);
                $('#userEmail').val(user.email);
                $('#userEmailtext').html(user.email);
            }
        }
    }).catch((err) => {
        alert("error", err)
    });
}

//關閉liff網頁
$(document).on('click', '#close', function () {
    liff.closeWindow();
});

//綁定送出按鈕
$(document).on('click', '#formBindAccount .save-button', function (e) {
    e.preventDefault();

    //驗證表單(需要jquery validate js, 在Modal頁面載入)
    if ($('#formBindAccount').valid()) {
        var _url = $('#formBindAccount').attr('action');
        // 取得form表單內所有input的值
        var _data = new FormData(document.getElementById('formBindAccount'));
        var _setting = {
            type: "POST",
            url: _url,
            data: _data
        }
        AjaxReturnJson(_setting, "FormDataFile").then(res => {
            alert(res.message);
            console.log(res.data);
            if (res.success) {
                //跳轉商家資訊頁面
                location.href = res.data;
                //location.href = 'https://liff.line.me/1661441976-ZJrQ1aqK';
            }
        }).catch(err => {
            alert('與伺服器連線發生錯誤，請稍後再試rrrrrr')
        })
    }
})