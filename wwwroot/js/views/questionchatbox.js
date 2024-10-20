// 提問頁面js

//自動登入
$(function () {
    // 判斷有無userId, 沒有則進行登入後, 取得userId並重新載入此頁面
    let _userId = $('#userId').val();
    console.log(_userId);
    if (_userId !== undefined && _userId.trim().length > 0) {
        // 滾動到頁面最下方
        scroll();
    }
    else {
        // 登入
        login();
    }
});

/**
 * 滾動到頁面最下方
 */
function scroll() {
    window.scrollTo(0, document.body.scrollHeight || document.documentElement.scrollHeight);
}

function login() {
    liff.init({
        liffId: '1661441976-vnGLKdyP',
        //withLoginOnExternalBrowser: true, // Enable automatic login process
    }).then(() => {
        //liff.logout();
        if (!liff.isLoggedIn()) {
            //alert("尚未登入");
            const context = liff.getContext();
            console.log(context);

            //執行Liff LINE登入
            //liff.login({ redirectUri: "https://liff.line.me/1661441976-vnGLKdyP" });
        }
        else {
            //alert("LINE登入成功");
            //$('#lineVersion').html(liff.getLineVersion());
            //$('#os').html(liff.getOS());

            //取得登入資訊
            liff.getProfile()
                .then(profile => {
                    //帶入 userId 並重新載入此頁面
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

//前往指定頁面
function redirect(userId) {
    let _questionNo = $('#questionNo').val();
    let _url = 'https://liff.line.me/1661441976-vnGLKdyP' + '?userId=' + userId + '&questionNo=' + _questionNo;
    window.location.replace(_url);
}

/**
 * 綁定 上傳檔案按鈕 (載入表單Modal)
 */
$(document).on('click', 'a.upload_item', function (e) {
    e.preventDefault();
    var _url = $(this).attr('href');
    var _setting = {
        type: "GET",
        url: _url,
    }
    AjaxReturnModal(_setting).then(res => {
        showModal(res);
        console.log('複製到Modal');
        $('#uploadFile1')[0].files = $('#file1')[0].files;
        $('#uploadFile2')[0].files = $('#file2')[0].files;
        $('#uploadFile3')[0].files = $('#file3')[0].files;
    }).catch(err => {
        console.log(err);
    })
})

/**
 * 綁定 上傳確定按鈕 (複製上傳資料到hidden欄位)
 */
$(document).on('click', '.upload-button', function (e) {
    e.preventDefault();
    console.log('複製到Hidden');
    $('#file1')[0].files = $('#uploadFile1')[0].files;
    $('#file2')[0].files = $('#uploadFile2')[0].files;
    $('#file3')[0].files = $('#uploadFile3')[0].files;

    let isOk = true;
    let errorMsg = "";

    //檢查檔案大小
    let $modal = $('#uploadFileModal');
    let fileCount = $modal.find("input[type='file']").length; //取得file個數
    // 逐筆檔案進行檢查
    if (fileCount > 0) {
        $modal.find("input[type='file']").each(function (index, file) {
            $file = $(file);
            let getfile = $file[0].files[0];
            //console.log(getfile);
            // 若找不到檔案代表 file 欄位非必填,使用者沒有上傳任何檔案,不用檢查
            if (typeof getfile !== "undefined") {
                console.log(getfile.size);
                if (getfile.size / 1024 > 5 * 1024) {
                    errorMsg += "第" + (Number(index) + 1) + "個檔案大小超過 5 MB\n";
                    // 刪除該筆資料
                    console.log($(this).attr('id'));
                    cancelFile('#' + $(this).attr('id'));
                    isOk = false;
                }
            }
        })
    }

    if (isOk) {
        closeModal();
        scroll();
    }
    else {
        alert(errorMsg);
    }
})

//綁定回覆送出按鈕
$(document).on('click', '#formQuestionChatBoxReply .save-button', function (e) {
    e.preventDefault();

    //驗證表單(需要jquery validate js, 在Modal頁面載入)
    if ($('#formQuestionChatBoxReply').valid()) {
        var _url = $('#formQuestionChatBoxReply').attr('action');
        // 取得form表單內所有input的值
        var _data = new FormData($('#formQuestionChatBoxReply')[0]);
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