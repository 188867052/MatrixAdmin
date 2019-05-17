using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalBetweenFilter<T> : BaseBetweenFilter<T>
    {
        public DecimalBetweenFilter(Expression<Func<T, decimal?>> expression, decimal? min, decimal? max) : base(expression.GetPropertyName(), min, max)
        {
        }
    }
}
