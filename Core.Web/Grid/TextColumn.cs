using System;
using System.Linq.Expressions;
using Core.Extension.Expression;

namespace Core.Web.Grid
{
    public class TextColumn<T> : BaseGridColumn<T>
    {
        public TextColumn(Expression<Func<T, string>> expression, string thead) : base(expression.GetPropertyInfo(), thead)
        {

        }
    }
}