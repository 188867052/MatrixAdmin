using System;
using System.Linq.Expressions;

namespace Core.Web.GridColumn
{
    public class TextGridColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, string>> _expression;

        public TextGridColumn(Expression<Func<T, string>> expression, string thead) : base(thead)
        {
            this._expression = expression;
        }

        public override string RenderTd(T entity)
        {
            var value = this._expression.Compile()(entity);
            return this.RenderTd(value);
        }
    }
}