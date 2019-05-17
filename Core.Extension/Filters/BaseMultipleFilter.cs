using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public abstract class BaseMultipleFilter<T> : IFilterInfo
    {
        protected BaseMultipleFilter(string propertyName, object value, object value2)
        {
            this.PropertyName = propertyName;
            this.Value = value;
            this.Value2 = value2;
            this.Validate();
            this.Operation = Operations.Operation.Between;
            this.Connector = Connector.And;
        }

        public virtual bool IsFilterEnable => true;

        public Connector Connector { get; set; }

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; }

        public object Value { get; set; }

        public object Value2 { get; set; }

        public void Validate()
        {
        }
    }
}