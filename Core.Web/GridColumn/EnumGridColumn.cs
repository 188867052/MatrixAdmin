using System;
using System.Linq.Expressions;
using Core.Web.Enums;

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
            Enum value = this.expression.Compile()(entity);
            string display = EnumMapping.ToString(value);
            return this.RenderTd(display);
        }
    }
}