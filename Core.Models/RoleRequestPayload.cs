using Core.Model.ResponseModels;

namespace Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class RolePostModel : Pager
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
