using System;
using System.Linq.Expressions;

namespace Core.Extension.Filters
{
    public class StringIsNullFilter<T> : BaseFilter<T>
    {
        public StringIsNullFilter(Expression<Func<T, string>> expression) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, null)
        {
            this.IsFilterEnable = true;
        }
    }
}
