using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringEndsWithFilter<T> : BaseFilter<T>
    {
        public StringEndsWithFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), Operations.Operation.EndsWith, value)
        {
        }
    }
}
