using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegarEqualFilter<T> : BaseFilter<T>
    {
        public IntegarEqualFilter(Expression<Func<T, int?>> expression, int? value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value, expression)
        {
        }
    }
}