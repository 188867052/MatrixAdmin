using System;
using Core.Entity;
using Core.Entity.Enums;

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
        public string Avatar { get; set; }

        /// <summary>
        ///
        /// </summary>
        public UserRoleEnum? UserRole { get; set; }

        /// <summary>
        ///
        /// </summary>
        public IsLockedEnum IsLocked { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ForbiddenStatusEnum Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 用户描述信息.
        /// </summary>
        public string Description { get; set; }

        public void MapTo(Entity.User entity)
        {
            entity.DisplayName = this.DisplayName;
            entity.LoginName = this.LoginName;
            entity.Password = this.Password;
            entity.UpdateTime = DateTime.Now;

            if (this.UserRole.HasValue)
            {
                if (entity.RoleMapping != null)
                {
                    entity.RoleMapping.RoleId = (int)this.UserRole.Value;
                }
                else
                {
                    entity.UserRoleMapping.Add(new UserRoleMapping
                    {
                        UserId = this.Id,
                        RoleId = (int)this.UserRole
                    });
                }
            }
        }
    }
}
