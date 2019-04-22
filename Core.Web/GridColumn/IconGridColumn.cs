using System;
using System.Linq.Expressions;

namespace Core.Web.GridColumn
{
    public class IconGridColumn<T> : TextGridColumn<T>
    {
        public IconGridColumn(Expression<Func<T, string>> expression, string thead) : base(expression, thead)
        {
        }
    }
}