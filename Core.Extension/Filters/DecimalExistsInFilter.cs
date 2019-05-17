using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalExistsInFilter<T, TCollection, TPropertyType> : BaseCollectionExistsInFilter<T, TCollection, decimal>
    {
        public DecimalExistsInFilter(Expression<Func<T, ICollection<TCollection>>> expression, IFilterInfo filter)
               : base(expression, filter)
        {
        }
    }
}
