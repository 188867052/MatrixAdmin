using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class BooleanEqualFilter<T> : BaseFilter<T>
    {
        public BooleanEqualFilter(Expression<Func<T, bool?>> expression, bool? value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}
