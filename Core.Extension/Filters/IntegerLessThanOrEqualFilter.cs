using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegerLessThanOrEqualFilter<T> : BaseFilter<T>
    {
        public IntegerLessThanOrEqualFilter(Expression<Func<T, int>> expression, int value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)

        {
        }
    }
}
