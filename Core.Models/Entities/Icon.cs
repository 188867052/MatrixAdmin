using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Entities
{
    /// <summary>
    /// 图标实体类
    /// </summary>
    public class Icon
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 图标名称
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        /// <summary>
        /// 图标的大小，单位是 px
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string Size { get; set; }

        /// <summary>
        /// 图标颜色
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Color { get; set; }

        /// <summary>
        /// 自定义图标
        /// </summary>
        [Column(TypeName = "nvarchar(60)")]
        public string Custom { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOn { get; set; }

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
