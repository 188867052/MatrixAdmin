using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class StringStartsWithFilter<T> : BaseFilter<T>
    {
        public StringStartsWithFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.StartsWith, value)
        {
        }
    }
}
