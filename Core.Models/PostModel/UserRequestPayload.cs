namespace Core.Model.PostModel
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRequestPayload : PostModel
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public bool? IsEnable { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public bool? Status { get; set; }
    }
}
