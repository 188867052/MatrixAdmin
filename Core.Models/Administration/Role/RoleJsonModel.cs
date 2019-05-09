using System;
using Core.Entity.Enums;

namespace Core.Model.Administration.Role
{
    /// <summary>
    ///
    /// </summary>
    public class RoleJsonModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public StatusEnum Status { get; set; }

        public IsDeletedEnum IsDeleted { get; set; }

        public string CreatedOn { get; set; }

        public Guid CreatedByUserGuid { get; set; }

        public string CreatedByUserName { get; set; }

        public string ModifiedOn { get; set; }

        public Guid? ModifiedByUserGuid { get; set; }

        public string ModifiedByUserName { get; set; }

        /// <summary>
        /// 是否是超级管理员(超级管理员拥有系统的所有权限).
        /// </summary>
        public bool IsSuperAdministrator { get; set; }

        /// <summary>
        /// 是否是系统内置角色(系统内置角色不允许删除,修改操作).
        /// </summary>
        public bool IsBuiltin { get; set; }
    }
}
