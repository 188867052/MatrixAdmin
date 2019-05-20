using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DecimalBetweenFilter<T> : BaseBetweenFilter<T>
    {
        public DecimalBetweenFilter(Expression<Func<T, decimal?>> expression, decimal? min, decimal? max) : base(expression.GetPropertyName(), min, max)
        {
        }
    }
}
