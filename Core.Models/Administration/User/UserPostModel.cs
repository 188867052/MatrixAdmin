using System;
using Core.Model.Enums;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserPostModel : Pager
    {
        /// <summary>   
        /// 是否已被删除
        /// </summary>
        public bool? IsEnable { get; set; }

        public string LoginName { get; set; }

        public string DisplayName { get; set; }

        public string Avatar { get; set; }

        public UserStatusEnum? Status { get; set; }

        public string UserType{ get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedOn { get; set; }

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

        /// <summary>
        /// 用户描述信息
        /// </summary>
        public string Description { get; set; }
    }
}
