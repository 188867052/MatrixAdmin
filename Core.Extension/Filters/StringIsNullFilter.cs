using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{

    public class StringIsNullFilter<T> : BaseFilter<T>
    {
        public StringIsNullFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)

        {
        }
    }
}
