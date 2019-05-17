using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalLessThanOrEqualFilter<T> : BaseFilter<T>
    {
        public DecimalLessThanOrEqualFilter(Expression<Func<T, decimal>> expression, decimal max) : base(expression.GetPropertyName(), Operations.Operation.LessThanOrEqualTo, max)
        {
        }
    }
}
