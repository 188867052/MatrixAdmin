using System;
using System.Linq.Expressions;
using Core.Extension;

namespace Core.Web.GridFilter
{
    public class IntegerGridFilter<TPostModel> : BaseGridFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerGridFilter{TPostModel}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="label">The label.</param>
        public IntegerGridFilter(Expression<Func<TPostModel, int?>> expression, string label) : base(label, expression.GetPropertyName())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerGridFilter{TPostModel}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="label">The label.</param>
        public IntegerGridFilter(Expression<Func<TPostModel, int>> expression, string label) : base(label, expression.GetPropertyName())
        {
        }
    }
}