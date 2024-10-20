// 歷史問題列表頁面js

//自動登入
$(function () {
    //判斷有無userId, 沒有則進行登入後, 取得userId並重新載入此頁面
    let _userId = $('#linebotuserId').val();
    if (_userId.trim().length === 0) {
        login();
    }
});

function login() {
    liff.init({
        liffId: '1661441976-rqEnZlRm',
        //withLoginOnExternalBrowser: true, // Enable automatic login process
    }).then(() => {
        //liff.logout();
        if (!liff.isLoggedIn()) {
            //alert("尚未登入");
            const context = liff.getContext();
            console.log(context);

            //執行Liff LINE登入
            //liff.login({ redirectUri: "https://liff.line.me/1661441976-rqEnZlRm" });
        }
        else {
            //alert("LINE登入成功");
            //$('#lineVersion').html(liff.getLineVersion());
            //$('#os').html(liff.getOS());

            //取得登入資訊
            liff.getProfile()
                .then(profile => {
                    //帶入 userId 並重新載入此頁面, 取得該用戶所屬的所有主問題
                    redirect(profile.userId);

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

//
/**
 * 重新載入頁面, 取得該用戶所屬的所有主問題
 * @param {any} userId
 */
function redirect(userId) {
    let _url = 'https://liff.line.me/1661441976-rqEnZlRm' + '?userId=' + userId;
    window.location.replace(_url);
}

//關閉liff網頁
$(document).on('click', '#close', function () {
    liff.closeWindow();
});

//綁定 前往聊天室按鈕
$(document).on('click', '.reply_item', function (e) {
    e.preventDefault();
    var _url = $(this).attr('href');
    var _userId = $(this).attr('data-userid');
    var _questionNo = $(this).attr('data-questionno');
    //組裝連結參數
    _url = _url + '?userId=' + _userId + '&questionNo=' + _questionNo;
    console.log(_url);
    //前往連結
    window.location.replace(_url);
})

//綁定 結案按鈕
$(document).on('click', '.finish_item', function (e) {
    e.preventDefault();
    var isConfirm = confirm('是否確定結案');
    if (isConfirm) {
        var _url = $(this).attr('href');
        var _userId = $(this).attr('data-userid');
        var _questionNo = $(this).attr('data-questionno');
        //組裝連結參數
        _url = _url + '?userId=' + _userId + '&questionNo=' + _questionNo;

        var _setting = {
            type: "GET",
            url: _url
        }
        AjaxReturnJson(_setting).then(res => {
            alert(res.message);
            console.log(res.success);
            if (res.success) {
                //重整畫面
                window.location.reload();
            }
        }).catch(err => {
            alert('與伺服器連線發生錯誤，請稍後再試');
            console.log(err);
        })
    }
})