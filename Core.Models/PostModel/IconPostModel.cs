using System;
using Core.Model.ResponseModels;

namespace Core.Model.PostModel
{
    /// <summary>
    /// 图标
    /// </summary>
    public class IconPostModel : Pager
    {
        public int? Id { get; set; }

        /// <summary>
        /// 图标名称
        /// </summary>
        public string Code { get; set; }

        /// 图标的大小，单位是 px
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
        /// 是否可用
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

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
        public DateTime? ModifiedOn { get; set; }

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
