using System;
using System.Linq.Expressions;
using Core.Extension;

namespace Core.Web.GridColumn
{
    public class RowContextMenuColumn<T> : BaseGridColumn<T>
    {
        private readonly Expression<Func<T, int?>> _expression;

        public RowContextMenuColumn(Expression<Func<T, int?>> expression, string thead, Url url) : base(thead)
        {
            this._expression = expression;
            this.Url = url;
        }

        public Url Url { get; set; }

        public override string RenderTd(T entity)
        {
            var value = this._expression.Compile()(entity);
            string innerHtml = $"<span class=\"icon-list dropdown-toggle\" data-url=\"{this.Url.Render()}\" data-id=\"{value}\" data-toggle=\"dropdown\"></span>" +
                               $"<div class=\"dropdown-menu\"></div>";
            return this.RenderTd(innerHtml);
        }
    }
}