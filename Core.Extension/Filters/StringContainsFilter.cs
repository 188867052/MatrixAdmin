using Core.Entity;
using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringContainsFilter<T> : BaseFilter<T>
    {
        public StringContainsFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), Operations.Operation.Contains, value)
        {
        }

        public StringContainsFilter(StringField stringField, string value) : base(stringField, Operations.Operation.Contains, value)
        {
        }
    }
}
