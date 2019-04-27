using System;
using System.Collections.Generic;

namespace Core.Model.Administration.Role
{
    /// <summary>
    /// 用户获得的角色实体对象
    /// </summary>
    public class SaveUserRolesViewModel
    {
        /// <summary>
        /// 用户GUID
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// 已获得的角色代码集合
        /// </summary>
        public List<string> AssignedRoles { get; set; }
    }
}
