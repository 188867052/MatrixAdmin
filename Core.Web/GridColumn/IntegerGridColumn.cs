using System;
using System.Linq.Expressions;

namespace Core.Web.GridColumn
{
    public class IntegerGridColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, int>> expression;

        public IntegerGridColumn(Expression<Func<T, int>> expression, string thead) : base(thead)
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