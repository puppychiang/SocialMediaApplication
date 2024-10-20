
/**
 * 設置ajax -- 回傳Json
 * @param _setting 設定內容
 * @param _datatype 送出時的資料型態(postbody時的header修改)
 */
function AjaxReturnJson(_setting, _datatype = "JSON") {
    console.log(_setting);

    //移除url中的html tag
    //_setting.url = stripHTML(_setting.url);

    var defSetting = {
        contentType: "application/json; charset=utf-8",
        processData: true,
    };

    if (_datatype == "FormData") {
        defSetting.contentType = "application/x-www-form-urlencoded; charset=UTF-8";
        defSetting.processData = true;
    }

    if (_datatype == "FormDataFile") {
        defSetting.contentType = false;
        defSetting.processData = false;
    }

    if (_datatype == "JSON") {
        console.log('JSON');
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: _setting.url,
                type: _setting.type,
                headers: _setting.header,
                contentType: defSetting.contentType,
                processData: defSetting.processData,
                dataType: _datatype,
                data: _setting.data,
                cache: false,
                beforeSend: function (xhr) {
                    //xhr.setRequestHeader("requestverificationtoken", $('input:hidden[name="__RequestVerificationToken"]').val());
                    //顯示Loading
                    //$("#loading").show();
                    //showLoading()
                },
                success: resolve,
                erorr: reject,
                complete: function () {
                    //關閉Loading
                    //$("#loading").hide();
                    //closeLoading()
                },
                statusCode: {
                    500: function (err) {
                        reject(err);
                    },
                    401: function () {
                        reject("權限不足，無法執行操作");
                    },
                    403: function () {
                        reject("權限不足，無法執行操作");
                    },
                },
            });
        });
    } else {
        console.log('not JSON');
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: _setting.url,
                type: _setting.type,
                headers: _setting.header,
                contentType: defSetting.contentType,
                processData: defSetting.processData,
                data: _setting.data,
                cache: false,
                beforeSend: function (xhr) {
                    //xhr.setRequestHeader("requestverificationtoken", $('input:hidden[name="__RequestVerificationToken"]').val());
                    //顯示Loading
                    //$("#loading").show();
                    //showLoading()
                },
                success: resolve,
                erorr: reject,
                complete: function () {
                    //關閉Loading
                    //$("#loading").hide();
                    //closeLoading()
                },
                statusCode: {
                    500: function (err) {
                        reject(err);
                    },
                    401: function () {
                        reject("權限不足，無法執行操作");
                    },
                    403: function () {
                        reject("權限不足，無法執行操作");
                    },
                },
            });
        });
    }
}

/**
 * 設置ajax -- 回傳htlm
 * @param _setting 設定內容
 * @param _datatype 送出時的資料型態(postbody時的header修改)
 */
function AjaxReturnModal(_setting, _datatype = "JSON") {

    if (_datatype == "JSON") _setting.data = JSON.stringify(_setting.data);

    return new Promise((resolve, reject) => {
        $.ajax({
            url: _setting.url,
            type: _setting.type,
            contentType: "application/html",
            data: _setting.data,
            cache: false,
            beforeSend: function (xhr) {
                //xhr.setRequestHeader("requestverificationtoken", $('input:hidden[name="__RequestVerificationToken"]').val());
                //$("#loading").show();
            },
            success: resolve,
            erorr: reject,
            complete: function (xhr) {
                //CheckAuthenticated(xhr);
                //$("#loading").hide();
            },
            statusCode: {
                500: function (err) {
                    reject(err);
                },
                401: function () {
                    reject("使用者權限不足，無法執行操作時");
                },
            },
        });
    });
}

/**
 * 開啟Modal表單
 * @param {any} modalHtml
 */
function showModal(modalHtml) {
    document.getElementById("modal").style.display = "block";
    document.body.style.position = "fixed";
    document.body.style.width = "100%";
    $("#modal").html(modalHtml);
}

/**
 * 關閉Modal表單
 * */
function closeModal() {
    document.getElementById("modal").style.display = "none";
    document.body.style.position = "unset";
    $("#modal").empty();
}

/**
* 取消檔案
* @param {any} fileID
*/
function cancelFile(fileID) {
    console.log('取消檔案' + fileID);
    $(fileID).val('');
}


/**
 * 去除HTML Tag、去除空白鍵&nbsp;
 */
function stripHTML(input) {
    var output = '';
    if (typeof (input) == 'string') {
        //去除HTML Tag
        var output = input.replace(/(<([^>]+)>)/ig, "");
        //去除空白鍵&nbsp;
        output = output.replace(/&nbsp;/ig, "");
    }
    return output;
}