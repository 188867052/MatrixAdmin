using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalGreaterThanOrEqualFilter<T> : BaseFilter<T>
    {
        public DecimalGreaterThanOrEqualFilter(Expression<Func<T, decimal>> expression, decimal value)
            : base(expression.GetPropertyName(), Operations.Operation.GreaterThanOrEqualTo, value)
        {
        }
    }
}
