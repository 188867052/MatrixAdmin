using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.GridFilter
{
    public class TextGridFilter<TPostModel> : BaseGridFilter
    {
        /// <summary>
        /// 构造函数[a
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="label"></param>
        public TextGridFilter(Expression<Func<TPostModel, string>> expression, string label) : base(label, expression.GetPropertyInfo())
        {
        }
    }
}