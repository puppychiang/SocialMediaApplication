namespace LineOfficial_MVC.Keys
{
    /// <summary>
    /// Google OAuth API 路徑
    /// </summary>
    public class GoogleOAuthApiUrl
    {
        /// <summary>
        /// 取得AccessToken、IDToken
        /// </summary>
        public const string accessTokenUrl = "https://oauth2.googleapis.com/token";
        /// <summary>
        /// 取得IDToken資訊
        /// </summary>
        public const string decodeIDTokenUrl = "https://oauth2.googleapis.com/tokeninfo?id_token=";
        /// <summary>
        /// 取得使用者資訊
        /// </summary>
        public const string getUserInformationUrl = "https://www.googleapis.com/oauth2/v3/userinfo";
    }
}
