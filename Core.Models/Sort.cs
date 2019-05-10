using System.Collections.Generic;
using System.Linq;

namespace Core.Model
{

    /// <summary>
    /// 排序实体对象.
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sort"/> class.
        /// 排序实体对象构造函数.
        /// </summary>
        public Sort()
        {
            this.Direct = "DESC";
        }

        /// <summary>
        /// 排序字段.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 排序方向.
        /// </summary>
        public string Direct { get; set; }
    }
}
