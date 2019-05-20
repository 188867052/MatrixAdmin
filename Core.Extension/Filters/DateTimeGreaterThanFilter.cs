using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DateTimeGreaterThanFilter<T> : BaseFilter<T>
    {
        public DateTimeGreaterThanFilter(Expression<Func<T, DateTime>> expression, DateTime value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.GreaterThan, value)
        {
        }
    }
}