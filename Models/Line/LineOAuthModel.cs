namespace LineOfficial_MVC.Models.Line
{
    /// <summary>
    /// 取得token的回傳Model
    /// </summary>
    public class TokenResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string id_token { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        /// <summary>
        /// Bearer
        /// </summary>
        public string token_type { get; set; }
    }

    /// <summary>
    /// 取得Line用戶profile的回傳Model
    /// </summary>
    public class InformationResponse
    {
        public string sub { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
    }

    /// <summary>
    /// 取得Line用戶profile的回傳Model
    /// </summary>
    public class ProfileResponse
    {
        public string userId { get; set; }
        public string displayName { get; set; }
        public string pictureUrl { get; set; }
        public string statusMessage { get; set; }
    }

    /// <summary>
    /// 取得Line用戶IDToken的回傳Model
    /// </summary>
    public class DecodeIDTokenResponse
    {
        public string iss { get; set; }
        public string sub { get; set; }
        public string aud { get; set; }
        public int exp { get; set; }
        public int iat { get; set; }
        public string nonce { get; set; }
        public List<string> amr { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string email { get; set; }
    }
}
