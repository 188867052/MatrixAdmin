using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class IntegerEqualFilter<T> : BaseFilter<T>
    {
        public IntegerEqualFilter(Expression<Func<T, int?>> expression, int? value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}