using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegarEqualFilter<T> : BaseSingleFilter<T>
    {
        public IntegarEqualFilter(Expression<Func<T, int?>> expression, int? value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value, expression)
        {
        }

        public override bool IsFilterEnable => this.Value != null;
    }
}