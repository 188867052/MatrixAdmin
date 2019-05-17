using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DateTimeLessThanOrEqualFilter<T> : BaseSingleFilter<T>
    {
        public DateTimeLessThanOrEqualFilter(Expression<Func<T, DateTime>> expression, DateTime value) : base(expression.GetPropertyName(), Operations.Operation.LessThanOrEqualTo, value)
        {
        }
    }
}