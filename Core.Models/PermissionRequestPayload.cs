using System;
using Core.Models.Models;

namespace Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionRequestPayload : PostedModel
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
