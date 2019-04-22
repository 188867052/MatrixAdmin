using System;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class EnumColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, Enum>> expression;

        public EnumColumn(Expression<Func<T, Enum>> expression, string thead) : base(thead)
        {
            this.expression = expression;
        }

        public override string RenderTd(T entity)
        {
            var value = this.expression.Compile()(entity);
            return $"<td><li><i class=\"{value}\"></i></li></td>";
        }
    }
}