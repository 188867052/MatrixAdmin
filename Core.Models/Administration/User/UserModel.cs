using System;
using Core.Model.Enums;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserModel
    {
        public UserModel()
        {
            this.UserStatus = new UserStatus();
        }

        public UserModel(User user)
        {
            this.Id = user.Id;
            this.LoginName = user.LoginName;
            this.DisplayName = user.DisplayName;
            this.Password = user.Password;
            this.UserType = user.UserType;
            this.CreateTime = user.CreateTime;
            this.RoleName = user.RoleName;
            this.UserStatus = user.UserStatus;
            this.CreatedByUserName = user.CreatedByUserName;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

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
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid CreatedByUserGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ModifiedByUserGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModifiedByUserName { get; set; }

        public string RoleName { get; set; }

        public UserStatus UserStatus { get; set; }
    }
}
