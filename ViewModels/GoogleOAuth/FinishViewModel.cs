namespace LineOfficial_MVC.ViewModels.GoogleOAuth
{
    public class FinishViewModel
    {
        public string authorizationCode { get; set; }
        public string accessToken { get; set; }
        public string idToken { get; set; }
        public string userId { get; set; }
        public string pictureUrl { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}
