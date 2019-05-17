using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegerBetweenFilter<T> : BaseBetweenFilter<T>
    {
        public IntegerBetweenFilter(Expression<Func<T, int?>> expression, int? min, int? max) : base(expression.GetPropertyName(), min, max)
        {
        }
    }
}
