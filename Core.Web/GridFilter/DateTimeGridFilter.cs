using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter
{
    public class DateTimeGridFilter<T> : BaseGridFilter<T>
    {
        private readonly Expression<Func<T, DateTime>> expression;

        public DateTimeGridFilter(Expression<Func<T, DateTime>> expression, string thead) : base(thead)
        {
            this.expression = expression;
        }

        public override string RenderTd(T entity)
        {
            var value = this.expression.Compile()(entity);
            return base.RenderTd(value);
        }
    }
}