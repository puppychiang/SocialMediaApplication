namespace LineOfficial_MVC.Models
{
    public class AppSettings
    {
        public FacebookOAuth FacebookOAuth { get; set; }
        public GoogleOAuth GoogleOAuth { get; set; }
        public LineOAuth LineOAuth { get; set; }
        public LineMessaging LineMessaging { get; set; }
    }

    public class FacebookOAuth
    {
        public string? clientId { get; set; }
        public string? clientSecret { get; set; }
        public string? redirectUri { get; set; }
    }

    public class GoogleOAuth
    {
        public string? grantType { get; set; }
        public string? clientId { get; set; }
        public string? clientSecret { get; set; }
        public string? redirectUri { get; set; }
    }

    public class LineOAuth
    {
        public string? grantType { get; set; }
        public string? clientId { get; set; }
        public string? clientSecret { get; set; }
        public string? redirectUri { get; set; }

    }

    public class LineMessaging
    {
        /// <summary>
        /// 我的Channel Token (API Bearer Token)
        /// </summary>
        public string? channelAccessToken { get; set; }
        /// <summary>
        /// line webhook use api uri (取得使用者傳送的檔案)
        /// </summary>
        public string? webhookApiUri { get; set; }

    }
}
