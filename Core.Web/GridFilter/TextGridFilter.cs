using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter
{
    public class TextGridFilter<T> : BaseGridFilter<T>
    {
        private readonly Expression<Func<T, string>> expression;

        public TextGridFilter(Expression<Func<T, string>> expression, string thead) : base(thead)
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