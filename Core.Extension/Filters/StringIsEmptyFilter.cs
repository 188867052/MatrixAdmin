using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class StringIsEmptyFilter<T> : BaseFilter<T>
    {
        public StringIsEmptyFilter(Expression<Func<T, string>> expression) : base(expression.GetPropertyName(), Operations.Operation.IsEmpty, string.Empty)
        {
        }
    }
}
