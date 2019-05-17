using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DateTimeGreaterThanFilter<T> : BaseFilter<T>
    {
        public DateTimeGreaterThanFilter(Expression<Func<T, DateTime>> expression, DateTime value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }
    }
}