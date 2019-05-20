using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class IntegerGreaterThanFilter<T> : BaseFilter<T>
    {
        public IntegerGreaterThanFilter(Expression<Func<T, int>> expression, int value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.GreaterThan, value)
        {
        }
    }
}
