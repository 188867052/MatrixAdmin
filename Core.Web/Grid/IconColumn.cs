using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.Grid
{
    public class IconColumn<T> : BaseGridColumn<T>
    {
        public IconColumn(Expression<Func<T, string>> expression, string thead) : base(expression.GetPropertyInfo(), thead)
        {
        }
    }
}