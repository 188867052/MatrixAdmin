using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class IntegerLessThanFilter<T> : BaseFilter<T>
    {
        public IntegerLessThanFilter(Expression<Func<T, int>> expression, int max) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.LessThan, max)
        {
        }
    }
}
