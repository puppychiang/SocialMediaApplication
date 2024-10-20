namespace LineOfficial_MVC.Keys
{
    /// <summary>
    /// Line OAuth API 路徑
    /// </summary>
    public class LineOAuthApiUrl
    {
        /// <summary>
        /// 取得AccessToken、RefreshToken、IDToken
        /// </summary>
        public const string accessTokenUrl = "https://api.line.me/oauth2/v2.1/token";
        /// <summary>
        /// 刷新AccessToken
        /// </summary>
        public const string refreshAccessTokenUrl = "https://api.line.me/oauth2/v2.1/token";
        /// <summary>
        /// 使用ID Token取得使用者資訊
        /// </summary>
        public const string decodeIDTokenUrl = "https://api.line.me/oauth2/v2.1/verify";
        /// <summary>
        /// 取得使用者資訊
        /// </summary>
        public const string getUserInformationUrl = "https://api.line.me/oauth2/v2.1/userinfo";
        /// <summary>
        /// 取得使用者個人介紹資訊
        /// </summary>
        public const string getUserProfileUrl = "https://api.line.me/v2/profile";
    }
}
