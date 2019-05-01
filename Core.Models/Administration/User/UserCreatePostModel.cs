using System;
using Core.Model.Enums;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserCreatePostModel
    {
        public int? Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserTypeEnum UserType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IsLockedEnum IsLocked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserStatusEnum Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IsDeletedEnum IsDeleted { get; set; }
        /// <summary>
        /// 用户描述信息
        /// </summary>
        public string Description { get; set; }
    }
}
