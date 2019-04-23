using System;
using Core.Model.PostModel;

namespace Core.Model.Menu
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuRequestPayload : PostModel.PostModel
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
        /// 上级菜单GUID
        /// </summary>
        public Guid? ParentGuid { get; set; }
    }
}
