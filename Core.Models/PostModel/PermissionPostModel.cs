using System;

namespace Core.Model.PostModel
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionPostModel : PostModel
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// 关联菜单GUID
        /// </summary>
        public Guid? MenuGuid { get; set; }
    }
}
