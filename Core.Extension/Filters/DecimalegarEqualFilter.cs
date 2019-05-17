using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalegarEqualFilter<T> : BaseFilter<T>
    {
        public DecimalegarEqualFilter(Expression<Func<T, decimal?>> expression, decimal? value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }
    }
}
