using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringExistsInFilter<T, TPropertyType> : BaseCollectionExistsInFilter<T, TPropertyType, string>
    {
        public StringExistsInFilter(Expression<Func<T, ICollection<TPropertyType>>> expression, Expression<Func<TPropertyType, string>> secondExpression, IOperation operation, string value)
              : base(expression, secondExpression, operation, value)
        {
        }
    }
}
