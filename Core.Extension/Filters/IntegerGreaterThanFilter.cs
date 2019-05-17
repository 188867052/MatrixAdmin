using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{

    public class IntegerGreaterThanFilter<T> : BaseSingleFilter<T>
    {
        public IntegerGreaterThanFilter(Expression<Func<T, int>> expression, int value) : base(expression.GetPropertyName(), Operations.Operation.GreaterThan, value)

        {
        }
    }
}
