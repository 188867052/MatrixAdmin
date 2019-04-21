using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class TextColumn<T>
    {
        public Expression<Func<T, string>> Expression { get; set; }

        public string Thead { get; set; }

        public TextColumn(Expression<Func<T, string>> expression, string thead)
        {
            this.Expression = expression;
            this.Thead = thead;
        }
    }
}