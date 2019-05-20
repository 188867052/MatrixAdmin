using System;
using System.Linq.Expressions;
using Core.Extension.FieldInfos;

namespace Core.Extension.Filters
{
    public class DecimalEqualFilter<T> : BaseFilter<T>
    {
        public DecimalEqualFilter(Expression<Func<T, decimal?>> expression, decimal? value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }

        public DecimalEqualFilter(DecimalField fieldInfo, decimal? value) : base(fieldInfo.Value, ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}
