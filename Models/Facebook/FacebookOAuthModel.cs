namespace LineOfficial_MVC.Models.Facebook
{
    /// <summary>
    /// 取得token的回傳Model
    /// </summary>
    public class TokenResponse
    {
        public string access_token { get; set; }
        /// <summary>
        /// Bearer
        /// </summary>
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    /// <summary>
    /// 取得Facebook User 基本資料
    /// </summary>
    public class UserInfoResponse
    {
        /// <summary>
        /// Facebook ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 大頭貼
        /// </summary>
        public string picture { get; set; }
        /// <summary>
        /// 全名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 姓氏
        /// </summary>
        public string last_name { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string first_name { get; set; }

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
        /// 性別 male:男 female:女
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public error error { get; set; }

        public UserInfoResponse()
        {
            this.id = string.Empty;
            this.name = string.Empty;
            this.last_name = string.Empty;
            this.first_name = string.Empty;
            this.gender = string.Empty;
            this.email = string.Empty;
            this.birthday = string.Empty;
            this.error = new error();
        }
    }

    /// <summary>
    /// 錯誤資訊
    /// </summary>
    public class error
    {
        /// <summary>
        /// 錯誤描述
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 錯誤代碼
        /// </summary>
        public string code { get; set; }

        public error()
        {
            this.message = string.Empty;
            this.code = string.Empty;
        }

    }
}
