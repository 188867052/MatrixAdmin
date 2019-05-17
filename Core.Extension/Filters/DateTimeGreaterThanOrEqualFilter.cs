using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DateTimeGreaterThanOrEqualFilter<T> : BaseSingleFilter<T>
    {
        public DateTimeGreaterThanOrEqualFilter(Expression<Func<T, DateTime>> expression, DateTime value)
            : base(expression.GetPropertyName(), Operations.Operation.GreaterThanOrEqualTo, value)
        {
        }
    }
}