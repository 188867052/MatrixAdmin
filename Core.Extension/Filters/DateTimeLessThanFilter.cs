using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DateTimeLessThanFilter<T> : BaseFilter<T>
    {
        public DateTimeLessThanFilter(Expression<Func<T, DateTime>> expression, DateTime value) : base(expression.GetPropertyName(), Operations.Operation.LessThan, value)
        {
        }
    }
}