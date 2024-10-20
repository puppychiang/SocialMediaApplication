using System.ComponentModel.DataAnnotations;

namespace LineOfficial_MVC.ViewModels
{
    /// <summary>
    /// 聊天室回覆ViewModel
    /// </summary>
    public class QuestionChatBoxViewModel : BaseViewModel
    {
        public QuestionChatBoxViewModel()
        {
            QuestionNo = string.Empty;
            Message = string.Empty;
        }

        public string QuestionNo { get; set; }

        /// <summary>
        /// 問題內容
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Message { get; set; }
    }
}
