using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entity;
using Core.Extension;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Framework;
using Core.Web.Html;
using Core.Web.JavaScript;
using Core.Web.SearchFilterConfiguration;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Home
{
    public abstract class SearchGridPage<T> : IRender
    {
        public static readonly string LeftText = "&laquo;";
        public static readonly string RightText = "&raquo;";

        /// <summary>
        /// Gets title.
        /// </summary>
        /// <returns>A title.</returns>
        protected virtual string Title { get; } = "My Web";

        /// <summary>
        /// Gets html 文件.
        /// </summary>
        /// <returns>A file name.</returns>
        protected virtual string FileName { get; } = "SearchGridPage";

        /// <summary>
        /// Css文件.
        /// </summary>
        /// <returns>css list.</returns>
        public abstract IList<string> Css();

        /// <summary>
        /// 渲染.
        /// </summary>
        /// <returns>A string.</returns>
        public virtual string Render()
        {
            string htmlFormat = new CoreContext().Configuration.FirstOrDefault(o => o.Key == this.FileName)?.Value;

            string html = htmlFormat.Replace("{{head}}", this.HtmlHead());
            html = html.Replace("{{sidebarMenu}}", SidebarNavigation.SidebarMenu());
            html = html.Replace("{{content-header}}", this.ContentHeader());
            html = html.Replace("{{Footer}}", this.Footer());
            html = html.Replace("{{tobHeader}}", SiteConfiguration.TopHeader);

            var filter = this.SearchFilterConfiguration();
            if (filter != null)
            {
                html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
                html = html.Replace("{{button-group}}", filter.GenerateButton());
                html = html.Replace("{{Pager}}", this.Pager());
            }

            var grid = this.GridConfiguration();
            if (grid != null)
            {
                string table = grid.GenerateGridColumn();
                html = html.Replace("{{Table}}", table);
            }

            return html + $"<script>{this.RenderJavaScript()}</script>";
        }

        public string Pager()
        {
            return $"<ul class=\"pagination pagination-md\">" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">{LeftText}</a></li>" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">1</a></li>" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">2</a></li>" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">3</a></li>" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">{RightText}</a></li>" +
                   $"</ul>";
        }

        /// <summary>
        /// JavaScript文件.
        /// </summary>
        /// <returns>The list.</returns>
        protected abstract IList<string> JavaScript();

        protected virtual SearchFilterConfiguration SearchFilterConfiguration()
        {
            return null;
        }

        protected abstract GridConfiguration<T> GridConfiguration();

        protected virtual string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader();
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }

        protected virtual IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            return new List<ViewInstanceConstruction>
            {
                new IndexViewInstance()
            };
        }

        private string Footer()
        {
            return $"<div class=\"row-fluid\">" +
                   $"<div id = \"footer\" class=\"span12\"> 2019 &copy;https://github.com/188867052" +
                   $"<a href=\"http://www.taobao.com/\" target=\"_blank\"> My Blog</a>" +
                   $"</div>" +
                   $"</div>";
        }

        private string RenderJavaScript()
        {
            return this.CreateViewInstanceConstructions().Aggregate<ViewInstanceConstruction, string>(default, (current, instance) => current + instance.ViewInstance().Render());
        }

        private IEnumerable<string> JavaScriptResource()
        {
            List<string> list = new List<string>
            {
                "/js/jquery.min.js",
                "/js/jquery.dataTables.min.js",
                Javascript.Popper,
                Javascript.Bootstrap,
                Javascript.BootstrapDatetimepicker,
                Javascript.Framework,
                Javascript.Core
            };
            list.AddRange(this.JavaScript());

            return list;
        }

        private IEnumerable<string> CssResource()
        {
            List<string> list = new List<string>
            {
                "/css/bootstrap.min.css",
                "/css/bootstrap-responsive.min.css",
                "/css/bootstrap-datetimepicker.css",
                "/css/core.css",
                "/font-awesome/css/font-awesome.css",
            };
            list.AddRange(this.Css());

            return list;
        }

        private string HtmlHead()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in this.CssResource())
            {
                stringBuilder.Append($"<link href=\"{item}\" rel=\"stylesheet\">");
            }

            foreach (var item in this.JavaScriptResource())
            {
                stringBuilder.Append($"<script src=\"{item}\"></script>");
            }

            return $"<head>{stringBuilder}</head>";
        }
    }
}