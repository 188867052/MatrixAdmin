using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringInArrayFilte<T> : IFilterInfo
    {
        public StringInArrayFilte(Expression<Func<T, string>> expression, string[] value)
        {
            this.PropertyName = expression.GetPropertyName();
            this.SetValues(value);
            this.Validate();
        }

        public bool IsFilterEnable => true;

        public Connector Connector { get; set; } = default;

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; } = Operations.Operation.In;

        public object Value { get; set; }

        public object Value2 { get; set; }

        public void Validate()
        {
        }

        private void SetValues(string[] value)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(typeof(int[]).GetElementType());
            this.Value = value != null ? Activator.CreateInstance(constructedListType, value) : null;
        }
    }
}
