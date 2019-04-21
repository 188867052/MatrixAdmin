using Core.Model.Models;

namespace Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleRequestPayload : PostedModel
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool? Status { get; set; }
    }
}
