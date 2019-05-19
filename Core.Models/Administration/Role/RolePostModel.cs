using System;

namespace Core.Model.Administration.Role
{
    /// <summary>
    ///
    /// </summary>
    public class RolePostModel : Pager
    {
        /// <summary>
        /// 是否已被删除.
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 状态.
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// 角色名称.
        /// </summary>
        public string RoleName { get; set; }

        public int? RoleId { get; set; }

        public string Description { get; set; }

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
