using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.Filters
{
    public class DateTimeExistsInFilter<T, TCollection, TPropertyType> : BaseCollectionExistsInFilter<T, TCollection, DateTime>
    {
        public DateTimeExistsInFilter(Expression<Func<T, ICollection<TCollection>>> expression, IFilterInfo filter)
                : base(expression, filter)
        {
        }
    }
}
