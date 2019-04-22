using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.Grid
{
    public class IconColumn<T> : DynamicGridColumn<T>
    {
        public IconColumn(Expression<Func<T, string>> expression, string thead) : base(expression.GetPropertyInfo(), thead)
        {
        }
    }
}