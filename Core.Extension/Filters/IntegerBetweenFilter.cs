using System;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegerBetweenFilter<T> : IFilterInfo
    {
        public IntegerBetweenFilter(Expression<Func<T, int>> expression, int value, int value2)
        {
            this.PropertyName = expression.GetPropertyName();
            this.Value = value;
            this.Value2 = value2;
            this.Validate();
        }

        public Connector Connector { get; set; } = default;

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; } = Operations.Operation.Between;

        public object Value { get; set; }

        public object Value2 { get; set; }

        public bool IsFilterEnable => true;

        public void Validate()
        {
        }
    }
}
