using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Model;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Dialog;
using Core.Web.Html;
using Core.Web.ViewConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas
{
    public class StandardController : Controller
    {
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _contentType = "text/html";

        protected ContentResult SearchGridConfiguration<T>(SearchGridPage<T> index)
        {
            return this.RenderContent(index);
        }

        protected IActionResult DataListContent<T>(IList<T> list, Expression<Func<T, int?>> id, Expression<Func<T, string>> displayText)
        {
            string options = list.Aggregate(string.Empty, (current, entity) => current + $"<option key=\"{id.Compile()(entity)}\" value=\"{displayText.Compile()(entity)}\"></option>");

            return this.Content(options, "text/html", Encoding.UTF8);
        }

        protected IActionResult Dialog<TPostModel, T>(DialogConfiguration<TPostModel, T> index)
        {
            return new JsonResult(new { data = index.Render(default), id = index.Identifier.Value });
        }

        protected IActionResult Submit(ResponseModel model)
        {
            return new JsonResult(model);
        }

        protected JsonResult GridConfiguration<T>(GridConfiguration<T> index)
        {
            return new JsonResult(new
            {
                data = index.GenerateGridColumn(),
                pageSize = index.PageSize,
                currentPage = index.CurrentPage,
                pager = index.Pager()
            });
        }

        private ContentResult RenderContent(IRender render)
        {
            return this.Content(render.Render(), this._contentType, this._encoding);
        }
    }
}