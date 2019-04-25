using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter
{
    public class IconGridFilter<T> : TextGridFilter<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="label"></param>
        public IconGridFilter(Expression<Func<T, string>> expression, string label) : base(expression, label)
        {
        }
    }
}