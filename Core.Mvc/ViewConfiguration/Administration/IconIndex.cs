using Core.Extension;
using Core.Mvc.Controllers;
using Core.Mvc.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Core.Model;
using Core.Mvc.Controllers.Administration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class IconIndex : SearchGridPage
    {
        private readonly ResponseModel response;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="response"></param>
        public IconIndex(IHostingEnvironment hostingEnvironment, ResponseModel response) : base(hostingEnvironment)
        {
            this.response = response;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/uniform.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override string FileName
        {
            get
            {
                return "Manage";
            }
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
                "/js/icon/index.js",
            };
        }

        public override string Render()
        {
            IconGridConfiguration configuration = new IconGridConfiguration(this.response);
            string table = configuration.Render();
            var html = base.Render().Replace("{{Table}}", table);

            IconSearchGridFilterConfiguration filter = new IconSearchGridFilterConfiguration();
            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());

            return html + RenderJavaScript();
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("图标管理");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        private string RenderJavaScript()
        {
            JavaScript js = new JavaScript("index", "Index");
            Url url = new Url(nameof(Administration), typeof(IconController), nameof(IconController.GridStateChange));
            js.AddUrlInstance("searchUrl", url);

            return $"<script>{js.Render()}</script>";
        }

        public new string Pager()
        {
            JavaScriptEvent js = new JavaScriptEvent("index.search", "page-link");
            string script = $"<script>{js.Render()}</script>";
            return $"<ul class=\"pager\">" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">&laquo;</a></li>" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">1</a></li>" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">2</a></li>" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">3</a></li>" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">&raquo;</a></li>" +
                   $"</ul>" + script;
        }
    }
}