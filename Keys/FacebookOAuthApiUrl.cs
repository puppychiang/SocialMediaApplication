namespace LineOfficial_MVC.Keys
{
    /// <summary>
    /// Facebook OAuth API 路徑
    /// </summary>
    public class FacebookOAuthApiUrl
    {
        /// <summary>
        /// 取得AccessToken
        /// </summary>
        public const string accessTokenUrl = "https://graph.facebook.com/v18.0/oauth/access_token";
        /// <summary>
        /// 取得使用者資訊
        /// </summary>
        public const string getUserInformationUrl = "https://graph.facebook.com/v18.0/me?fields=id%2Cname%2Cemail%2Cbirthday%2Clast_name%2Cfirst_name%2Cgender&access_token=";
        /// <summary>
        /// 取得AccessToken
        /// </summary>
        public const string getUserPictureUrl = "https://graph.facebook.com/v18.0/";
    }
}
