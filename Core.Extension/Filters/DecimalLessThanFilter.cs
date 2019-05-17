using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalLessThanFilter<T> : BaseFilter<T>
    {
        public DecimalLessThanFilter(Expression<Func<T, decimal>> expression, decimal max) : base(expression.GetPropertyName(), Operations.Operation.LessThan, max)
        {
        }
    }
}
