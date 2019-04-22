using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.Grid
{
    public class IntegerColumn<T> : BaseGridColumn<T>
    {
        public IntegerColumn(Expression<Func<T, int>> expression, string thead) : base(expression.GetPropertyInfo(), thead)
        {
        }
    }
}