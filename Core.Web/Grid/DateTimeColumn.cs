using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class DateTimeColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, DateTime>> expression;

        public DateTimeColumn(Expression<Func<T, DateTime>> expression, string thead) : base(thead)
        {
            this.expression = expression;
        }

        public override string RenderTd(T entity)
        {
            var value = this.expression.Compile()(entity);
            return $"<td>{value}</td>";
        }
    }
}