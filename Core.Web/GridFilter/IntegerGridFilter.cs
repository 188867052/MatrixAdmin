using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.GridFilter
{
    public class IntegerGridFilter<TPostModel> : BaseGridFilter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="label"></param>
        public IntegerGridFilter(Expression<Func<TPostModel, int?>> expression, string label) : base(label, expression.GetPropertyInfo())
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="label"></param>
        public IntegerGridFilter(Expression<Func<TPostModel, int>> expression, string label) : base(label, expression.GetPropertyInfo())
        {
        }
    }
}