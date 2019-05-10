using System.Collections.Generic;
using System.Linq;

namespace Core.Model
{
    /// <summary>
    /// 请求实体.
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostModel"/> class.
        /// </summary>
        public PostModel()
        {
            this.Sort = new List<Sort>();
            this.KeyWord = string.Empty;
        }

        /// <summary>
        /// 分页大小.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 排序对象集合(支持多个字段排序).
        /// </summary>
        public List<Sort> Sort { get; set; }

        /// <summary>
        /// 组合后的排序字符串.
        /// </summary>
        public string OrderBy
        {
            get
            {
                string orderBy = string.Empty;
                List<Sort> sort = this.Sort.Where(x => string.IsNullOrEmpty(x.Field) && string.IsNullOrEmpty(x.Direct)).ToList();
                if (sort.Count > 0)
                {
                    orderBy = "ORDER BY " + string.Join(",", sort.Select(x => $"{x.Field} {x.Direct}"));
                }

                return orderBy;
            }
        }

        /// <summary>
        /// 第一个排序字段(单个字段排序).
        /// </summary>
        public Sort FirstSort
        {
            get
            {
                if (this.Sort == null || this.Sort.Count == 0)
                {
                    return null;
                }

                Sort fs = this.Sort[0];
                if (fs == null)
                {
                    return null;
                }

                Sort sort = new Sort
                {
                    Direct = fs.Direct.ToUpper(),
                    Field = fs.Field
                };
                return sort;
            }
        }

        /// <summary>
        /// 搜索关键字.
        /// </summary>
        public string KeyWord { get; set; }
    }
}
