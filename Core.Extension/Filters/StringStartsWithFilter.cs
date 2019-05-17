using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringStartsWithFilter<T> : BaseFilter<T>
    {
        public StringStartsWithFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), Operations.Operation.StartsWith, value)
        {
        }
    }
}
