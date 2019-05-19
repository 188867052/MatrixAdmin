using System;

namespace Core.Model.Administration.Menu
{
    /// <summary>
    ///
    /// </summary>
    public class MenuPostModel : Pager
    {
        /// <summary>
        /// 菜单名称.
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 是否已被删除.
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 状态.
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// 上级菜单GUID.
        /// </summary>
        public Guid? ParentGuid { get; set; }

        /// <summary>
        /// 开始创建时间.
        /// </summary>
        public DateTime? StartCreateTime { get; set; }

        /// <summary>
        /// 结束创建时间.
        /// </summary>
        public DateTime? EndCreateTime { get; set; }
    }
}
