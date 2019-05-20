using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.Filters
{
    public class BaseInArrayFilte<T> : IFilterInfo
    {
        public BaseInArrayFilte(string propertyName, object value)
        {
            this.PropertyName = propertyName;
            this.SetValues(value);
            this.Validate();
            this.Operation = ExpressionBuilder.Operations.Operation.In;
            this.Connector = Connector.And;
            this.IsFilterEnable = true;
        }

        public bool IsFilterEnable { get; set; }

        public Connector Connector { get; set; }

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; }

        public object Value { get; set; }

        public object Value2 { get; set; }

        public IEnumerable<IFilterInfo> FilterInfos { get; set; }

        public Expression Expression { get; set; }

        public void Validate()
        {
        }

        private void SetValues(object value)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(typeof(int[]).GetElementType());
            this.Value = value != null ? Activator.CreateInstance(constructedListType, value) : null;
        }
    }
}
