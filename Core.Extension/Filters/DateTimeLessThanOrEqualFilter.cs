using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DateTimeLessThanOrEqualFilter<T> : BaseFilter<T>
    {
        public DateTimeLessThanOrEqualFilter(Expression<Func<T, DateTime>> expression, DateTime value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.LessThanOrEqualTo, value)
        {
        }
    }
}