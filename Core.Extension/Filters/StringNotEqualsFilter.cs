using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class StringNotEqualsFilter<T> : BaseFilter<T>
    {
        public StringNotEqualsFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}