using System;
using System.Linq.Expressions;
using Core.Extension.FieldInfos;

namespace Core.Extension.Filters
{
    public class IntegarEqualFilter<T> : BaseFilter<T>
    {
        public IntegarEqualFilter(Expression<Func<T, int?>> expression, int? value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }

        public IntegarEqualFilter(IntegerField fieldInfo, int? value) : base(fieldInfo, ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}