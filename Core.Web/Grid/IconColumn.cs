using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class IconColumn<T> : TextColumn<T>
    {
        public IconColumn(Expression<Func<T, string>> expression, string thead) : base(expression, thead)
        {
        }
    }
}