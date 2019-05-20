using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class IntegerInArrayFilte<T> : BaseInArrayFilte<T>
    {
        public IntegerInArrayFilte(Expression<Func<T, int>> expression, int[] value) : base(expression.GetPropertyName(), value)
        {
        }
    }
}
