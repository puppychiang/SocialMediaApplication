namespace LineOfficial_MVC.Models
{
    public class ResponseModel
    {
        /// <summary> 
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary> 
        /// 自定義狀態碼
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary> 
        /// 訊息
        /// </summary>
        public string? Message { get; set; }
    }

    /// <summary>
    /// 系統內部使用的回傳Model
    /// </summary>
    public class ResponseModel<T> : ResponseModel
    {
        /// <summary>
        /// 附加資料
        /// </summary>
        public T Data { get; set; }
    }
}