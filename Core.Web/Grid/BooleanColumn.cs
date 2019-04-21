using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class BooleanColumn<T>
    {
        public Expression<Func<T, bool>> Expression { get; set; }

        public string Thead { get; set; }


        public BooleanColumn(Expression<Func<T, bool>> expression, string thead)
        {
            this.Expression = expression;
            this.Thead = thead;
        }
    }
}