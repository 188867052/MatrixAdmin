using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter
{
    public class EnumGridFilter<T> : BaseGridFilter<T>
    {
        private readonly Expression<Func<T, Enum>> expression;

        public EnumGridFilter(Expression<Func<T, Enum>> expression, string thead) : base(thead)
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