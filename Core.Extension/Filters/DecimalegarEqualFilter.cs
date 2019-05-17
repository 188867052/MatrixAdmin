using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalegarEqualFilter<T> : BaseSingleFilter<T>
    {
        public DecimalegarEqualFilter(Expression<Func<T, decimal?>> expression, decimal? value) : base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }

        public override bool IsFilterEnable => this.Value != null;
    }
}
