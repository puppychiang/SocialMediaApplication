using System.ComponentModel.DataAnnotations;

namespace LineOfficial_MVC.ViewModels
{
    /// <summary>
    /// 提問ViewModel
    /// </summary>
    public class QuestionViewModel : BaseViewModel
    {
        public QuestionViewModel()
        {
            Message = string.Empty;
            QuestionTypeSelectList = new();
        }
        /// <summary>
        /// 問題類別ID
        /// </summary>
        public int QuestionTypeID { get; set; }
        /// <summary>
        /// 問題內容
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Message { get; set; }
        /// <summary>
        /// 問題下拉選單列表
        /// </summary>
        public List<QuestionTypeModel> QuestionTypeSelectList { get; set; }
    }

    /// <summary>
    /// 問題下拉選單物件
    /// </summary>
    public class QuestionTypeModel
    {
        public QuestionTypeModel()
        {
            TypeName = string.Empty;
        }
        public int TypeID { get; set;}
        public string TypeName { get; set; }
    }
}
