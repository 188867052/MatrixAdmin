using AutoMapper;
using Core.Entity;
using Core.Entity.Enums;

namespace Core.Model.Administration.User
{
    /// <summary>
    ///
    /// </summary>
    public class UserCreatePostModel
    {
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
        public ForbiddenStatusEnum? Status { get; set; }

        /// <summary>
        /// 用户描述信息.
        /// </summary>
        public string Description { get; set; }

        public Entity.User MapTo(IMapper mapper)
        {
            Entity.User entity = mapper.Map<UserCreatePostModel, Entity.User>(this);
            if (this.UserRole.HasValue)
            {
                entity.UserRoleMapping.Add(new UserRoleMapping
                {
                    UserId = entity.Id,
                    RoleId = (int)this.UserRole
                });
            }

            return entity;
        }
    }
}
