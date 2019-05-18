using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Core.Entity;
using Core.Extension;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Web.Html;
using Core.Web.JavaScript;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Home
{
    public abstract class SearchGridPage : IRender
    {
        public static readonly string LeftText = "&laquo;";
        public static readonly string RightText = "&raquo;";

        private readonly IHostingEnvironment _hostingEnvironment;

        protected SearchGridPage(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Gets title.
        /// </summary>
        /// <returns>A title.</returns>
        protected virtual string Title { get; } = "My Web";

        /// <summary>
        /// Gets html 文件.
        /// </summary>
        /// <returns>A file name.</returns>
        protected abstract string FileName { get; }

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
            SidebarNavigation sidebarNavigation = new SidebarNavigation();
            string sidebarMenu = sidebarNavigation.GenerateSidebarMenu();

            string contentHeader = this.ContentHeader();
            string htmlFormat = CoreApiContext.Instance.Configuration.FirstOrDefault(o => o.Key == this.FileName).Value;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in this.CssResource())
            {
                stringBuilder.Append($"<link href=\"{item}\" rel=\"stylesheet\">");
            }

            foreach (var item in this.JavaScriptResource())
            {
                stringBuilder.Append($"<script src=\"{item}\"></script>");
            }

            string head = $"<head>{stringBuilder}</head>";
            string html = htmlFormat.Replace("{{head}}", head);
            html = html.Replace("{{sidebarMenu}}", sidebarMenu);
            html = html.Replace("{{content-header}}", contentHeader);
            html = html.Replace("{{Footer}}", this.Footer());

            string tobHeader = CoreApiContext.Instance.Configuration.FirstOrDefault(o => o.Key == "TopHeader").Value;
            html = html.Replace("{{tobHeader}}", tobHeader);

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

        protected virtual string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader();
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }

        protected virtual IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            IList<ViewInstanceConstruction> constructions = new List<ViewInstanceConstruction>
            {
                new IndexViewInstance()
            };
            return constructions;
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
                "/js/popper.js",
                "/js/bootstrap.min.js",
                "/js/bootstrap-datetimepicker.js",
                "/js/framework.js",
                "/js/core.js",
            };
            list.AddRange(this.JavaScript());

            return list;
        }

        private IList<string> CssResource()
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
    }
}