using System;
using System.Linq.Expressions;

namespace Core.Web.GridColumn
{
    public class DateTimeGridColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, DateTime?>> _expression;

        public DateTimeGridColumn(Expression<Func<T, DateTime?>> expression, string thead) : base(thead)
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