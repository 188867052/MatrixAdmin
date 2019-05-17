using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class IntegarEqualsFilter<T> : BaseFilter<T>
    {
        public IntegarEqualsFilter(Expression<Func<T, int?>> expression, int? value) :    base(expression.GetPropertyName(), Operations.Operation.EqualTo, value)
        {
        }

        public override bool IsFilterEnable => this.Value != null;
    }
}