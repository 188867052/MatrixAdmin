using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets.Internal;

namespace Core.Web.GridColumn
{
    public class IconGridColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, string>> expression;

        public IconGridColumn(Expression<Func<T, string>> expression, string thead) : base(thead)
        {
            this.expression = expression;
        }

        public override string RenderTd(T entity)
        {
            var value = this.expression.Compile()(entity);
            var innerHtml = $"<span class=\"{value}\"></span>";
            return this.RenderTd(innerHtml);
        }
    }
}