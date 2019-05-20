using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class IntegerGreaterThanOrEqualFilter<T> : BaseFilter<T>
    {
        public IntegerGreaterThanOrEqualFilter(Expression<Func<T, int>> expression, int value)
            : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.GreaterThanOrEqualTo, value)
        {
        }
    }
}
