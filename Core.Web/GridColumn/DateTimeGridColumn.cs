using System;
using System.Linq.Expressions;
using Core.Shared.Utilities;

namespace Core.Web.GridColumn
{
    public class DateTimeGridColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, DateTime?>> _expression;

        public DateTimeGridColumn(Expression<Func<T, DateTime?>> expression, string thead) : base(thead)
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