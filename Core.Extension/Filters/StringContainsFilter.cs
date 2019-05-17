using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringContainsFilter<T> : BaseSingleFilter<T>
    {
        public StringContainsFilter(Expression<Func<T, string>> expression,  string value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)

        {
        }
    }
}
