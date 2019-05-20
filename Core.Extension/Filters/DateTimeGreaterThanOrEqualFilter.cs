using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DateTimeGreaterThanOrEqualFilter<T> : BaseFilter<T>
    {
        public DateTimeGreaterThanOrEqualFilter(Expression<Func<T, DateTime>> expression, DateTime value)
            : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.GreaterThanOrEqualTo, value)
        {
        }
    }
}