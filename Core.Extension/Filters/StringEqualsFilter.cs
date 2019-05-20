using System;
using System.Linq.Expressions;
using Core.Extension.FieldInfos;

namespace Core.Extension.Filters
{
    public class StringEqualsFilter<T> : BaseFilter<T>
    {
        public StringEqualsFilter(Expression<Func<T, string>> expression, string value) : base(expression.GetPropertyName(), ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }

        public StringEqualsFilter(StringField fieldInfo, string value) : base(fieldInfo.Value, ExpressionBuilder.Operations.Operation.EqualTo, value)
        {
        }
    }
}
