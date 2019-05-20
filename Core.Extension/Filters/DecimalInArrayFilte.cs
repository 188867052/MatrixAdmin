using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class DecimalInArrayFilte<T> : BaseInArrayFilte<T>
    {
        public DecimalInArrayFilte(Expression<Func<T, decimal>> expression, decimal[] value) : base(expression.GetPropertyName(), value)
        {
        }
    }
}
