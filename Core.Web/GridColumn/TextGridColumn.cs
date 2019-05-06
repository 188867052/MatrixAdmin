using System;
using System.Linq.Expressions;

namespace Core.Web.GridColumn
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
            return this.RenderTd(value);
        }
    }
}