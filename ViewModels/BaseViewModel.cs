namespace LineOfficial_MVC.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            LineBotUserID = string.Empty;
        }
        /// <summary>
        /// 用戶在LineBot的ID
        /// </summary>
        public string LineBotUserID { get; set; }
    }
}
