using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DateTimeBetweenFilter<T> : BaseMultipleFilter<T>
    {
        public DateTimeBetweenFilter(Expression<Func<T, DateTime>> expression, DateTime? value, DateTime? value2) : base(expression.GetPropertyName(), value, value2)
        {
        }
    }
}
