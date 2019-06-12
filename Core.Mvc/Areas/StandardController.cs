using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using Core.Model;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Dialog;
using Core.Web.Html;
using Core.Web.ViewConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Core.Mvc.Areas
{
    public class StandardController : Controller
    {
        public AuthenticationHeaderValue Authentication;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _contentType = "text/html";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var isSuccess = this.HttpContext.Request.Headers.TryGetValue("token", out StringValues outValue);
            this.Authentication = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, outValue);
        }

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