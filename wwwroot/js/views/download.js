// 下載頁面js

//自動登入
$(function () {
    login();
});

function login() {
    liff.init({
        liffId: '1661441976-NDjvMBwW',
        //withLoginOnExternalBrowser: true, // Enable automatic login process
    }).then(() => {
        //liff.logout();
        if (!liff.isLoggedIn()) {
            alert("尚未登入");
            const context = liff.getContext();

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
                    $('#userIdtext').html(profile.userId);

                    //取消loading
                    //$('.wrap').show();
                    //$('.loading_login').hide();
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