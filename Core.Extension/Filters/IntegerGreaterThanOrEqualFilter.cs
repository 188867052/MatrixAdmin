using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegerGreaterThanOrEqualFilter<T> :  BaseFilter<T>
    {
        public IntegerGreaterThanOrEqualFilter(Expression<Func<T, int>> expression, int value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)

        {
        }
    }
}
