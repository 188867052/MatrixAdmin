using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public static string LeftText = "&laquo;";
        public static string RightText = "&raquo;";

        protected readonly IHostingEnvironment HostingEnvironment;

        protected SearchGridPage(IHostingEnvironment hostingEnvironment)
        {
            this.HostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Title
        /// </summary>
        /// <returns></returns>
        protected virtual string Title
        {
            get
            {
                return "My Web";
            }
        }

        /// <summary>
        /// Html文件
        /// </summary>
        /// <returns></returns>
        protected abstract string FileName { get; }

        /// <summary>
        /// Css文件
        /// </summary>
        /// <returns></returns>
        public abstract IList<string> Css();

        private IList<string> CssResource()
        {
            List<string> list = new List<string>
            {
                "/css/bootstrap.min.css",
                "/css/bootstrap-responsive.min.css",
                "/css/bootstrap-datetimepicker.css",
                "/css/core.css",
            };
            list.AddRange(this.Css());

            return list;
        }

        private IList<string> JavaScriptResource()
        {
            List<string> list = new List<string>
            {
                "/js/jquery.min.js",
                "/js/jquery.dataTables.min.js",
                "/js/popper.js",
                "/js/bootstrap.min.js",
                "/js/bootstrap-datetimepicker.js",
                "/js/framework.js",
                "/js/core.js"
            };
            list.AddRange(this.JavaScript());

            return list;
        }

        /// <summary>
        /// JavaScript文件
        /// </summary>
        /// <returns></returns>
        protected abstract IList<string> JavaScript();

        /// <summary>
        /// 渲染
        /// </summary>
        /// <returns></returns>
        public virtual string Render()
        {
            SidebarNavigation sidebarNavigation = new SidebarNavigation();
            string sidebarMenu = sidebarNavigation.GenerateSidebarMenu();

            string contentHeader = this.ContentHeader();
            string htmlFormat = File.ReadAllText(Path.Combine(this.HostingEnvironment.WebRootPath, $@"html\{this.FileName}.html"));
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

            string tobHeader = File.ReadAllText(Path.Combine(this.HostingEnvironment.WebRootPath, $@"html\topHeader.html"));
            html = html.Replace("{{tobHeader}}", tobHeader);

            return html + $"<script>{this.RenderJavaScript()}</script>";
        }


        protected virtual string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader();
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }

        private string Footer()
        {
            return $"<div class=\"row-fluid\">" +
                   $"<div id = \"footer\" class=\"span12\"> 2019 &copy;https://github.com/188867052" +
                   $"<a href=\"http://www.taobao.com/\" target=\"_blank\"> My Blog</a>" +
                   $"</div>" +
                   $"</div>";
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

        private string RenderJavaScript()
        {
            return CreateViewInstanceConstructions().Aggregate<ViewInstanceConstruction, string>(default, (current, instance) => current + instance.ViewInstance().Render());
        }

        protected virtual IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            IList<ViewInstanceConstruction> constructions = new List<ViewInstanceConstruction>
            {
                new IndexViewInstance()
            };
            return constructions;
        }
    }
}