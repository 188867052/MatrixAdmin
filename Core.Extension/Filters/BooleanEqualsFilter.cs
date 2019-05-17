using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class BooleanEqualsFilter<T> : BaseFilter<T>
    {
        public BooleanEqualsFilter(Expression<Func<T, bool?>> expression, bool? value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }
    }
}
