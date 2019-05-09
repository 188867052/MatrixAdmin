using System;
using System.Collections.Generic;

namespace Core.Model.Administration.Menu
{
    /// <summary>
    /// 用于iview的菜单树.
    /// </summary>
    public class MenuTree
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuTree"/> class.
        /// </summary>
        public MenuTree()
        {
            this.Children = new List<MenuTree>();
        }

        /// <summary>
        /// GUID.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Guid? ParentGuid { get; set; }

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
        /// 子节点属性数组.
        /// </summary>
        public List<MenuTree> Children { get; set; }
    }
}
