namespace Core.Model.Administration.Role
{
    /// <summary>
    ///
    /// </summary>
    public class RoleEditPostModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? Status { get; set; }

        public bool? IsEnable { get; set; }

        /// <summary>
        /// 是否是超级管理员(超级管理员拥有系统的所有权限).
        /// </summary>
        public bool IsSuperAdministrator { get; set; }

        public bool? IsForbidden { get; set; }
    }
}