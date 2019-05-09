using System;
using Core.Entity.Enums;

namespace Core.Model.Administration.Icon
{
    /// <summary>
    /// 
    /// </summary>
    public class IconJsonModel
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
        public StatusEnum Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IsDeletedEnum IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid CreatedByUserGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModifiedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ModifiedByUserGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModifiedByUserName { get; set; }
    }
}
