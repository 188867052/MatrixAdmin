using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class BaseCollectionExistsInFilter<T, TCollection, TPropertyType> : IFilterInfo
    {
        public BaseCollectionExistsInFilter(Expression<Func<T, ICollection<TCollection>>> expression, Expression<Func<TCollection, TPropertyType>> secondExpression, IOperation operation, TPropertyType value)
        {
            this.Connector = Connector.And;
            this.Operation = operation;
            this.FilterInfos = new List<IFilterInfo>
            {
                new FilterInfo<T, TCollection, TPropertyType>(expression, secondExpression, operation, value)
            };
        }

        public IEnumerable<IFilterInfo> FilterInfos { get; }

        public virtual bool IsFilterEnable => true;

        public Connector Connector { get; set; } = default;

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; }

        public object Value { get; set; }

        public object Value2 { get; set; }

        public void Validate()
        {
        }
    }
}
