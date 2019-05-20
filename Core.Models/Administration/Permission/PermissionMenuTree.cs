using System.Collections.Generic;

namespace Core.Model.Administration.Permission
{
    /// <summary>
    /// 用于角色权限的菜单树.
    /// </summary>
    public class PermissionMenuTree
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionMenuTree"/> class.
        /// </summary>
        public PermissionMenuTree()
        {
            this.Permissions = new List<PermissionElement>();
            this.Children = new List<PermissionMenuTree>();
        }

        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 标题(菜单名称).
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否展开子节点.
        /// </summary>
        public bool Expand { get; set; }

        /// <summary>
        /// 禁掉响应.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 禁掉 checkbox.
        /// </summary>
        public bool DisableCheckbox { get; set; }

        /// <summary>
        /// 是否选中子节点 .
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// 是否勾选(如果勾选，子节点也会全部勾选).
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 当前菜单的所有权限都已分配到指定角色.
        /// </summary>
        public bool AllAssigned { get; set; }

        /// <summary>
        /// 当前菜单拥有的权限功能.
        /// </summary>
        public List<PermissionElement> Permissions { get; set; }

        /// <summary>
        /// 子节点属性数组.
        /// </summary>
        public List<PermissionMenuTree> Children { get; set; }
    }
}
