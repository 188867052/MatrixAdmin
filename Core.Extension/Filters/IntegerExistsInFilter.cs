using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegerExistsInFilter<T, TPropertyType> : BaseCollectionExistsInFilter<T, TPropertyType, int>
    {
        public IntegerExistsInFilter(Expression<Func<T, ICollection<TPropertyType>>> expression, Expression<Func<TPropertyType, int>> secondExpression, IOperation operation, int value)
              : base(expression, secondExpression, operation, value)
        {
        }
    }
}
