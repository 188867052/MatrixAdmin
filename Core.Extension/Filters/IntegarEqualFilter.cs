using System;
using System.Linq.Expressions;
using Core.Entity;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegarEqualFilter<T> : BaseFilter<T>
    {
        public IntegarEqualFilter(Expression<Func<T, int?>> expression, int? value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }

        public IntegarEqualFilter(IntegerField fieldInfo, int? value) : base(fieldInfo, Operations.Operation.EqualTo, value)
        {
        }
    }
}