using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class StringNotContainFilter<T> : BaseFilter<T>
    {
        public StringNotContainFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}
