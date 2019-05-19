using Core.Entity;

namespace Core.Model
{
    public class MenuModel
    {
        public MenuModel(Menu entity)
        {
        }

        /// <summary>
        /// 菜单名称.
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 是否已被删除.
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 状态.
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// 上级菜单ID.
        /// </summary>
        public int ParentId { get; set; }
    }
}