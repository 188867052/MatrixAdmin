using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class BaseCollectionExistsInFilter<T, TCollection, TPropertyType> : IFilterInfo
    {
        public BaseCollectionExistsInFilter(Expression<Func<T, ICollection<TCollection>>> expression, IFilterInfo filter)
        {
            this.Connector = Connector.And;
            this.Operation = filter.Operation;
            string name = expression.ToString().Split('.')[1] + $"[{filter.PropertyName}]";
            this.FilterInfos = new List<IFilterInfo>
            {
                new FilterInfo<TPropertyType>(name, this.Operation,filter.Value)
            };
            this.IsFilterEnable = filter.Value != null;
        }

        public IEnumerable<IFilterInfo> FilterInfos { get; }

        public virtual bool IsFilterEnable { get; set; }

        public Connector Connector { get; set; } = default;

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; }

        public object Value { get; set; }

        public object Value2 { get; set; }

        public Expression Expression => throw new NotImplementedException();

        public void Validate()
        {
        }
    }
}
