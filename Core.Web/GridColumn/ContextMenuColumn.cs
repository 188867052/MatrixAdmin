using System;
using System.Linq.Expressions;
using Core.Extension;

namespace Core.Web.GridColumn
{
    public class ContextMenuColumn<T> : BaseGridColumn<T>
    {
        private readonly string iconClass;
        private readonly Expression<Func<T, string>> expression;

        public ContextMenuColumn(Expression<Func<T, string>> expression, string thead, Url url) : base(thead)
        {
            //this.iconClass = iconClass;
            this.expression = expression;

            this.Url = url;
        }

        public Url Url { get; set; }

        public override string RenderTd(T entity)
        {
            var value = this.expression.Compile()(entity);
            string innerHtml = $"<span class=\"icon-list dropdown-toggle\" data-url=\"{Url.Render()}\" data-id=\"{value}\" data-toggle=\"dropdown\"></span>" +
                               $"<div class=\"dropdown-menu\"></div>";
            return base.RenderTd(innerHtml);
        }
    }
}