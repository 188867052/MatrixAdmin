using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
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
            return base.RenderTd(value);
        }
    }
}