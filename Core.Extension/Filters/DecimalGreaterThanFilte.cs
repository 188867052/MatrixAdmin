using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DecimalGreaterThanFilter<T> : BaseFilter<T>
    {
        public DecimalGreaterThanFilter(Expression<Func<T, decimal>> expression, decimal value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.GreaterThan, value)
        {
        }
    }
}
