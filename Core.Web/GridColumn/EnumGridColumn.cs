using System;
using System.Linq.Expressions;

namespace Core.Web.GridColumn
{
    public class EnumGridColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, Enum>> expression;

        public EnumGridColumn(Expression<Func<T, Enum>> expression, string thead) : base(thead)
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