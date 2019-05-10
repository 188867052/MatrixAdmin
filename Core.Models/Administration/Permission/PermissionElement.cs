using System;
using System.Collections.Generic;

namespace Core.Model.Administration.Permission
{
    /// <summary>
    ///
    /// </summary>
    public class PermissionElement
    {
        /// <summary>
        /// 权限编码.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 权限名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否已分配到指定角色.
        /// </summary>
        public bool IsAssignedToRole { get; set; }
    }
}
