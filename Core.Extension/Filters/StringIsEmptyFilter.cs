using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class StringIsEmptyFilter<T> : BaseFilter<T>
    {
        public StringIsEmptyFilter(Expression<Func<T, string>> expression) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.IsEmpty, string.Empty)
        {
        }
    }
}
