using System;
using System.Linq;
using Core.Entity.Enums;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// UserModel.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class.
        /// </summary>
        public UserModel()
        {
            this.UserStatus = new UserStatusModel();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class.
        /// </summary>
        /// <param name="user">user.</param>
        public UserModel(Entity.User user)
        {
            this.Id = user.Id;
            this.LoginName = user.LoginName;
            this.DisplayName = user.DisplayName;
            this.Password = user.Password;
            if (user.UserRole != null)
            {
                this.UserRole = (UserRoleEnum)user.UserRole;
            }

            this.CreateTime = user.CreateTime;
            this.Status = (ForbiddenStatusEnum)user.Status;
            this.IsDeleted = user.IsDeleted;
            this.CreatedByUserName = user.CreateByUserName;
            this.UpdateTime = user.UpdateTime;

            if (user.UserStatus != null)
            {
                this.UserStatus = new UserStatusModel(user.UserStatus);
            }

            if (user.UserRoleMapping != null && user.UserRoleMapping.Count > 0)
            {
                this.RoleName = user.UserRoleMapping.First().Role.Name;
            }
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
        /// 用户角色.
        /// </summary>
        public UserRoleEnum UserRole { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ForbiddenStatusEnum Status { get; set; }

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
        /// 更新时间.
        /// </summary>
        public DateTime UpdateTime { get; set; }

        public string RoleName { get; set; }

        public UserStatusModel UserStatus { get; set; }

        public static UserModel Convert(Entity.User item)
        {
            return new UserModel(item);
        }
    }
}
