using System;
using System.Linq.Expressions;

namespace Core.Extension.ExpressionBuilder.Generics
{
    public class DecimalInArrayFilte<T> : BaseInArrayFilte<T>
    {
        public DecimalInArrayFilte(Expression<Func<T, decimal>> expression, decimal[] value) : base(expression.GetPropertyName(), value)
        {
        }
    }
}
