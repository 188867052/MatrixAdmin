using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter
{
    public class IconGridFilter<T> : TextGridFilter<T>
    {
        public IconGridFilter(Expression<Func<T, string>> expression, string thead) : base(expression, thead)
        {
        }
    }
}