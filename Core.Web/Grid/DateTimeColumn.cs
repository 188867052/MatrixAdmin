using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class DateTimeColumn<T>
    {
        public Expression<Func<T, DateTime>> Expression { get; set; }

        public string Thead { get; set; }


        public DateTimeColumn(Expression<Func<T, DateTime>> expression, string thead)
        {
            this.Expression = expression;
            this.Thead = thead;
        }
    }
}