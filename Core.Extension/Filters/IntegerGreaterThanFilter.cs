using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{

    public class IntegerGreaterThanFilter<T> : BaseFilter<T>
    {
        public IntegerGreaterThanFilter(Expression<Func<T, int>> expression, int value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)

        {
        }
    }
}
