using System.Collections.Generic;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public abstract class BaseSingleFilter<T> : IFilterInfo
    {
        protected BaseSingleFilter(string propertyName, IOperation operation, object value)
        {
            this.PropertyName = propertyName;
            this.Operation = operation;
            this.Value = value;
            this.Validate();
        }

        public virtual bool IsFilterEnable => true;

        public Connector Connector { get; set; } = default;

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; }

        public object Value { get; set; }

        public object Value2 { get; set; }

        public IEnumerable<IFilterInfo> FilterInfos => throw new System.NotImplementedException();

        public void Validate()
        {
        }
    }
}