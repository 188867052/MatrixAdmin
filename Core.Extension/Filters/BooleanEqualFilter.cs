using System;
using System.Linq.Expressions;
using Core.Entity;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class BooleanEqualFilter<T> : BaseFilter<T>
    {
        public BooleanEqualFilter(Expression<Func<T, bool?>> expression, bool? value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }

        public BooleanEqualFilter(BooleanField fieldInfo, bool? value) : base(fieldInfo.Value, Operations.Operation.EqualTo, value)
        {
        }
    }
}
