using System;
using System.Linq.Expressions;
using Core.Shared.Utilities;

namespace Core.Web.GridColumn
{
    public class TextGridColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, string>> _expression;

        public TextGridColumn(Expression<Func<T, string>> expression, string thead) : base(thead)
        {
            Check.NotNull(expression, nameof(expression));

            this._expression = expression;
        }

        public override string RenderTd(T entity)
        {
            Check.NotNull(entity, nameof(entity));

            var value = this._expression.Compile()(entity);
            return this.RenderTd(value);
        }
    }
}