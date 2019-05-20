using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DateTimeEqualFilter<T> : BaseFilter<T>
    {
        public DateTimeEqualFilter(Expression<Func<T, DateTime>> expression, DateTime value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}