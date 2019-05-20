using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class StringEndsWithFilter<T> : BaseFilter<T>
    {
        public StringEndsWithFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EndsWith, value)
        {
        }
    }
}
