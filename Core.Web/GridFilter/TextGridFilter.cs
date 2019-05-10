using System;
using System.Linq.Expressions;
using Core.Extension;

namespace Core.Web.GridFilter
{
    public class TextGridFilter<TPostModel> : BaseGridFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextGridFilter{TPostModel}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="label">The label.</param>
        public TextGridFilter(Expression<Func<TPostModel, string>> expression, string label) : base(label, expression.GetPropertyName())
        {
        }
    }
}