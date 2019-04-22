using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.Grid
{
    public sealed class BooleanColumn<T> : BaseGridColumn<T>
    {
        public BooleanColumn(Expression<Func<T, bool>> expression, string thead) : base(expression.GetPropertyInfo(), thead)
        {
        }
    }
}