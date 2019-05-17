using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{

    public class StringEndsWithFilter<T> : BaseSingleFilter<T>
    {
        public StringEndsWithFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)

        {
        }
    }
}
