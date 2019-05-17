using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalGreaterThanFilter<T> : BaseSingleFilter<T>
    {
        public DecimalGreaterThanFilter(Expression<Func<T, decimal>> expression, decimal value) : base(expression.GetPropertyName(), Operations.Operation.GreaterThan, value)
        {
        }
    }
}
