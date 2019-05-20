using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class IntegerLessThanOrEqualFilter<T> : BaseFilter<T>
    {
        public IntegerLessThanOrEqualFilter(Expression<Func<T, int>> expression, int max) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.LessThanOrEqualTo, max)
        {
        }
    }
}
