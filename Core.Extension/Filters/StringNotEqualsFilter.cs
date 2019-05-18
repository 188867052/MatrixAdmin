using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringNotEqualsFilter<T> : BaseFilter<T>
    {
        public StringNotEqualsFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }
    }
}