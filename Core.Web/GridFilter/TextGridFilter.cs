using System;
using System.Linq.Expressions;
using Core.Extension;

namespace Core.Web.GridFilter
{
    public class TextGridFilter<T> : BaseGridFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextGridFilter{TPostModel}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="label">The label.</param>
        /// <param name="tooltip">The tooltip.</param>
        public TextGridFilter(Expression<Func<T, string>> expression, string label, string tooltip = default) : base(label, expression.GetPropertyName(), tooltip: tooltip)
        {
        }
    }
}