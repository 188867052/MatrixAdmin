using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DecimalGreaterThanOrEqualFilter<T> : BaseFilter<T>
    {
        public DecimalGreaterThanOrEqualFilter(Expression<Func<T, decimal>> expression, decimal value)
            : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.GreaterThanOrEqualTo, value)
        {
        }
    }
}
