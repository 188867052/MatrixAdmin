using Core.Entity.Enums;

namespace Core.Mvc.Authentication
{
    /// <summary>
    /// 登录用户上下文.
    /// </summary>
    public class AuthContextUser
    {
        /// <summary>
        /// 用户ID.
        /// </summary>
        public int Id { get; set; } = 1;

        /// <summary>
        /// 显示名.
        /// </summary>
        public string DisplayName { get; set; } = "System";

        /// <summary>
        /// 登录名.
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 电子邮箱.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 用户类型.
        /// </summary>
        public UserRoleEnum UserType { get; set; }

        /// <summary>
        /// 头像地址.
        /// </summary>
        public string Avator { get; set; }
    }
}