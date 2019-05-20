using System;
using System.Linq.Expressions;
using Core.Extension.FieldInfos;

namespace Core.Extension.Filters
{
    public class BooleanEqualFilter<T> : BaseFilter<T>
    {
        public BooleanEqualFilter(Expression<Func<T, bool?>> expression, bool? value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }

        public BooleanEqualFilter(BooleanField fieldInfo, bool? value) : base(fieldInfo, ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}
