using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DateTimeEqualFilter<T> : BaseFilter<T>
    {
        public DateTimeEqualFilter(Expression<Func<T, DateTime>> expression, DateTime value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }
    }
}