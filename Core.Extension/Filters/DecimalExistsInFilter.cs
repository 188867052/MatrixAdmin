using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalExistsInFilter<T, TPropertyType> : BaseCollectionExistsInFilter<T, TPropertyType, decimal>
    {
        public DecimalExistsInFilter(Expression<Func<T, ICollection<TPropertyType>>> expression, Expression<Func<TPropertyType, decimal>> secondExpression, IOperation operation, decimal value)
              : base(expression, secondExpression, operation, value)
        {
        }
    }
}
