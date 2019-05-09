namespace Core.Model.Administration.Icon
{
    /// <summary>
    /// 图标的视图类(创建/编辑)
    /// </summary>
    public class IconCreateViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 图标名称
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 图标的大小，单位是 px
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 图标颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 自定义图标
        /// </summary>
        public string Custom { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
