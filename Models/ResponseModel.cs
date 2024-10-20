namespace LineOfficial_MVC.Models
{
    public class ResponseModel
    {
        /// <summary> 
        /// �O�_���\
        /// </summary>
        public bool Success { get; set; }

        /// <summary> 
        /// �۩w�q���A�X
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary> 
        /// �T��
        /// </summary>
        public string? Message { get; set; }
    }

    /// <summary>
    /// �t�Τ����ϥΪ��^��Model
    /// </summary>
    public class ResponseModel<T> : ResponseModel
    {
        /// <summary>
        /// ���[���
        /// </summary>
        public T Data { get; set; }
    }
}