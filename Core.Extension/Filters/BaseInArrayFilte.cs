using System;
using System.Collections.Generic;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class BaseInArrayFilte<T> : IFilterInfo
    {
        public BaseInArrayFilte(string propertyName, object value)
        {
            this.PropertyName = propertyName;
            this.SetValues(value);
            this.Validate();
            this.Operation = Operations.Operation.In;
            this.Connector = Connector.And;
        }

        public bool IsFilterEnable => true;

        public Connector Connector { get; set; }

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; }

        public object Value { get; set; }

        public object Value2 { get; set; }

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
