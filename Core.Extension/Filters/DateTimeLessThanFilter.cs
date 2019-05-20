using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DateTimeLessThanFilter<T> : BaseFilter<T>
    {
        public DateTimeLessThanFilter(Expression<Func<T, DateTime>> expression, DateTime value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.LessThan, value)
        {
        }
    }
}