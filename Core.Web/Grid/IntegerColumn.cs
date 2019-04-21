using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class IntegerColumn<T>
    {
        public Expression<Func<T, int>> Expression { get; set; }

        public string Thead { get; set; }


        public IntegerColumn(Expression<Func<T, int>> expression, string thead)
        {
            this.Expression = expression;
            this.Thead = thead;
        }
    }
}