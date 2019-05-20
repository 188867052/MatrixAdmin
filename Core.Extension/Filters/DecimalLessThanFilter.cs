using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DecimalLessThanFilter<T> : BaseFilter<T>
    {
        public DecimalLessThanFilter(Expression<Func<T, decimal>> expression, decimal max) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.LessThan, max)
        {
        }
    }
}
