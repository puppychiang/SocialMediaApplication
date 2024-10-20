namespace LineOfficial_MVC.Models.Google
{
    /// <summary>
    /// Google Token Response
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id_token { get; set; }
    }


    public class UserInfoResponse
    {
        public string sub { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string picture { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public string locale { get; set; }
        public string hd { get; set; }
    }


    /// <summary>
    /// 取得Google用戶IDToken的回傳Model
    /// </summary>
    public class DecodeIDTokenResponse
    {
        /// <summary>
        /// Google ID
        /// </summary>
        public string sub { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 是否認證過
        /// </summary>
        public bool verified_email { get; set; }
        /// <summary>
        /// 全名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 姓氏
        /// </summary>
        public string family_name { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string given_name { get; set; }
        /// <summary>
        /// 個人Google網址
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// 大頭照
        /// </summary>
        public string picture { get; set; }
        /// <summary>
        /// 性別代號
        /// </summary>
        public string sex
        {
            get
            {
                string SexCode = "M";
                switch (gender.ToUpper())
                {
                    case "FEMALE":
                        SexCode = "F";
                        break;
                    case "MALE":
                    default:
                        SexCode = "M";
                        break;
                }
                return SexCode;
            }
        }
        /// <summary>
        /// 性別
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 預設語言
        /// </summary>
        public string locale { get; set; }
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public error error { get; set; }
        public DecodeIDTokenResponse()
        {
            sub = string.Empty;
            email = string.Empty;
            verified_email = false;
            name = string.Empty;
            family_name = string.Empty;
            given_name = string.Empty;
            link = string.Empty;
            picture = string.Empty;
            gender = string.Empty;
            locale = string.Empty;
            error = new error();
        }
    }

    /// <summary>
    /// 錯誤資訊
    /// </summary>
    public class error
    {
        /// <summary>
        /// 錯誤代碼
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string message { get; set; }
        public error()
        {
            code = string.Empty;
            message = string.Empty;
        }
    }
}
