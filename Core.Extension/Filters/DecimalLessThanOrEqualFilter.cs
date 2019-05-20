using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DecimalLessThanOrEqualFilter<T> : BaseFilter<T>
    {
        public DecimalLessThanOrEqualFilter(Expression<Func<T, decimal>> expression, decimal max) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.LessThanOrEqualTo, max)
        {
        }
    }
}
