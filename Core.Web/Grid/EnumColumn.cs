using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class EnumColumn<T>
    {
        public Expression<Func<T, Enum>> Expression { get; set; }

        public string Thead { get; set; }


        public EnumColumn(Expression<Func<T, Enum>> expression, string thead)
        {
            this.Expression = expression;
            this.Thead = thead;
        }
    }
}