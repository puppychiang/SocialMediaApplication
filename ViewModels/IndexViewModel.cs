using System.ComponentModel.DataAnnotations;

namespace LineOfficial_MVC.ViewModels
{
    /// <summary>
    /// 綁定SCM帳號 ViewModel
    /// </summary>
    public class IndexViewModel : BaseViewModel
    {
        public IndexViewModel()
        {
            CustomerNum = string.Empty;
            CustomerAccountName = string.Empty;
        }
        /// <summary>
        /// 商家編號
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string CustomerNum { get; set; }
        /// <summary>
        /// 商家Line用戶名稱
        /// </summary>
        public string CustomerAccountName { get; set; }
        // <summary>
        /// 商家Line用戶信箱
        /// </summary>
        public string CustomerAccountEmail { get; set; }
        /// <summary>
        /// 商家Line用戶電話
        /// </summary>
        public string CustomerAccountPhone { get; set; }
    }
}
