using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class StringEqualsFilter<T> : BaseFilter<T>
    {
        public StringEqualsFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}
