using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DateTimeExistsInFilter<T, TPropertyType> : BaseCollectionExistsInFilter<T, TPropertyType, DateTime>
    {
        public DateTimeExistsInFilter(Expression<Func<T, ICollection<TPropertyType>>> expression, Expression<Func<TPropertyType, DateTime>> secondExpression, IOperation operation, DateTime value)
              : base(expression, secondExpression, operation, value)
        {
        }
    }
}
