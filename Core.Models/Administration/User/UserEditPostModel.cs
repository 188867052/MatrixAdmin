using Core.Model.Enums;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserEditPostModel
    {
        /// <summary>
        /// 
        /// </summary>
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
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 用户描述信息
        /// </summary>
        public string Description { get; set; }
    }
}
