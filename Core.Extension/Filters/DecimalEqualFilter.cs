using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DecimalEqualFilter<T> : BaseFilter<T>
    {
        public DecimalEqualFilter(Expression<Func<T, decimal?>> expression, decimal? value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}
