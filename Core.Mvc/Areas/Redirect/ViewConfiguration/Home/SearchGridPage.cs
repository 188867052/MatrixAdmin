using System.Collections.Generic;
using System.Linq;
using Core.Api;
using Core.Entity;
using Core.Mvc.Areas.Redirect.Routes;
using Core.Web.GridFilter;
using Core.Web.Html;
using Core.Web.JavaScript;
using Core.Web.SearchFilterConfiguration;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;
using Microsoft.AspNetCore.Razor.TagHelpers;

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
        public abstract IList<string> CssFiles();

        /// <summary>
        /// 渲染.
        /// </summary>
        /// <returns>A string.</returns>
        public virtual string Render()
        {
            using (CoreContext context = new CoreContext())
            {
                string html = context.Configuration.FirstOrDefault(o => o.Key == this.FileName)?.Value.Replace("{{head}}", this.HtmlHead());
                html = html.Replace("{{sidebarMenu}}", SidebarNavigation.SidebarMenu());
                html = html.Replace("{{content-header}}", this.ContentHeader());
                html = html.Replace("{{Footer}}", this.Footer());
                html = html.Replace("{{tobHeader}}", SiteConfiguration.TopHeader);

                var filter = this.SearchFilterConfiguration();
                if (filter != null)
                {
                    html = html.Replace("{{grid-search-filter}}", HtmlContent.ToString(filter.GenerateSearchFilter()));
                    html = html.Replace("{{button-group}}", HtmlContent.ToString(filter.GenerateButton()));
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
        protected abstract IList<string> JavaScriptFiles();

        protected virtual SearchFilterConfiguration SearchFilterConfiguration()
        {
            return null;
        }

        protected abstract GridConfiguration<T> GridConfiguration();

        protected virtual string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader();
            contentHeader.AddAnchor(new Anchor(RedirectRoute.Index, "Home", "Go to Home", "icon-home", "tip-bottom"));
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
            var div = HtmlContent.TagHelper("div", new TagHelperAttributeList { { "class", "row-fluid" }, });
            var div2 = HtmlContent.TagHelper("div", new TagHelperAttributeList { { "class", "span12" },{ "id", "footer" }, });
            div2.Content.Append(" 2019 ©https://github.com/188867052");
            var a = HtmlContent.TagHelper("a", new TagHelperAttributeList { { "href", "http://www.taobao.com/" },{ "target", "_blank" }, });
            a.Content.SetContent(" My Blog");
            div2.Content.AppendHtml(a);
            div.Content.SetHtmlContent(div2);
            return HtmlContent.ToString(div);
        }

        private string RenderJavaScript()
        {
            return this.CreateViewInstanceConstructions().Aggregate<ViewInstanceConstruction, string>(default, (current, instance) => current + instance.ViewInstance().Render());
        }

        private IEnumerable<string> JavaScriptResource()
        {
            List<string> list = new List<string>
            {
                Javascript.Jquery,
                "/js/jquery.dataTables.min.js",
                Javascript.Popper,
                Javascript.Bootstrap,
                Javascript.BootstrapDatetimepicker,
                Javascript.Framework,
                Javascript.Core,
                Javascript.JqueryRedirect
            };
            list.AddRange(this.JavaScriptFiles());

            return list;
        }

        private IEnumerable<string> CssResource()
        {
            List<string> list = new List<string>
            {
                Css.Bootstrap,
                "/css/bootstrap-responsive.min.css",
                Css.BootstrapDatetimepicker,
                Css.Core,
                "/font-awesome/css/font-awesome.css",
            };
            list.AddRange(this.CssFiles());

            return list;
        }

        private string HtmlHead()
        {
            var head = HtmlContent.TagHelper("head");
            foreach (var item in this.CssResource())
            {
                head.Content.AppendHtml(HtmlContent.TagHelper("link", attributes: new TagHelperAttributeList
               {
                    new TagHelperAttribute("href", item),
                    new TagHelperAttribute("rel", "stylesheet"),
               }));
            }

            foreach (var item in this.JavaScriptResource())
            {
                head.Content.AppendHtml(HtmlContent.TagHelper("script", attributes: new TagHelperAttributeList
               {
                    new TagHelperAttribute("src", item),
               }));
            }

            return HtmlContent.ToString(head);
        }
    }
}