using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringEqualsFilter<T> : BaseSingleFilter<T>
    {
        public StringEqualsFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)

        {
        }
    }
}
