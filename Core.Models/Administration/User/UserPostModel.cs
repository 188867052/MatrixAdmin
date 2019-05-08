using System;
using Core.Entity.Enums;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// User post model.
    /// </summary>
    public class UserPostModel : Pager
    {
        /// <summary>   
        /// 是否已被删除
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 展示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public UserIsForbiddenEnum? Status { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// 开始创建时间
        /// </summary>
        public DateTime? StartCreateTime { get; set; }

        /// <summary>
        /// 结束创建时间
        /// </summary>
        public DateTime? EndCreateTime { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// 最近修改时间
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// 最近修改者姓名
        /// </summary>
        public string ModifiedByUserName { get; set; }
    }
}
