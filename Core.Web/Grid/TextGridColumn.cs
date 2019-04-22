using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class TextGridColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, string>> expression;

        public TextGridColumn(Expression<Func<T, string>> expression, string thead) : base(thead)
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