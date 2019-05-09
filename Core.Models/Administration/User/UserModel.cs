using ConsoleApp.DataModels;
using Core.Entity.Enums;
using System;

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

        public UserModel(ConsoleApp.DataModels.User user)
        {
            this.Id = user.Id;
            this.LoginName = user.LoginName;
            this.DisplayName = user.DisplayName;
            this.Password = user.Password;
            //this.UserType = user.UserType;
            this.CreateTime = user.CreateTime;
            //this.RoleName = user.RoleName;
            this.UserStatus = user.UserStatus;
            //this.Status = user.Status;
            this.IsDeleted = user.IsDeleted;
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
        public UserRoleEnum UserType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UserIsForbiddenEnum Status { get; set; }

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
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime { get; set; }

        public string RoleName { get; set; }

        public UserStatus UserStatus { get; set; }
    }
}
