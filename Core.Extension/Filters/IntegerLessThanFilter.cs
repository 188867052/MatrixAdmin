using System;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegerLessThanFilter<T> : IFilterInfo
    {
        public IntegerLessThanFilter(Expression<Func<T, int>> expression, int value)
        {
            this.PropertyName = expression.GetPropertyName();
            this.Value = value;
        }

        public Connector Connector { get; set; } = default;

        public string PropertyName { get; set; }

        public IOperation Operation { get; set; } = Operations.Operation.LessThan;

        public object Value { get; set; }

        public object Value2 { get; set; }

        public void Validate()
        {
        }
    }
}
