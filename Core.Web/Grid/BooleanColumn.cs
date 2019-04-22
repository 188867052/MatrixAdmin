using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class BooleanColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, bool>> expression;

        public BooleanColumn(Expression<Func<T, bool>> expression, string thead) : base(thead)
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