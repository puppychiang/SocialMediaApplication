///<reference path="http://tools.taiwantravelmap.com/javascript/JQuery/1.8.3/jquery.min.js" />
///<reference path="https://connect.facebook.net/zh_TW/all.js" />

/*
    程式說明:Facebook應用程式模組
    參數說明:
        AppID - Facebook應用程式ID.
        Languages - 介面使用語系(預設zh_TW).
        Version - 應用程式版本(預設v2.7).
        Scope - 應用程式需要取得權限(https://developers.facebook.com/docs/facebook-login/permissions)
        Display - 登入顯示方式.
                    page : 網址重新導向對話方塊實作 
                    popup : 視窗
                    touch : 行動版網頁
        ConfirmMsg - 登入前詢問訊息.
    功能說明:
        FormatDateTime  :   日期格式化，輸入值為日期，可複寫設計自己要的日期格式。
    其他說明： 
        使用時請先載入jquery.js與facebook的all.js。
    參考網站：
    https://developers.facebook.com/docs/facebook-login/overview
*/

function FaceBookAPI(Param) {
    var $this = this;
    //取得參數值
    $this.AppID = (typeof Param.AppID != 'undefined' && Param.AppID != null && Param.AppID != "") ? Param.AppID : undefined;
    $this.Languages = (typeof Param.Languages != 'undefined' && Param.Languages != null && Param.Languages != "") ? Param.Languages : "zh_TW";
    $this.Version = (typeof Param.Version != 'undefined' && Param.Version != null && Param.Version != "") ? Param.Version : "v8.0";
    $this.Scope = (typeof Param.Scope != 'undefined' && Param.Scope != null && Param.Scope != "") ? Param.Scope : "email, public_profile, user_birthday";
    $this.Display = (typeof Param.Display != 'undefined' && Param.Display != null && Param.Display != "") ? Param.Display : "popup";
    $this.ConfirmMsg = (typeof Param.ConfirmMsg != 'undefined' && Param.ConfirmMsg != null && Param.ConfirmMsg != "") ? Param.ConfirmMsg : undefined;
    //預設參數
    $this.accessToken = undefined;//驗證accessToken
    $this.userID = undefined;//FB帳號ID
    $this.userName = undefined;//FB暱稱
    $this.userEmail = undefined;//FB信箱
    $this.userData = undefined;//所有查詢到的資訊

    if ($this.AppID != undefined) {
        FB.init({
            appId: $this.AppID,
            status: true,
            xfbml: true,
            cookie: true,
            version: $this.Version
        });

        //FB帳號登入
        $this.login = function (callback) {
            //登入後執行項目
            if (typeof callback != "function") callback = function (response) { };

            //執行登入
            FB.login(function (response) {
                if (response.authResponse) {
                    $this.accessToken = response.authResponse.accessToken;
                }
                else {
                    alert("您的FaceBook帳號登入失敗！");
                }
                callback(response);
            }, { scope: $this.Scope, display: $this.Display });
        };

        //FB帳號登入檢查
        $this.checkLogin = function (callback) {
            //登入檢查後執行項目
            if (typeof callback != "function") callback = function (response) { };

            FB.getLoginStatus(function (response) {
                if (response.status == 'connected') {
                    $this.accessToken = response.authResponse.accessToken;
                    callback(response);
                } else {
                    //有詢問訊息，才跳出提示，否則直接前往登入
                    if ($this.ConfirmMsg != undefined) {
                        if (confirm($this.ConfirmMsg)) {
                            $this.login(callback);
                        }
                        else {
                            callback(response);
                        }
                    }
                    else {
                        $this.login(callback);
                    }
                }
            });
        };

        //取得USER資訊(id,name,email)
        $this.getUserProfile = function (callback) {
            //取得USER資訊後執行項目
            if (typeof callback != "function") callback = function (response) { };

            //先檢查是否以登入
            $this.checkLogin(function (response) {
                if (response.authResponse.accessToken && response.authResponse.userID) {
                    FB.api('/me', function (response) {
                        $this.userID = response.id;
                        $this.userName = response.name;
                        $this.userEmail = response.email;
                        $this.userData = response;
                        callback(response);
                    });
                }
                else
                    callback(response);
            });
        };

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/" + $this.Languages + "/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    }
};

var FB_API = undefined;

$(document).ready(function () {
    FB_API = new FaceBookAPI({ AppID: "953024698439811" });
});