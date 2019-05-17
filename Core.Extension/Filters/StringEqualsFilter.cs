using Core.Entity;
using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringEqualsFilter<T> : BaseFilter<T>
    {
        public StringEqualsFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }

        public StringEqualsFilter(StringField fieldInfo, string value) : base(fieldInfo.Value, Operations.Operation.EqualTo, value)
        {
        }
    }
}
