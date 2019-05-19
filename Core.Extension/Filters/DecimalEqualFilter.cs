using System;
using System.Linq.Expressions;
using Core.Entity;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalEqualFilter<T> : BaseFilter<T>
    {
        public DecimalEqualFilter(Expression<Func<T, decimal?>> expression, decimal? value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }

        public DecimalEqualFilter(DecimalField fieldInfo, decimal? value) : base(fieldInfo.Value, Operations.Operation.EqualTo, value)
        {
        }
    }
}
