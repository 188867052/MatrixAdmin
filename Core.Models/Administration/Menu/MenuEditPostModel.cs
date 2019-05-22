using System;
using Core.Entity.Enums;

namespace Core.Model.Administration.Menu
{
    /// <summary>
    ///
    /// </summary>
    public class MenuEditPostModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 菜单名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 链接地址.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 页面别名.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 菜单图标(可选).
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 父级ID.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 上级菜单名称.
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 菜单层级深度.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 描述信息.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序.
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否可用(0:禁用,1:可用).
        /// </summary>
        public StatusEnum Status { get; set; }

        /// <summary>
        /// 是否已删.
        /// </summary>
        public IsDeletedEnum IsDeleted { get; set; }

        /// <summary>
        /// 是否为默认路由.
        /// </summary>
        public YesOrNoEnum IsDefaultRouter { get; set; }

        public void MapTo(Entity.Menu entity)
        {
            entity.Name = this.Name;
            entity.Icon = this.Icon;
            entity.Level = 1;
            entity.ParentId = 1;
            entity.Sort = this.Sort;
            entity.Url = this.Url;
            entity.UpdateByUserId = 1;
            entity.UpdateByUserName = "System";
            entity.UpdateTime = DateTime.Now;
            entity.Description = this.Description;
            entity.ParentName = this.ParentName;
            entity.Alias = this.Alias;
        }
    }
}
