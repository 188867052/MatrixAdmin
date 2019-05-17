using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegerBetweenFilter<T> : BaseMultipleFilter<T>
    {
        public IntegerBetweenFilter(Expression<Func<T, int?>> expression, int? value, int? value2) : base(expression.GetPropertyName(), value, value2)
        {
        }
    }
}
