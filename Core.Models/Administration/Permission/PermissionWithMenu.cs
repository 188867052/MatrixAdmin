using System;
using Core.Entity.Enums;

namespace Core.Model.Administration.Permission
{
    /// <summary>
    /// 权限实体类
    /// </summary>
    public class PermissionWithMenu
    {
        /// <summary>
        /// 权限码
        /// </summary>
        public string PermissionCode { get; set; }
        /// <summary>
        /// 权限操作码
        /// </summary>
        public string PermissionActionCode { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PermissionTypeEnum PermissionType { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单GUID
        /// </summary>
        public Guid MenuGuid { get; set; }
        /// <summary>
        /// 菜单别名(与前端路由配置中的name值保持一致)
        /// </summary>
        public string MenuAlias { get; set; }
        /// <summary>
        /// 是否是默认前端路由
        /// </summary>
        public YesOrNoEnum IsDefaultRouter { get; set; }
    }
}