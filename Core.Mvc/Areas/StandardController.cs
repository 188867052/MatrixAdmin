using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using Core.Model;
using Core.Mvc.Areas.Redirect.Routes;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Dialog;
using Core.Web.Html;
using Core.Web.ViewConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Mvc.Areas
{
    public class StandardController : Controller
    {
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _contentType = "text/html";

        public AuthenticationHeaderValue Authentication { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            this.HttpContext.Request.Cookies.TryGetValue("token", out string token);
            if (string.IsNullOrEmpty(token))
            {
                if (this.HttpContext.Request.Path.Value != RedirectRoute.Login)
                {
                    this.HttpContext.Response.Redirect(RedirectRoute.Login);
                }
            }
            else
            {
                this.Authentication = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                this.HttpContext.Response.Cookies.Delete("token");
                this.HttpContext.Response.Cookies.Append("token", token);
            }
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
            return new JsonResult(new { data = HtmlContent.ToString(index.Render(default)), id = index.Identifier.Value });
        }

        protected IActionResult Submit(HttpResponseModel model)
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