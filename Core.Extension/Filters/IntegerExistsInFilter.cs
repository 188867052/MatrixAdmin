using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Interfaces;
using Core.Extension.FieldInfos;

namespace Core.Extension.Filters
{
    public class IntegerExistsInFilter<T, TCollection> : BaseCollectionExistsInFilter<T, TCollection, int?>
    {
        public IntegerExistsInFilter(Expression<Func<T, ICollection<TCollection>>> expression, IFilterInfo filter)
              : base(expression, filter)
        {
        }

        public IntegerExistsInFilter(CollectionField expression, IFilterInfo filter)
        : base(expression, filter)
        {
        }
    }
}
