using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegerGreaterThanOrEqualFilter<T> : BaseSingleFilter<T>
    {
        public IntegerGreaterThanOrEqualFilter(Expression<Func<T, int>> expression, int value)
            : base(expression.GetPropertyName(), Operations.Operation.GreaterThanOrEqualTo, value)

        {
        }
    }
}
